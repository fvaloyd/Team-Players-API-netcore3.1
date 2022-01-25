using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RelacionTablas.Data;
using RelacionTablas.Models;
using RelacionTablas.Repository.Interfaces;

namespace RelacionTablas.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationContext _context;
        public AuthRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordSalt;
            byte[] passwordHash;

            CreatePasswordHash(password, out passwordSalt, out passwordHash);
            user.passwordHash = passwordHash;
            user.passwordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }
        private void CreatePasswordHash(string password, out byte[] passwordSalt, out byte[] passwordHash)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<User> Login(string password, string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if(user == null)return null;

            if(!verifyPasswordHash(password, user.passwordSalt, user.passwordHash)) return null;

            return user;
            
        }
        private bool verifyPasswordHash(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for(int i = 0; i < computeHash.Length; i++)
                {
                    if(computeHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }

        public async Task<bool> UserExist(string email)
        {
            if(await _context.Users.AnyAsync(u => u.Email == email)) return true;
            return false;
        }


    }
}
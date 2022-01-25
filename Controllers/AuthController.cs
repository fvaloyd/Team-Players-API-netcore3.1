using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RelacionTablas.Dtos;
using RelacionTablas.Models;
using RelacionTablas.Repository.Interfaces;
using RelacionTablas.Services.Interfaces;

namespace RelacionTablas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly ITokenService _token;
        private readonly IMapper _mapper;
        public AuthController(IAuthRepository repo, ITokenService token, IMapper mapper)
        {
            _repo = repo;
            _token = token;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto userDto)
        {
            userDto.Email = userDto.Email.ToLower();

            if(await _repo.UserExist(userDto.Email)) return BadRequest("User exist");

            var user = _mapper.Map<User>(userDto);

            var userCreated = await _repo.Register(user, userDto.Password);

            var userCreatedDto = _mapper.Map<UserDto>(userCreated);

            return Ok(userCreatedDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userDto)
        {
            var userLog = await _repo.Login(userDto.Password, userDto.Email);

            if(userLog == null) return Unauthorized("User is not authorize");

            var token = _token.CreateToken(userLog);

            var LoginuserDto = _mapper.Map<UserDto>(userLog);

            return Ok(new{
                token = token,
                LoginuserDto = LoginuserDto
            });
        }
    }
}
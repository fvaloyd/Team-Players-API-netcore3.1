using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RelacionTablas.Data;
using RelacionTablas.Dtos;
using RelacionTablas.Models;
using RelacionTablas.Repository.Interfaces;

namespace RelacionTablas.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        public PlayerRepository(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetPlayersDto>> GetPlayersDtoAsync()
        {
            var playersDto = await _context.Players.Include(x => x.Team).Select(p => _mapper.Map<GetPlayersDto>(p)).ToListAsync();

            return playersDto;
        }
        public async Task<GetPlayersDto> GetPlayerDtoByIdAsync(int id)
        {
            var player = await _context.Players.Include(x => x.Team).FirstOrDefaultAsync(p => p.Id == id);

            var playerDto = _mapper.Map<GetPlayersDto>(player);

            return playerDto;
        }
        public async Task<int> CreatePlayerAsync(PostPlayerDto playerDto)
        {
            var player = _mapper.Map<Player>(playerDto);

            await _context.Players.AddAsync(player);

            return await _context.SaveChangesAsync();
        }
        public async Task<int> EditPlayerAsync(int id, PostPlayerDto playerDto)
        {
            var player = await _context.Players.FindAsync(id);

            _mapper.Map(playerDto, player);

            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeletePlayerAsync(int id)
        {
            var player = await _context.Players.FindAsync(id);

            _context.Players.Remove(player);
        
            return await _context.SaveChangesAsync();
        }
    }
}
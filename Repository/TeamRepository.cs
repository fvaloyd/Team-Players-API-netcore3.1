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
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        public TeamRepository(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GetTeamsDto>> GetTeamsDtoAsync()
        {
            var teamsDto = await _context.Teams.Include(x => x.Players).Select(t => _mapper.Map<GetTeamsDto>(t)).ToListAsync();

            return teamsDto;
        }
        public async Task<GetTeamsDto> GetTeamByIdAsync(int id)
        {
            var team = await _context.Teams.Include(x => x.Players).FirstOrDefaultAsync(t => t.Id == id);
            
            var teamDto = _mapper.Map<GetTeamsDto>(team);

            return teamDto;
        }
        public async Task<int> CreateTeamAsync(PostTeamDto teamDto)
        {
            var team = _mapper.Map<Team>(teamDto);

            await _context.Teams.AddAsync(team);

            return await _context.SaveChangesAsync();
        }
        public async Task<int> EditTeamAsync(int id, PostTeamDto teamDto)
        {
            var team = await _context.Teams.FindAsync(id);
            
            _mapper.Map(teamDto, team);

            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteTeamAsync(int id)
        {
            var team = await _context.Teams.Include(x => x.Players).FirstOrDefaultAsync(x => x.Id == id);

            _context.RemoveRange(team.Players);
            _context.Remove(team);

            return await _context.SaveChangesAsync();
        }
        
    }
}
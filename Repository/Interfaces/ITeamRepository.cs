using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RelacionTablas.Dtos;

namespace RelacionTablas.Repository.Interfaces
{
    public interface ITeamRepository
    {
        Task<IEnumerable<GetTeamsDto>> GetTeamsDtoAsync();
        Task<GetTeamsDto> GetTeamByIdAsync(int id);
        Task<int> CreateTeamAsync(PostTeamDto teamDto);
        Task<int> EditTeamAsync(int id, PostTeamDto teamDto);
        Task<int> DeleteTeamAsync(int id); 
    }
}
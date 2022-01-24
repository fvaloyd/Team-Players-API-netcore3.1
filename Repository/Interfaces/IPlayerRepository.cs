using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RelacionTablas.Dtos;

namespace RelacionTablas.Repository.Interfaces
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<GetPlayersDto>> GetPlayersDtoAsync();
        Task<GetPlayersDto> GetPlayerDtoByIdAsync(int id);
        Task<int> CreatePlayerAsync(PostPlayerDto playerDto);
        Task<int> EditPlayerAsync(int id, PostPlayerDto playerDto);
        Task<int> DeletePlayerAsync(int id);
    }
}
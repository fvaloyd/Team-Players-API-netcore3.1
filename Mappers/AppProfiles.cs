using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RelacionTablas.Dtos;
using RelacionTablas.Models;

namespace RelacionTablas.Mappers
{
    public class AppProfiles : Profile
    {
        public AppProfiles()
        {
            //Player mapper
            CreateMap<PostPlayerDto, Player>();
            CreateMap<Player, GetPlayersDto>()
                        .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src => src.Team.Name));

            //Team mapper
            CreateMap<PostTeamDto, Team>();
            CreateMap<Team, GetTeamsDto>()
                        .ForMember(dest => dest.PlayersName, opt => opt.MapFrom(src => src.Players.Select(x => x.Name)));

            //User mapper
            CreateMap<UserRegisterDto, User>();
            CreateMap<UserLoginDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
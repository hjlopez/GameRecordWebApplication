using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.DTOs.Billiards;
using API.Entities;
using API.Entities.Billiards;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // map from, map to

            // for member -> what property are we going to affect
            // ForMember(destination, map from(source))
            CreateMap<AppUser, UserDto>()
                .ForMember(d => d.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault().Url));
            CreateMap<Photo, PhotoDto>();
            CreateMap<RegisterDto, AppUser>();
            CreateMap<UserUpdateDto, AppUser>();
            CreateMap<AdminUserUpdateDto, AppUser>();
            CreateMap<Games, GamesDto>();
            CreateMap<Tournament, BilliardsTournamentDto>();
            CreateMap<BilliardsTournamentDto, Tournament>();
            CreateMap<BilliardsTournamentMembersDto, TournamentMembers>();
            CreateMap<TournamentMembers, BilliardsTournamentMembersDto>();
            CreateMap<BilliardsMatchType, BilliardsMatchTypeDto>();
            CreateMap<BilliardsMatchTypeDto, BilliardsMatchType>();
            CreateMap<TournamentMatchType, TournamentMatchTypesDto>();
            CreateMap<TournamentMatchTypesDto, TournamentMatchType>();
            CreateMap<BilliardsMode, BilliardsModeDto>();
            CreateMap<BilliardsModeDto, BilliardsMode>();
            CreateMap<TournamentModeDto, TournamentMode>();
            CreateMap<TournamentMode, TournamentModeDto>();
            CreateMap<SeasonDto, Season>();
            CreateMap<Season, SeasonDto>();
            CreateMap<SeasonHistoryDto, SeasonHistory>();
            CreateMap<SeasonHistory, SeasonHistoryDto>();
            CreateMap<TournamentMembers, BilliardsTournamentDto>();
            CreateMap<BilliardsMatchDto, BilliardsMatch>();
            CreateMap<BilliardsMatch, BilliardsMatchDto>();
            CreateMap<SeasonHistory, SeasonHistoryDto>();
            CreateMap<SeasonHistoryDto, SeasonHistory>();
        }

        
    }
}
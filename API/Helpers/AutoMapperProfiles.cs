using API.DTOs;
using API.Entities;
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
            CreateMap<RegisterDto, AppUser>();
        }

        
    }
}
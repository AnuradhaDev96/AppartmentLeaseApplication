using AppartmentLeaseAPI.Dtos;
using AppartmentLeaseAPI.Models;
using AutoMapper;

namespace AppartmentLeaseAPI.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserModel, UserGetDto>();
            CreateMap<UserGetDto, UserModel>();
        }
    }
}

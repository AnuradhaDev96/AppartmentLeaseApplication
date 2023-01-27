using AppartmentLeaseAPI.Dtos;
using AppartmentLeaseAPI.Models;
using AppartmentLeaseAPI.Models.Anonymous;
using AppartmentLeaseAPI.Models.Apartments;
using AutoMapper;

namespace AppartmentLeaseAPI.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserModel, UserGetDto>();
            CreateMap<UserGetDto, UserModel>();

            CreateMap<ApartmentClass, ApartmentClassFacilitiesDto>();
            CreateMap<ApartmentClassFacilitiesDto, ApartmentClass>();

            CreateMap<ReservationInquiry, ReservationInquiryCreateDto>();
            CreateMap<ReservationInquiryCreateDto, ReservationInquiry>();

            CreateMap<Apartment, ApartmentGetDto>();
            CreateMap<ApartmentGetDto, Apartment>();
        }
    }
}

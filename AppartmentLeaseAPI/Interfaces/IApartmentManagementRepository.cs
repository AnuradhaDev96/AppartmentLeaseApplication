using AppartmentLeaseAPI.Data.Enums;
using AppartmentLeaseAPI.Dtos;
using AppartmentLeaseAPI.Models.Apartments;

namespace AppartmentLeaseAPI.Interfaces
{
    public interface IApartmentManagementRepository
    {
        ICollection<ApartmentClassFacilitiesDto> GetApartmentClasses();

        ICollection<ApartmentGetDto> GetAvailableApartments();

        ICollection<ApartmentGetDto> FilterAvailableApartments(string location = "", string apartmentType = "");

        ICollection<ParkingSpace> GetAvailableParkingSpaces();

        ApartmentClass? GetApartmentClassDetails(int apartmentId);

        ParkingSpace? GetParkingSpaceById(int parkingSpaceId);

        bool UpdateApartmentStatus(int apartmentId, ApartmentAvailabilityStatus statusToUpdate);

        bool UpdateParkingSpaceStatus(int parkingSpaceId, ParkingSpaceStatus statusToUpdate);
    }
}

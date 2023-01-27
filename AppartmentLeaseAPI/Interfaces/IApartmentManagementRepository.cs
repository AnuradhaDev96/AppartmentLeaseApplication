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
    }
}

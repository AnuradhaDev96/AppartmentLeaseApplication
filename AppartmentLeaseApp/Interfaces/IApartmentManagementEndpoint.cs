using AppartmentLeaseApp.Models.Apartments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.Interfaces
{
    public interface IApartmentManagementEndpoint
    {
        Task<List<ApartmentClassFacilitiesResponse>> GetAppartmentClassesWithFacilities();

        Task<List<ApartmentsResponse>> GetAvailableApartmentsWithDetails();

        Task<List<ApartmentsResponse>> FilterAvailableApartments(string location, string apartmentType);

        Task<List<ParkingSpaceResponse>> GetAvailableParkingSpaces();
    }
}

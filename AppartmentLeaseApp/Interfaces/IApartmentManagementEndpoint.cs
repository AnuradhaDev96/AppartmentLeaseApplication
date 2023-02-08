using AppartmentLeaseApp.Models.Apartments;
using AppartmentLeaseApp.Models.LeaseAgreement;
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

        Task<LeaseAgreementPricingGetResponse?> GetApartmentPricingDetails(int apartmentId, int? purchasedParkingId, DateTime leaseStartDate, DateTime leaseEndDate);

        Task<List<ApartmentsResponse>> GetAllApartmentsWithDetails();

        Task<List<ApartmentsResponse>> FilterAllApartments(string location, string apartmentType);

        Task<List<ApartmentsWithMatchingWaitingApplicationsResponse>> GetAvailableApartmentsWithMatchingWaitingApplicationInfo();
    }
}

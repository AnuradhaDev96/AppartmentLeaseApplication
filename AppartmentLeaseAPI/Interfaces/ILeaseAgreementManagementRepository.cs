using AppartmentLeaseAPI.Models.LeaseManagement;

namespace AppartmentLeaseAPI.Interfaces
{
    public interface ILeaseAgreementManagementRepository
    {
        Task<int?> CreateLeaseAgreement(LeaseAgreement leaseAgreement);
    }
}

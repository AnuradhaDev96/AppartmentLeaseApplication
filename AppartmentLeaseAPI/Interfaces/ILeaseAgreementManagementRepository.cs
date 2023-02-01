using AppartmentLeaseAPI.Dtos;
using AppartmentLeaseAPI.Models.LeaseManagement;

namespace AppartmentLeaseAPI.Interfaces
{
    public interface ILeaseAgreementManagementRepository
    {
        Task<int?> CreateLeaseAgreement(LeaseAgreement leaseAgreement);

        ICollection<LeaseAgreement>? GetLeaseAgreementsByChiefOccupantId(int chiefOccupantId);

        LeaseAgreement? GetLeaseAgreementByAgreementId(int agreementId);

        ICollection<LeaseExtentionRequest>? GetLeaseExtentionRequestsByLeaseeAgreementId(int leaseAgreementId);
    }
}

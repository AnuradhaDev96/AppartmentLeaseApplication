using AppartmentLeaseApp.Models.Customers;
using AppartmentLeaseApp.Models.LeaseAgreement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.Interfaces
{
    public interface ILeaseAgreementManagementEndpoint
    {
        Task<string?> CreateLeaseAgreementForGuestUser(CreateLeaseAgreementModel createLeaseAgreementModel);

        Task<List<LeaseAgreementSummaryResponse>>? GetLeaseAgreementsByLoggedUser(int userId);

        #region Dependants
        Task<List<DependantResponseModel>>? GetDependantsByLoggedUser(int userId, int selectedLeaseAgreementId);

        Task<string?> CreateDependantByChiefOccupantUserId(int leaseAgreementId, int userId, DependantCreateModel dependantCreateModel);

        Task<string?> UpdateDependant(int userId, DependantResponseModel updateData);

        Task<string?> DeleteDependant(int userId, int dependantId);
        #endregion

        #region LeaseExtentionRequests
        Task<List<LeaseExtentionGetResponse>>? GetLeaseExtentionsByLeaseAgreementId(int selectedLeaseAgreementId);

        #endregion
    }
}

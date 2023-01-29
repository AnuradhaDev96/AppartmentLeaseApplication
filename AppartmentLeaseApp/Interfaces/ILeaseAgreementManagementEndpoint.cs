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

        Task<List<LeaseAgreementSummaryResponse>>? GetLeaseAgreementsByChiefOccupant(int chiefOccupantId);
    }
}

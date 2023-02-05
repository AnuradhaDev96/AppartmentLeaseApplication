using AppartmentLeaseApp.Models.AnonymousModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.Interfaces
{
    public interface IAnonymousManagementEndpoint
    {
        Task<string?> CreateReservationRequest(string fullName, string email, string telephoneNumber);

        Task<List<ReservationRequestResponse>?> GetReservationRequests();

        Task<string?> CreateWaitingApplication(CreateWaitingApplicationRequest waitingApplication);
    }
}

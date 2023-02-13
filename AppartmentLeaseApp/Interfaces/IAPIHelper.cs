using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.Interfaces
{
    public interface IAPIHelper
    {
        HttpClient ApiClient { get; }

        Task<string?> Authenticate(string username, string password);

        Task SyncLoggedInUser(string token);

        void OnLogoutUser();
    }
}

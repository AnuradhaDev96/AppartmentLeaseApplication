using AppartmentLeaseApp.Interfaces;
using AppartmentLeaseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.ApiProviders
{
    public class UserManagementEndpoint : IUserManagementEndpoint
    {
        private IAPIHelper _apiHelper;

        public UserManagementEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<List<SystemUser>> GetAllUsers()
        {
            using (HttpResponseMessage responseMessage = await _apiHelper.ApiClient.GetAsync(requestUri: "UserManagement/GetUsers"))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = await responseMessage.Content.ReadAsAsync<List<SystemUser>>();

                    return result;
                    //return result;
                }
                else
                {
                    throw new Exception(responseMessage.StatusCode.ToString());
                }
            }
        }
    }
}

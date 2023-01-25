using AppartmentLeaseApp.Interfaces;
using AppartmentLeaseApp.Models.Apartments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.ApiProviders
{
    public class ApartmentManagementEndpoint : IApartmentManagementEndpoint
    {
        private IAPIHelper _apiHelper;

        public ApartmentManagementEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<List<ApartmentClassFacilitiesResponse>> GetAppartmentClassesWithFacilities()
        {
            using (HttpResponseMessage responseMessage = await _apiHelper.ApiClient.GetAsync(requestUri: "ApartmentManagement/ApartmentClasses"))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = await responseMessage.Content.ReadAsAsync<List<ApartmentClassFacilitiesResponse>>();

                    return result;
                }
                else
                {
                    throw new Exception(responseMessage.StatusCode.ToString());
                }
            }
        }
    }
}

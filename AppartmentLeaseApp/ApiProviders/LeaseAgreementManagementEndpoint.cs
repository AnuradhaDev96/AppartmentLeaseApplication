using AppartmentLeaseApp.Interfaces;
using AppartmentLeaseApp.Models.Customers;
using AppartmentLeaseApp.Models.LeaseAgreement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.ApiProviders
{
    public class LeaseAgreementManagementEndpoint : ILeaseAgreementManagementEndpoint
    {
        private IAPIHelper _apiHelper;

        public LeaseAgreementManagementEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<string?> CreateLeaseAgreementForGuestUser(CreateLeaseAgreementModel data)
        {

            using (HttpResponseMessage responseMessage = await _apiHelper.ApiClient.PostAsync(requestUri: "LeaseAgreementManagement/LeaseAgreements", content: data.ToStringContent()))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = await responseMessage.Content.ReadAsAsync<string>();

                    return result;
                }
                else
                {
                    throw new Exception(responseMessage.StatusCode.ToString());
                }
            }
        }

        public async Task<List<DependantResponseModel>>? GetDependantsByLoggedUser(int userId, int selectedLeaseAgreementId)
        {
            using (HttpResponseMessage responseMessage = await _apiHelper.ApiClient.GetAsync(requestUri: @$"LeaseAgreementManagement/LeaseAgreements/User/{userId}/Dependants/{selectedLeaseAgreementId}"))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = await responseMessage.Content.ReadAsAsync<List<DependantResponseModel>>();

                    return result;
                }
                else
                {
                    throw new Exception(responseMessage.StatusCode.ToString());
                }
            }
        }

        public async Task<List<LeaseAgreementSummaryResponse>>? GetLeaseAgreementsByLoggedUser(int userId)
        {
            using (HttpResponseMessage responseMessage = await _apiHelper.ApiClient.GetAsync(requestUri: @$"LeaseAgreementManagement/LeaseAgreements/User/{userId}"))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = await responseMessage.Content.ReadAsAsync<List<LeaseAgreementSummaryResponse>>();

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

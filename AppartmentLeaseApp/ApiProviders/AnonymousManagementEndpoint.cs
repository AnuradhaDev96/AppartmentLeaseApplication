using AppartmentLeaseApp.Interfaces;
using AppartmentLeaseApp.Models.AnonymousModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.ApiProviders
{
    public class AnonymousManagementEndpoint : IAnonymousManagementEndpoint
    {
        private IAPIHelper _apiHelper;

        public AnonymousManagementEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<string?> CreateReservationRequest(string fullName, string email, string telephoneNumber)
        {
            var data = new CreateReservationRequestModel
            {
                FullName = fullName,
                Email = email,
                TelephoneNo = telephoneNumber
            };

            using (HttpResponseMessage responseMessage = await _apiHelper.ApiClient.PostAsync(requestUri: "AnonymousManagement/ReservationRequests", content: data.ToStringContent()))
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

        public async Task<string?> CreateWaitingApplication(CreateWaitingApplicationRequest waitingApplication)
        {
            var sc = waitingApplication.ToStringContent();
            using (HttpResponseMessage responseMessage = await _apiHelper.ApiClient.PostAsync(requestUri: "AnonymousManagement/WaitingApplications", content: waitingApplication.ToStringContent()))
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

        public async Task<List<ReservationRequestResponse>?> GetReservationRequests()
        {
            using (HttpResponseMessage responseMessage = await _apiHelper.ApiClient.GetAsync(requestUri: "AnonymousManagement/ReservationRequests"))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = await responseMessage.Content.ReadAsAsync<List<ReservationRequestResponse>?>();

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

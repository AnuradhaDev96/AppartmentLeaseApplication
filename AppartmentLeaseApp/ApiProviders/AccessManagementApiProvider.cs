using AppartmentLeaseApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.ApiProviders
{
    public class AccessManagementApiProvider
    {
        private APIHelper _apiHelper;

        public AccessManagementApiProvider()
        {
            _apiHelper = new APIHelper();
        }

        public async Task Authenticate(string username, string password)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", password),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
            });

            //using (HttpResponseMessage responseMessage = await _apiHelper.ApiClient.PostAsync(requestUri: "Token", content: data))
            //{
            //    if (responseMessage.IsSuccessStatusCode)
            //    {
            //        var result = await responseMessage.Content.ReadAsAsync<string>();
            //    }
            //}
        }
    }
}

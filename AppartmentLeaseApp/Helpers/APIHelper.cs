using AppartmentLeaseApp.Interfaces;
using AppartmentLeaseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.Helpers
{
    public class APIHelper : IAPIHelper
    {        
        private ILoggedInUser _loggedInUser;

        public APIHelper(ILoggedInUser loggedInUser)
        {
            InitializeClient();
            _loggedInUser = loggedInUser;
        }

        private void InitializeClient()
        {
            _apiClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7031/api/")
                
            };
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        
        private HttpClient _apiClient;

        public HttpClient ApiClient
        {
            get { return _apiClient; }
        }

        public async Task<string?> Authenticate(string username, string password)
        {
            //var data = new FormUrlEncodedContent(new[]
            //{
            //    new KeyValuePair<string, string>("username", username),
            //    new KeyValuePair<string, string>("password", password),
            //});

            var data = new LoginModel
            {
                username = username,
                password = password
            };

            using (HttpResponseMessage responseMessage = await _apiClient.PostAsync(requestUri: "LoginManagement/PasswordLogin", content: data.ToStringContent()))
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

        public async Task SyncLoggedInUser(string token)
        {
            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");


            using (HttpResponseMessage responseMessage = await _apiClient.GetAsync(requestUri: "UserManagement/GetCurrentUser"))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = await responseMessage.Content.ReadAsAsync<LoggedInUser>();

                    _loggedInUser.Token = token;
                    _loggedInUser.Username = result.Username;
                    _loggedInUser.Role = result.Role;
                    _loggedInUser.Email = result.Email;
                    _loggedInUser.Id = result.Id;
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

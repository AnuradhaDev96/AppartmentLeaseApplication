using AppartmentLeaseApp.Interfaces;
using AppartmentLeaseApp.Models.Apartments;
using AppartmentLeaseApp.Models.LeaseAgreement;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        public async Task<List<ApartmentsResponse>> FilterAllApartments(string location, string apartmentType)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (string.IsNullOrEmpty(location.Trim()) && string.IsNullOrEmpty(apartmentType.Trim()))
            {
                stringBuilder.Append("ApartmentManagement/Apartments/Filter");
            }
            else
            {
                stringBuilder.Append("ApartmentManagement/Apartments/Filter?");

                NameValueCollection queryParams = System.Web.HttpUtility.ParseQueryString(string.Empty);
                if (!string.IsNullOrEmpty(location.Trim()))
                {
                    queryParams.Add("Location", location);
                }

                if (!string.IsNullOrEmpty(apartmentType.Trim()))
                {
                    queryParams.Add("ApartmentType", apartmentType);
                }
                var keys = queryParams.ToString();
                stringBuilder.Append(queryParams.ToString());

            }

            var res = stringBuilder.ToString();
            using (HttpResponseMessage responseMessage = await _apiHelper.ApiClient.GetAsync(requestUri: stringBuilder.ToString()))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = await responseMessage.Content.ReadAsAsync<List<ApartmentsResponse>>();

                    return result;
                }
                else
                {
                    throw new Exception(responseMessage.StatusCode.ToString());
                }
            }
        }

        public async Task<List<ApartmentsResponse>> GetAllApartmentsWithDetails()
        {
            using (HttpResponseMessage responseMessage = await _apiHelper.ApiClient.GetAsync(requestUri: "ApartmentManagement/Apartments"))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = await responseMessage.Content.ReadAsAsync<List<ApartmentsResponse>>();

                    return result;
                }
                else
                {
                    throw new Exception(responseMessage.StatusCode.ToString());
                }
            }
        }

        public async Task<List<ApartmentsResponse>> FilterAvailableApartments(string location, string apartmentType)
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            if (string.IsNullOrEmpty(location.Trim()) && string.IsNullOrEmpty(apartmentType.Trim()))
            {
                stringBuilder.Append("ApartmentManagement/Apartments/AvailableApartments/Filter");
            }
            else
            {
                stringBuilder.Append("ApartmentManagement/Apartments/AvailableApartments/Filter?");

                NameValueCollection queryParams = System.Web.HttpUtility.ParseQueryString(string.Empty);
                if (!string.IsNullOrEmpty(location.Trim()))
                {                    
                    queryParams.Add("Location", location);
                }

                if (!string.IsNullOrEmpty(apartmentType.Trim()))
                {
                    queryParams.Add("ApartmentType", apartmentType);                    
                }
                var keys = queryParams.ToString();
                stringBuilder.Append(queryParams.ToString());

            }           
            
            var res = stringBuilder.ToString();
            using (HttpResponseMessage responseMessage = await _apiHelper.ApiClient.GetAsync(requestUri: stringBuilder.ToString()))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = await responseMessage.Content.ReadAsAsync<List<ApartmentsResponse>>();

                    return result;
                }
                else
                {
                    throw new Exception(responseMessage.StatusCode.ToString());
                }
            }
        }

        public async Task<LeaseAgreementPricingGetResponse?> GetApartmentPricingDetails(int apartmentId, int? purchasedParkingId, DateTime leaseStartDate, DateTime leaseEndDate)
        {
            StringBuilder stringBuilder = new StringBuilder("ApartmentManagement/ApartmentClasses/PricingDetails?");

            NameValueCollection queryParams = System.Web.HttpUtility.ParseQueryString(string.Empty);

            queryParams.Add("ApartmentId", apartmentId.ToString());
            queryParams.Add("LeaseStartDate", leaseStartDate.ToString());
            queryParams.Add("LeaseEndDate", leaseEndDate.ToString());

            if (purchasedParkingId != null || purchasedParkingId > 0)
                queryParams.Add("PurchaseParkingSpaceId", purchasedParkingId.ToString());

            stringBuilder.Append(queryParams.ToString());

            var res = stringBuilder.ToString();
            using (HttpResponseMessage responseMessage = await _apiHelper.ApiClient.GetAsync(requestUri: stringBuilder.ToString()))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = await responseMessage.Content.ReadAsAsync<LeaseAgreementPricingGetResponse?>();

                    return result;
                }
                else
                {
                    throw new Exception(responseMessage.StatusCode.ToString());
                }
            }
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

        public async Task<List<ApartmentsResponse>> GetAvailableApartmentsWithDetails()
        {
            using (HttpResponseMessage responseMessage = await _apiHelper.ApiClient.GetAsync(requestUri: "ApartmentManagement/Apartments/AvailableApartments"))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = await responseMessage.Content.ReadAsAsync<List<ApartmentsResponse>>();

                    return result;
                }
                else
                {
                    throw new Exception(responseMessage.StatusCode.ToString());
                }
            }
        }

        public async Task<List<ParkingSpaceResponse>> GetAvailableParkingSpaces()
        {
            using (HttpResponseMessage responseMessage = await _apiHelper.ApiClient.GetAsync(requestUri: "ApartmentManagement/ParkingSpaces/AvailableParkingSpaces"))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = await responseMessage.Content.ReadAsAsync<List<ParkingSpaceResponse>>();

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

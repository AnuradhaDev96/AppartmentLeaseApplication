using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.Models.LeaseAgreement
{
    public class CreateLeaseAgreementModel
    {
        public int ReservationInquiryId { get; set; }

        #region LeaseAgreementTable
        public DateTime StartDate { get; set; }

        public DateTime EndtDate { get; set; }

        public int? PurchasedParkingSpaceId { get; set; }

        public int ApartmentId { get; set; }
        #endregion

        #region ChiefOccupant Details
        public string FullName { get; set; }

        public string Address { get; set; }

        public string NICPassportNo { get; set; }

        public string EmergencyContactNo { get; set; }
        #endregion

        #region  Sysem User Conversion
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        #endregion

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public StringContent ToStringContent()
        {
            return new StringContent(ToJson(), Encoding.UTF8, "application/json");
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.Models.AnonymousModels
{
    public class CreateReservationRequestModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string TelephoneNo { get; set; }

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

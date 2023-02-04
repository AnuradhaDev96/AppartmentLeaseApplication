using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.Models.LeaseAgreement
{
    public class LeaseExtentionCreateRequest
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int LeaseAgreementId { get; set; }

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

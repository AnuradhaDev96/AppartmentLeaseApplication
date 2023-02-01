using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.Models.LeaseAgreement
{
    public class LeaseAgreementSummaryResponse
    {
        //Lease agreement data
        public int AgreementId { get; set; }
        public string Status { get; set; }
        public string DownPaymentStatus { get; set; }
        public DateTime LeaseStartDate { get; set; }
        public DateTime LeaseEndDate { get; set; }

        //Apartment data
        public int ApartmentId { get; set; }
        public string ApartmentClassName { get; set; }
        public string BuildingName { get; set; }
        public string BuildingLocation { get; set; }
        public int MaximumOccupantCount { get; set; }
    }
}

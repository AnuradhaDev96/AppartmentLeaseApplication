using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.Models.AnonymousModels
{
    public class ReservationRequestResponse
    {
        public int Id { get; set; }

        public string FullName { get; set; }
        public string TelephoneNo { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// If (InquiryStatus == WaitingList) WaitingApplicationId != null
        /// If (InquiryStatus == LeaseCreated) LeaseAgreementId != null
        /// </summary>
        public string Status { get; set; }

        //[ForeignKey("WaitingApplicationId")]
        //public int? WaitingApplicationId { get; set; }
        //public WaitingApplication? WaitingApplication { get; set; }

        //[ForeignKey("LeaseAgreementId")]
        //public int? LeaseAgreementId { get; set; }
        //public LeaseAgreement? LeaseAgreement { get; set; }

    }
}

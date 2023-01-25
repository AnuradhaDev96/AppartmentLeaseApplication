using AppartmentLeaseAPI.Data.Enums;
using AppartmentLeaseAPI.Models.LeaseManagement;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppartmentLeaseAPI.Models.Anonymous
{
    public class ReservationInquiry
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; }
        public string TelephoneNo { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// If (InquiryStatus == WaitingList) WaitingApplicationId != null
        /// If (InquiryStatus == LeaseCreated) LeaseAgreementId != null
        /// </summary>
        public InquiryStatus Status { get; set; }
        
        [ForeignKey("WaitingApplicationId")]
        public int? WaitingApplicationId { get; set; }
        public WaitingApplication? WaitingApplication { get; set; }
        
        [ForeignKey("LeaseAgreementId")]
        public int? LeaseAgreementId { get; set; }
        public LeaseAgreement? LeaseAgreement { get; set; }


    }
}

using AppartmentLeaseAPI.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppartmentLeaseAPI.Models.LeaseManagement
{
    public class LeaseExtentionRequest
    {
        [Key]
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public LeaseExtentionRequestStatus Status { get; set; }

        [ForeignKey("LeaseAgreementId")]
        public int LeaseAgreementId { get; set; }
        public LeaseAgreement LeaseAgreement { get; set; }
    }
}

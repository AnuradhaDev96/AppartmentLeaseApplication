using AppartmentLeaseAPI.Data.Enums;
using AppartmentLeaseAPI.Models.LeaseManagement;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppartmentLeaseAPI.Models.Payments
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        public PaymentReason PaymentReason { get; set; }

        public DateTime PaidOn;

        [ForeignKey("LeaseAgreementId")]
        public int LeaseAgreementId { get; set; }
        public LeaseAgreement LeaseAgreement { get; set; }
    }
}

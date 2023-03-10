using AppartmentLeaseAPI.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppartmentLeaseAPI.Models.LeaseManagement
{
    public class PaymentInstallment
    {
        [Key]
        public int Id { get; set; }

        public PaymentInstallmentStatus Status { get; set; }

        public DateTime DueDate { get; set; }

        public int PayOrder { get; set; }

        [ForeignKey("LeaseAgreementId")]
        public int LeaseAgreementId { get; set; }
        public LeaseAgreement LeaseAgreement { get; set; }
    }
}

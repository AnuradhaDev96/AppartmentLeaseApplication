using AppartmentLeaseAPI.Models.LeaseManagement;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppartmentLeaseAPI.Models.Customers
{
    public class ChiefOccupant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }
        
        [Required]
        public string Address { get; set; }

        [Required]
        public string NICPassportNo { get; set; }
        
        [Required]
        public string Email { get; set; }

        [Required]
        public string EmergencyContactNo { get; set; }

        [ForeignKey("SystemUserId")]
        public int SystemUserId { get; set; }
        public UserModel SystemUser { get; set; }

        public ICollection<Dependant> Dependants { get; set; }

        public ICollection<LeaseAgreement> LeaseAgreements { get; set; }
    }
}

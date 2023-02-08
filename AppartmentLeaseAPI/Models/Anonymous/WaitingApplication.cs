using AppartmentLeaseAPI.Models.Apartments;
using AppartmentLeaseAPI.Models.LeaseManagement;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppartmentLeaseAPI.Models.Anonymous
{
    public class WaitingApplication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string TelephoneNo { get; set; }

        [Required]
        public string Email { get; set; }

        public DateTime RequiredStartDate { get; set; }

        public DateTime CreatedOn { get; set; }

        [ForeignKey("ApartmentClassId")]
        public int ApartmentClassId { get; set; }
        public ApartmentClass ApartmentClass { get; set; }

        public string RequiredBuildingLocation { get; set; }
    }
}

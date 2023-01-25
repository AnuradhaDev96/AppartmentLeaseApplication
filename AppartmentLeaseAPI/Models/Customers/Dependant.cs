using AppartmentLeaseAPI.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppartmentLeaseAPI.Models.Customers
{
    public class Dependant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public DependantRelationshipType Relationship { get; set; }

        [ForeignKey("ChiefOccupantId")]
        public int ChiefOccupantId { get; set; }
        public ChiefOccupant ChiefOccupant { get; set; }
    }
}

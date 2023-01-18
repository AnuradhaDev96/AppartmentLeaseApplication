using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppartmentLeaseAPI.Models.Apartments
{
    public class ApartmentClassFacility
    {
        [Key]
        public int Id { get; set; }
        // Id 0 is means the facility available for all the apartment classes

        [ForeignKey("ApartmentClassId")]
        public int ApartmentClassId { get; set; }
        public ApartmentClass ApartmentClass { get; set; }

        public string Name { get; set; }
    }
}

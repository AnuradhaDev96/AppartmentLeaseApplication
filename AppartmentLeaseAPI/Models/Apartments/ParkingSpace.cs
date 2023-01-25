using AppartmentLeaseAPI.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace AppartmentLeaseAPI.Models.Apartments
{
    public class ParkingSpace
    {
        [Key]
        public int Id { get; set; }

        public string LotNo { get; set; }

        public ParkingSpaceStatus Status { get; set; }
    }
}

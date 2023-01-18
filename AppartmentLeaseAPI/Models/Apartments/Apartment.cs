using AppartmentLeaseAPI.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseAPI.Models.Apartments
{
    public class Apartment
    {
        [Key]
        public int Id { get; set; }

        public ApartmentAvailabilityStatus Status { get; set; }

        [ForeignKey("BuildingId")]
        public int BuildingId { get; set; }
        public Building Building { get; set; }

        [ForeignKey("ApartmentClassId")]
        public int ApartmentClassId { get; set; }
        public ApartmentClass ApartmentClass { get; set; }

        [ForeignKey("ParkingSpaceId")]
        public int ParkingSpaceId { get; set; }
        public ParkingSpace ParkingSpace { get; set; }
    }
}

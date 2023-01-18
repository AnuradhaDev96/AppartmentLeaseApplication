using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseAPI.Models.Apartments
{
    public class ApartmentClass
    {
        [Key]
        public int Id { get; set; }

        public int AttachedBathroomCount { get; set; }

        public int RoomCount { get; set; }

        public int MaximumOccupantCount { get; set; }
        public double RefundableDepositAmount { get; set; }
        public double PricePerMonth { get; set; }

        public ICollection<ApartmentClassFacility> Facilities { get; set; }

        public ICollection<Apartment> Apartments { get; set; }

    }
}

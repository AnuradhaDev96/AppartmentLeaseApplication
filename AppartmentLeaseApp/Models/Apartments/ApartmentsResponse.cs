using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.Models.Apartments
{
    public class ApartmentsResponse
    {
        public int Id { get; set; }

        public string Status { get; set; }

        public string? BuildingName { get; set; }
        public string? BuildingLocation { get; set; }
        public string? ApartmentClassName { get; set; }
        public string? ReservedParkingSpace { get; set; }
    }
}

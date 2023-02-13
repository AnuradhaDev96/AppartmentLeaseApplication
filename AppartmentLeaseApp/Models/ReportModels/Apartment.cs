using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.Models.ReportModels
{
    public class Apartment
    {
        public int Id { get; set; }

        public string Status { get; set; }

        public int BuildingId { get; set; }

        public int ApartmentClassId { get; set; }

        public int ParkingSpaceId { get; set; }
    }
}

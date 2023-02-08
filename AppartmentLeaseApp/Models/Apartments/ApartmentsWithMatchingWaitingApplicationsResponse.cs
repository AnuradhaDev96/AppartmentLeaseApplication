using AppartmentLeaseApp.Models.AnonymousModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.Models.Apartments
{
    public class ApartmentsWithMatchingWaitingApplicationsResponse
    {
        public int Id { get; set; }

        public string Status { get; set; }

        public string? BuildingName { get; set; }
        public string? BuildingLocation { get; set; }
        public int? ApartmentClassId { get; set; }
        public string? ApartmentClassName { get; set; }
        public string? ReservedParkingSpace { get; set; }

        public int? MatchingWaitingApplicationCount { get; set; }
        public List<WaitingApplicationResponse>? MatchingWaitingApplicationsList { get; set; }
    }
}

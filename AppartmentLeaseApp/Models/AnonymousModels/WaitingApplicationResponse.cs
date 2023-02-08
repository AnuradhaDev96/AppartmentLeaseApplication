using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.Models.AnonymousModels
{
    public class WaitingApplicationResponse
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string TelephoneNo { get; set; }

        public string Email { get; set; }

        public DateTime RequiredStartDate { get; set; }

        public DateTime CreatedOn { get; set; }

        public int ApartmentClassId { get; set; }

        public string RequiredBuildingLocation { get; set; }
    }
}

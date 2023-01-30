using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.Models.Customers
{
    public class DependantResponseModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Relationship { get; set; }

        public int ChiefOccupantId { get; set; }
    }
}

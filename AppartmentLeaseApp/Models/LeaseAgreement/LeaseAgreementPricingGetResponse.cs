using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.Models.LeaseAgreement
{
    public class LeaseAgreementPricingGetResponse
    {
        public double PricePerMonth { get; set; }
        public double RefundableDepositAmount { get; set; }
        public double AdditionalParkingUnitPrice { get; set; }
        public int LeasePeriod { get; set; }
        public double TotalCost { get; set; }
    }
}

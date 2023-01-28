namespace AppartmentLeaseAPI.Dtos
{
    public class ApartmentPricingGetDto
    {
        public double PricePerMonth { get; set; }
        public double RefundableDepositAmount { get; set; }
        public double AdditionalParkingUnitPrice { get; set; }
        public int LeasePeriod { get; set; }
        public double TotalCost { get; set; }
    }
}

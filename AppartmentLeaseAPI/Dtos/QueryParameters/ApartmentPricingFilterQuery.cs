namespace AppartmentLeaseAPI.Dtos.QueryParameters
{
    public class ApartmentPricingFilterQuery
    {
        public int ApartmentId { get; set; }

        public int PurchaseParkingSpaceId { get; set; } = 0;

        public DateTime LeaseStartDate { get; set; }

        public DateTime LeaseEndDate { get; set; }
    }
}

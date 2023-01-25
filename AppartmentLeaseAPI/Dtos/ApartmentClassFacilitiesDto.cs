namespace AppartmentLeaseAPI.Dtos
{
    public class ApartmentClassFacilitiesDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AttachedBathroomCount { get; set; }

        public int BedCount { get; set; }

        public int MaximumOccupantCount { get; set; }
        public double RefundableDepositAmount { get; set; }
        public double PricePerMonth { get; set; }

        public List<string>? AvailableFacilites { get; set; }
    }
}

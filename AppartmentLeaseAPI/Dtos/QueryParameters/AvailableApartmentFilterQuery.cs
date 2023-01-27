namespace AppartmentLeaseAPI.Dtos.QueryParameters
{
    public class AvailableApartmentFilterQuery
    {
        public string Location { get; set; } = string.Empty;
        public string ApartmentType { get; set; } = string.Empty;
    }
}

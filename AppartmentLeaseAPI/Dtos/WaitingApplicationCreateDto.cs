namespace AppartmentLeaseAPI.Dtos
{
    public class WaitingApplicationCreateDto
    {
        public string FullName { get; set; }

        public string TelephoneNo { get; set; }

        public string Email { get; set; }

        public DateTime RequiredStartDate { get; set; }

        public int ApartmentClassId { get; set; }

        public string RequiredBuildingLocation { get; set; }

        public int ReservationInquiryId { get; set; }
    }
}

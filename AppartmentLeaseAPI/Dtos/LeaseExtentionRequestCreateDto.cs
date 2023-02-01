namespace AppartmentLeaseAPI.Dtos
{
    public class LeaseExtentionRequestCreateDto
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int LeaseAgreementId { get; set; }
    }
}

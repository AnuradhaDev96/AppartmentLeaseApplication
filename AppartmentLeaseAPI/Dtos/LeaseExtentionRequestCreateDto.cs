namespace AppartmentLeaseAPI.Dtos
{
    public class LeaseExtentionRequestCreateDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int LeaseAgreementId { get; set; }
    }
}

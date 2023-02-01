namespace AppartmentLeaseAPI.Dtos
{
    public class DependantCreateDto
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Relationship { get; set; }

        public int ChiefOccupantId { get; set; }
    }
}

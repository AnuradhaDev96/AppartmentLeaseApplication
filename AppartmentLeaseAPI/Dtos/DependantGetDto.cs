using AppartmentLeaseAPI.Data.Enums;

namespace AppartmentLeaseAPI.Dtos
{
    public class DependantGetDto
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public DependantRelationshipType Relationship { get; set; }

        public int ChiefOccupantId { get; set; }
    }
}

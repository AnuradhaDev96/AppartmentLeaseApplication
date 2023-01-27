using AppartmentLeaseAPI.Data.Enums;

namespace AppartmentLeaseAPI.Dtos
{
    public class ApartmentGetDto
    {
        public int Id { get; set; }

        public ApartmentAvailabilityStatus Status { get; set; }

        public string? BuildingName { get; set; }
        public string? BuildingLocation { get; set; }
        public string? ApartmentClassName { get; set; }
        public string? ReservedParkingSpace { get; set; }

    }
}

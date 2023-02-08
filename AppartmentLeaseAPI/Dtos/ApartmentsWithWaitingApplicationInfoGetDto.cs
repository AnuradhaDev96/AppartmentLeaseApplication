using AppartmentLeaseAPI.Data.Enums;
using AppartmentLeaseAPI.Models.Anonymous;

namespace AppartmentLeaseAPI.Dtos
{
    public class ApartmentsWithWaitingApplicationInfoGetDto : ApartmentGetDto
    {
        public ApartmentsWithWaitingApplicationInfoGetDto(ApartmentGetDto parentObject, int? matchingWaitingApplicationCount, List<WaitingApplication>? waitingApplicationsList)
        {
            this.Id = parentObject.Id;
            this.Status = parentObject.Status;
            this.ApartmentClassId = parentObject.ApartmentClassId;
            this.ApartmentClassName = parentObject.ApartmentClassName;
            this.ReservedParkingSpace = parentObject.ReservedParkingSpace;
            this.BuildingName = parentObject.BuildingName;
            this.BuildingLocation = parentObject.BuildingLocation;
            this.MatchingWaitingApplicationCount = matchingWaitingApplicationCount;
            this.MatchingWaitingApplicationsList = waitingApplicationsList;
        }

        public int? MatchingWaitingApplicationCount { get; set; }
        public List<WaitingApplication>? MatchingWaitingApplicationsList { get; set; }
    }
}

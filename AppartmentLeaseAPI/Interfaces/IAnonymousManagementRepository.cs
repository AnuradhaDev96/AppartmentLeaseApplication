using AppartmentLeaseAPI.Models.Anonymous;

namespace AppartmentLeaseAPI.Interfaces
{
    public interface IAnonymousManagementRepository
    {
        bool CreateReservationRequest(ReservationInquiry reservationInquiry);

        bool isPendingStatusInquiryExistForTelephoneNumber(string telephoneNumber);
    }
}

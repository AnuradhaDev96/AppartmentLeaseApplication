using AppartmentLeaseAPI.Models.Anonymous;

namespace AppartmentLeaseAPI.Interfaces
{
    public interface IAnonymousManagementRepository
    {
        bool CreateReservationRequest(ReservationInquiry reservationInquiry);

        bool IsPendingStatusInquiryExistForTelephoneNumber(string telephoneNumber);

        bool IsReservationInquiryExist(int inquiryId);

        bool UpdateReservationInquiryToLeaseCreated(int inquiryId, int leaseAgreementId);

        ICollection<ReservationInquiry>? GetReservationInquiries();
    }
}

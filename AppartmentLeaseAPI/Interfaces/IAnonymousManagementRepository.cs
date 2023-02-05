using AppartmentLeaseAPI.Models.Anonymous;

namespace AppartmentLeaseAPI.Interfaces
{
    public interface IAnonymousManagementRepository
    {
        bool CreateReservationRequest(ReservationInquiry reservationInquiry);

        Task<int?> CreateWaitingApplication(WaitingApplication waitingApplication);

        bool IsPendingStatusInquiryExistForTelephoneNumber(string telephoneNumber);

        bool IsReservationInquiryExist(int inquiryId);

        bool UpdateReservationInquiryToLeaseCreated(int inquiryId, int leaseAgreementId);

        bool UpdateReservationInquiryToWaitingListed(int inquiryId, int waitingApplicationId);

        ICollection<ReservationInquiry>? GetReservationInquiries();

        ReservationInquiry? GetReservationInquiryById(int inquiryId);
    }
}

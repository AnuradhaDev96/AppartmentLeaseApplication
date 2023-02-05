using AppartmentLeaseAPI.Data;
using AppartmentLeaseAPI.Data.Enums;
using AppartmentLeaseAPI.Interfaces;
using AppartmentLeaseAPI.Models.Anonymous;

namespace AppartmentLeaseAPI.Repositories
{
    public class AnonymousManagementRepository : IAnonymousManagementRepository
    {
        private readonly DataContext _dataContext;

        public AnonymousManagementRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CreateReservationRequest(ReservationInquiry reservationInquiry)
        {
            _dataContext.ReservationInquiries.Add(reservationInquiry);
            return Save();
        }

        public async Task<int?> CreateWaitingApplication(WaitingApplication waitingApplication)
        {
            var newWaitingApplication = await _dataContext.WaitingApplications.AddAsync(waitingApplication);
            await _dataContext.SaveChangesAsync();
            return newWaitingApplication.Entity.Id;
        }

        public ICollection<ReservationInquiry>? GetReservationInquiries()
        {
            var reservationInquiries = _dataContext.ReservationInquiries.ToList();
            return reservationInquiries;
        }

        public ReservationInquiry? GetReservationInquiryById(int inquiryId)
        {
            return _dataContext.ReservationInquiries.FirstOrDefault(r => r.Id == inquiryId);
        }

        public bool IsPendingStatusInquiryExistForTelephoneNumber(string telephoneNumber)
        {
            return _dataContext.ReservationInquiries.Any(
                rq => rq.TelephoneNo == telephoneNumber.Trim().Replace(" ", "") 
                && rq.Status == InquiryStatus.PendingResponse);
        }

        public bool IsReservationInquiryExist(int inquiryId)
        {
            return _dataContext.ReservationInquiries.Any(r => r.Id == inquiryId);
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReservationInquiryToLeaseCreated(int inquiryId, int leaseAgreementId)
        {
            var reservationInquiry = _dataContext.ReservationInquiries.Where(r => r.Id == inquiryId).FirstOrDefault();

            if (reservationInquiry == null)
                return false;

            reservationInquiry.Status = InquiryStatus.LeaseCreated;
            reservationInquiry.LeaseAgreementId = leaseAgreementId;

            _dataContext.ReservationInquiries.Update(reservationInquiry);

            return Save();

        }

        public bool UpdateReservationInquiryToWaitingListed(int inquiryId, int waitingApplicationId)
        {
            var reservationInquiry = _dataContext.ReservationInquiries.Where(r => r.Id == inquiryId).FirstOrDefault();

            if (reservationInquiry == null)
                return false;

            reservationInquiry.Status = InquiryStatus.WaitingList;
            reservationInquiry.WaitingApplicationId = waitingApplicationId;

            _dataContext.ReservationInquiries.Update(reservationInquiry);

            return Save();
        }
    }
}

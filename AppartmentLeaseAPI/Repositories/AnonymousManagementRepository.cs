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

        public ICollection<ReservationInquiry>? GetReservationInquiries()
        {
            var reservationInquiries = _dataContext.ReservationInquiries.ToList();
            return reservationInquiries;
        }

        public bool isPendingStatusInquiryExistForTelephoneNumber(string telephoneNumber)
        {
            return _dataContext.ReservationInquiries.Any(
                rq => rq.TelephoneNo == telephoneNumber.Trim().Replace(" ", "") 
                && rq.Status == InquiryStatus.PendingResponse);
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}

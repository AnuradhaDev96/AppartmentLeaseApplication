using AppartmentLeaseAPI.Data;
using AppartmentLeaseAPI.Dtos;
using AppartmentLeaseAPI.Interfaces;
using AppartmentLeaseAPI.Models.LeaseManagement;

namespace AppartmentLeaseAPI.Repositories
{
    public class LeaseAgreementManagementRepository : ILeaseAgreementManagementRepository
    {
        private readonly DataContext _context;

        public LeaseAgreementManagementRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<int?> CreateLeaseAgreement(LeaseAgreement leaseAgreement)
        {
            var newLeaseAgreement = await _context.LeaseAgreements.AddAsync(leaseAgreement);
            await _context.SaveChangesAsync();
            return newLeaseAgreement.Entity.Id;
        }

        public bool CreateLeaseExtentionRequestByLeaseAgreementId(LeaseExtentionRequest leaseExtentionRequest)
        {
            _context.LeaseExtentionRequests.Add(leaseExtentionRequest);
            return Save();
        }

        public bool DeleteLeaseExtentionRequest(int requestId)
        {
            var request = _context.LeaseExtentionRequests.FirstOrDefault(e => e.Id == requestId);
            if (request == null)
                return false;

            _context.LeaseExtentionRequests.Remove(request);
            return Save();
        }

        public LeaseAgreement? GetLeaseAgreementByAgreementId(int agreementId)
        {
            return _context.LeaseAgreements.Where(x => x.Id == agreementId).FirstOrDefault();
        }

        public ICollection<LeaseAgreement>? GetLeaseAgreementsByChiefOccupantId(int chiefOccupantId)
        {
            var leaseAgreements = _context.LeaseAgreements.Where(x => x.ChiefOccupantId == chiefOccupantId).ToList();
            return leaseAgreements;
        }

        public ICollection<LeaseExtentionRequest>? GetLeaseExtentionRequestsByLeaseeAgreementId(int leaseAgreementId)
        {
            return _context.LeaseExtentionRequests.Where(x => x.LeaseAgreementId == leaseAgreementId).ToList();
        }

        public bool IsLeaseExtentionRequestExist(int requestId)
        {
            return _context.LeaseExtentionRequests.Any(e => e.Id == requestId);
        }

        public bool UpdateLeaseExtentionRequest(LeaseExtentionRequest updateRequesData)
        {
            _context.LeaseExtentionRequests.Update(updateRequesData);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool ConfirmTermsOfApprovedLeaseExtention(LeaseExtentionRequest existingLeaseExtention)
        {
            var leaseAgreement = _context.LeaseAgreements.Where(l => l.Id == existingLeaseExtention.LeaseAgreementId).FirstOrDefault();
            if (leaseAgreement == null) return false;

            // Update lease extention status to Terms confirmed
            existingLeaseExtention.Status = Data.Enums.LeaseExtentionRequestStatus.TermsConfirmed;
            _context.LeaseExtentionRequests.Update(existingLeaseExtention);

            // Update lease agreement status to extended
            leaseAgreement.Status = Data.Enums.LeaseAgreementStatus.Extended;
            _context.LeaseAgreements.Update(leaseAgreement);

            // Get the apartment class information
            var apartment = _context.Apartments.Where(ap => ap.Id == leaseAgreement.ApartmentId).FirstOrDefault();
            var classOfApartment = _context.ApartmentClasses.Where(c => c.Id == apartment.ApartmentClassId).FirstOrDefault();

            // calculate total cost for new lease agreement
            var extendedLeasePeriodInDays = (existingLeaseExtention.EndDate - existingLeaseExtention.StartDate).Days;
            int extendedLeasePeriodInMonths = extendedLeasePeriodInDays / 30;
            var costForExtendedrLeasePeriod = extendedLeasePeriodInMonths * classOfApartment.PricePerMonth;

            if (leaseAgreement.PurchasedParkingSpaceId != null || leaseAgreement.PurchasedParkingSpaceId > 0)
                costForExtendedrLeasePeriod += 5000.00 * extendedLeasePeriodInMonths;

            var freshLeaseAgreement = new LeaseAgreement
            {
                Status = Data.Enums.LeaseAgreementStatus.Fresh,
                StartDate = existingLeaseExtention.StartDate,
                EndtDate = existingLeaseExtention.EndDate,
                IsRefundableDepositPaid = true,
                IsMonthAdvancePaid = true,
                TotalCost = costForExtendedrLeasePeriod,
                PurchasedParkingSpaceId = leaseAgreement.PurchasedParkingSpaceId,
                ApartmentId = leaseAgreement.ApartmentId,
                ChiefOccupantId = leaseAgreement.ChiefOccupantId,
            };
            
            _context.LeaseAgreements.Add(freshLeaseAgreement);
            return Save();
        }

        public LeaseExtentionRequest? GetLeaseExtentionRequestById(int requestId)
        {
            return _context.LeaseExtentionRequests.Where(x => x.Id == requestId).FirstOrDefault();
        }
    }
}

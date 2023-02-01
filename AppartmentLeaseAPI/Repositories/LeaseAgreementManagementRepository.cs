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
    }
}

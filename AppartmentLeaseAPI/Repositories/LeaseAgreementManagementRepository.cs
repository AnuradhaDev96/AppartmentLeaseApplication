﻿using AppartmentLeaseAPI.Data;
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
    }
}
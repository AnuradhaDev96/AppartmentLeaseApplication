using AppartmentLeaseAPI.Data;
using AppartmentLeaseAPI.Interfaces;
using AppartmentLeaseAPI.Models.Customers;

namespace AppartmentLeaseAPI.Repositories
{
    public class CustomerManagementRepository : ICustomerManagementRepository
    {
        private readonly DataContext _context;

        public CustomerManagementRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<int?> CreateChiefOccupant(ChiefOccupant chiefOccupant)
        {
            var newChiefOccupant = await _context.ChiefOccupants.AddAsync(chiefOccupant);
            await _context.SaveChangesAsync();
            return newChiefOccupant.Entity.Id;
        }
    }
}

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

        public bool CreateDependantByChiefOccupantId(Dependant dependant)
        {
            _context.Dependants.Add(dependant);
            return Save();
        }

        public ChiefOccupant? GetChiefOccupantBySystemUserId(int systemUserId)
        {
            return _context.ChiefOccupants.FirstOrDefault(c => c.SystemUserId == systemUserId);
        }

        public ICollection<Dependant>? GetDependantsByChiefOccupantId(int chiefOccupantId)
        {
            return _context.Dependants.Where(d => d.ChiefOccupantId == chiefOccupantId).ToList();
        }

        public bool IsDependantExistByDependantId(int dependantId)
        {
            return _context.Dependants.Any(d => d.Id == dependantId);
        }

        public bool UpdateDependant(Dependant dependant)
        {
            _context.Dependants.Update(dependant);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool DeleteDependant(int dependantId)
        {
            var dependant = _context.Dependants.FirstOrDefault(d => d.Id == dependantId);
            _context.Dependants.Remove(dependant);
            return Save();
        }
    }
}

using AppartmentLeaseAPI.Models.Customers;

namespace AppartmentLeaseAPI.Interfaces
{
    public interface ICustomerManagementRepository
    {
        Task<int?> CreateChiefOccupant(ChiefOccupant chiefOccupant);

        ChiefOccupant? GetChiefOccupantBySystemUserId(int systemUserId);

        ICollection<Dependant>? GetDependantsByChiefOccupantId(int chiefOccupantId);
    }
}

using AppartmentLeaseAPI.Models.Customers;

namespace AppartmentLeaseAPI.Interfaces
{
    public interface ICustomerManagementRepository
    {
        Task<int?> CreateChiefOccupant(ChiefOccupant chiefOccupant);
    }
}

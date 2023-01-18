using AppartmentLeaseAPI.Models;

namespace AppartmentLeaseAPI.Interfaces
{
    public interface IUserManagementRepository
    {
        ICollection<UserModel> GetSystemUsers();

        UserModel? GetUserByEmail(string email);

        UserModel? GetUserByCredentials(string username, string password);
    }
}

using AppartmentLeaseAPI.Data;
using AppartmentLeaseAPI.Interfaces;
using AppartmentLeaseAPI.Models;

namespace AppartmentLeaseAPI.Repositories
{
    public class UserManagementRepository : IUserManagementRepository
    {
        private readonly DataContext _context;

        public UserManagementRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<UserModel> GetSystemUsers()
        {
            return _context.SystemUsers.OrderBy(x => x.Id).ToList();
        }

        public UserModel? GetUserByCredentials(string username, string password)
        {
            return _context.SystemUsers.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
        }

        public UserModel? GetUserByEmail(string email)
        {
            return _context.SystemUsers.Where(x => x.Email == email).FirstOrDefault();
        }
    }
}

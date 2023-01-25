using AppartmentLeaseAPI.Data.Enums;

namespace AppartmentLeaseAPI.Models
{
    public class UserConstants
    {
        public static List<UserModel> UsersList = new()
        {
            new UserModel()
            {
                Id = 1,
                Username = "john",
                Password = "j1234",
                Role = UserRole.Manager,
                Email = "john@mailinator.com"
            },
            new UserModel()
            {
                Id = 2,
                Username = "harper",
                Password = "h1234",
                Role = UserRole.Clerk,
                Email = "harp@mailinator.com"
            },
        };
    }
}

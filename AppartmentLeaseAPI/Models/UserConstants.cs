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
                Role = "Admin",
                Email = "john@mailinator.com"
            },
            new UserModel()
            {
                Id = 2,
                Username = "harper",
                Password = "h1234",
                Role = "Clerk",
                Email = "harp@mailinator.com"
            },
        };
    }
}

namespace AppartmentLeaseApp.Interfaces
{
    public interface ILoggedInUser
    {
        string Email { get; set; }
        int Id { get; set; }
        string Password { get; set; }
        string Role { get; set; }
        string Token { get; set; }
        string Username { get; set; }
    }
}
using AppartmentLeaseAPI.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace AppartmentLeaseAPI.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }

    }
}

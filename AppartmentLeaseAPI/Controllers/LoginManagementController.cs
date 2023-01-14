using AppartmentLeaseAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppartmentLeaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginManagementController : ControllerBase
    {
        private IConfiguration _configuration;

        public LoginManagementController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("PasswordLogin")]
        public IActionResult PasswordLogin([FromBody] LoginModel data)
        {
            var user = Authenticate(data);

            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }
            return NotFound("User not found");
        }

        private string Generate(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.UserData, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel? Authenticate(LoginModel data)
        {
            var currentUser = UserConstants.UsersList.FirstOrDefault(x => x.Username == data.Username && x.Password == data.Password);
            return currentUser;
        }


        // Sample authorized request        
        [HttpGet("GetUserByEmail/{email}")]
        [Authorize]
        public IActionResult GetUserByEmail(string email)
        {
            var user = UserConstants.UsersList.FirstOrDefault(x => x.Email == email);        

            if (user != null)
            {
                return Ok(user);
            }
            return NotFound(@$"User not found for email {email}");
        }
    }
}

using AppartmentLeaseAPI.Data.Enums;
using AppartmentLeaseAPI.Interfaces;
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
        private readonly IUserManagementRepository _userManagementRespository;

        public LoginManagementController(IConfiguration configuration, 
            IUserManagementRepository userManagementRespository)
        {
            _configuration = configuration;
            _userManagementRespository = userManagementRespository;
        }

        [AllowAnonymous]
        [HttpPost("PasswordLogin")]
        public IActionResult PasswordLogin([FromBody] LoginModel data)
        {
            if (data == null)
                return BadRequest(ModelState);

            try
            {
                var user = Authenticate(data);

                if (user != null)
                {
                    var token = Generate(user);
                    return Ok(token);
                }
                return NotFound("User not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
                new Claim(ClaimTypes.Role, Enum.GetName(typeof(UserRole), user.Role)!),
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
            //var currentUser = UserConstants.UsersList.FirstOrDefault(x => x.Username == data.Username && x.Password == data.Password);
            var currentUser = _userManagementRespository.GetUserByCredentials(data.Username, data.Password);
            return currentUser;
        }
    }
}

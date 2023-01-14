using AppartmentLeaseAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppartmentLeaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        [Authorize]
        [HttpGet("GetUserByEmail/{email}")]        
        public IActionResult GetUserByEmail(string email)
        {
            var user = UserConstants.UsersList.FirstOrDefault(x => x.Email == email);

            if (user != null)
            {
                return Ok(user);
            }
            return NotFound(@$"User not found for email {email}");
        }
        
        [Authorize]
        [HttpGet("GetCurrentUser")]
        [ProducesResponseType(200, Type = (typeof(UserModel)))]
        [ProducesResponseType(404)]
        public IActionResult GetUserWithClaims()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                var userWithClaims = new UserModel
                {
                    Id = Convert.ToInt32(userClaims.FirstOrDefault(x => x.Type == ClaimTypes.UserData)?.Value ?? "0"),
                    Username = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? "",
                    Email = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? "",
                    Role = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value ?? "",
                };

                return Ok(userWithClaims);
            }

            return NotFound("User not found");
        }
    }
}

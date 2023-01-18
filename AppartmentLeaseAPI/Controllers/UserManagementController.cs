using AppartmentLeaseAPI.Dtos;
using AppartmentLeaseAPI.Interfaces;
using AppartmentLeaseAPI.Models;
using AutoMapper;
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
        private readonly IUserManagementRepository _userManagementRespository;
        private readonly IMapper _mapper;

        public UserManagementController(IUserManagementRepository userManagementRespository, IMapper mapper)
        {
            _userManagementRespository = userManagementRespository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("GetUserByEmail/{email}")]
        [ProducesResponseType(200, Type = (typeof(UserGetDto)))]
        public IActionResult GetUserByEmail(string email)
        {
            try
            {
                var userResult = _userManagementRespository.GetUserByEmail(email);
                var user = _mapper.Map<UserGetDto>(userResult);

                if (user != null)
                {
                    return Ok(user);
                }
                return NotFound(@$"User not found for email {email}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [Authorize]
        [HttpGet("GetUsers")]
        [ProducesResponseType(200, Type = (typeof(IEnumerable<UserGetDto>)))]
        public IActionResult GetUsers()
        {
            var users = _userManagementRespository.GetSystemUsers();
            var userGetDtos = _mapper.Map<ICollection<UserGetDto>>(users);

            return Ok(userGetDtos);
        }
        
        [Authorize]
        [HttpGet("GetCurrentUser")]
        [ProducesResponseType(200, Type = (typeof(UserGetDto)))]
        [ProducesResponseType(404)]
        public IActionResult GetUserWithClaims()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                var userWithClaims = new UserGetDto
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

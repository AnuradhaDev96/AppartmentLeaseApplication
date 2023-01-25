using AppartmentLeaseAPI.Dtos;
using AppartmentLeaseAPI.Interfaces;
using AppartmentLeaseAPI.Models.Apartments;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppartmentLeaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentManagementController : ControllerBase
    {
        private readonly IApartmentManagementRepository _apartmentManagementRepository;
        private readonly IMapper _mapper;

        public ApartmentManagementController(IApartmentManagementRepository apartmentManagementRepository, IMapper mapper)
        {
            _apartmentManagementRepository = apartmentManagementRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("ApartmentClasses")]
        [ProducesResponseType(200, Type = (typeof(IEnumerable<ApartmentClassFacilitiesDto>)))]
        public IActionResult GetApartmentClasses()
        {
            try
            {
                var result = _apartmentManagementRepository.GetApartmentClasses();
                //var user = _mapper.Map<List<ApartmentClassFacilitiesDto>>(result);

                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

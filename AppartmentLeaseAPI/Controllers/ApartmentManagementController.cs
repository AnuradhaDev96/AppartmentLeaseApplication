using AppartmentLeaseAPI.Dtos;
using AppartmentLeaseAPI.Dtos.QueryParameters;
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

        /// <summary>
        /// Initially returns the Apartments in Available, Maintenance, and Occupied status
        /// If in Occupied status special query should be executed
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("Apartments/AvailableApartments")]
        [ProducesResponseType(200, Type = (typeof(IEnumerable<ApartmentGetDto>)))]
        [ProducesResponseType(400)]
        public IActionResult GetAvailableApartments()
        {
            try
            {
                var result = _apartmentManagementRepository.GetAvailableApartments();

                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Authorize]
        [HttpGet("ParkingSpaces/AvailableParkingSpaces")]
        [ProducesResponseType(200, Type = (typeof(IEnumerable<ParkingSpace>)))]
        [ProducesResponseType(400)]
        public IActionResult GetAvailableParkingSpaces()
        {
            try
            {
                var result = _apartmentManagementRepository.GetAvailableParkingSpaces();

                if (!ModelState.IsValid)
                    return BadRequest();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("Apartments/AvailableApartments/Filter")]
        [ProducesResponseType(200, Type = (typeof(IEnumerable<ApartmentGetDto>)))]
        [ProducesResponseType(400)]
        public IActionResult GetAvailableApartments([FromQuery] AvailableApartmentFilterQuery query)
        {
            try
            {
                var result = _apartmentManagementRepository.FilterAvailableApartments(location: query.Location, apartmentType: query.ApartmentType);

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

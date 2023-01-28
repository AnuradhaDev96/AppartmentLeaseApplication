using AppartmentLeaseAPI.Dtos;
using AppartmentLeaseAPI.Interfaces;
using AppartmentLeaseAPI.Models.Anonymous;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppartmentLeaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnonymousManagementController : ControllerBase
    {
        private readonly IAnonymousManagementRepository _anonymousManagementRepository;
        private readonly IMapper _mapper;

        public AnonymousManagementController(IAnonymousManagementRepository anonymousManagementRepository, IMapper mapper)
        {
            _anonymousManagementRepository = anonymousManagementRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("ReservationRequests")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateReservationRequestRecord([FromBody] ReservationInquiryCreateDto data)
        {
            if (data == null)
                return BadRequest(ModelState);

            if (_anonymousManagementRepository.IsPendingStatusInquiryExistForTelephoneNumber(data.TelephoneNo))
            {
                return Ok("Request already exists. Our agents will contact you soon.");
            }

            var createData = _mapper.Map<ReservationInquiry>(data);
            createData.TelephoneNo = createData.TelephoneNo.Trim().Replace(" ", "");
            createData.CreatedOn = DateTime.Now;
            createData.Status = Data.Enums.InquiryStatus.PendingResponse;

            if (!_anonymousManagementRepository.CreateReservationRequest(createData))
            {
                return Ok("Please try again.");
            }

            return Ok("Request created. Our agents will contact you soon.");
        }

        [Authorize]
        [HttpGet("ReservationRequests")]
        [ProducesResponseType(200, Type = (typeof(IEnumerable<ReservationInquiry>)))]
        public IActionResult GetApartmentClasses()
        {
            try
            {
                var result = _anonymousManagementRepository.GetReservationInquiries();
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

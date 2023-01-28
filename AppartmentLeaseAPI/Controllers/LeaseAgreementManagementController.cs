using AppartmentLeaseAPI.Dtos;
using AppartmentLeaseAPI.Interfaces;
using AppartmentLeaseAPI.Models;
using AppartmentLeaseAPI.Models.Anonymous;
using AppartmentLeaseAPI.Models.Customers;
using AppartmentLeaseAPI.Models.LeaseManagement;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppartmentLeaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaseAgreementManagementController : ControllerBase
    {
        private readonly IUserManagementRepository _userManagementRepository;
        private readonly ICustomerManagementRepository _customerManagementRepository;
        private readonly ILeaseAgreementManagementRepository _leaseAgreementManagementRepository;
        private readonly IAnonymousManagementRepository _anonymousManagementRepository;
        private readonly IApartmentManagementRepository _apartmentManagementRepository;
        private readonly IMapper _mapper;

        public LeaseAgreementManagementController(ILeaseAgreementManagementRepository leaseAgreementManagementRepository, IMapper mapper,
            IAnonymousManagementRepository anonymousManagementRepository, IUserManagementRepository userManagementRepository,
            ICustomerManagementRepository customerManagementRepository, IApartmentManagementRepository apartmentManagementRepository)
        {
            _userManagementRepository = userManagementRepository;
            _customerManagementRepository = customerManagementRepository;
            _leaseAgreementManagementRepository = leaseAgreementManagementRepository;
            _anonymousManagementRepository = anonymousManagementRepository;
            _apartmentManagementRepository = apartmentManagementRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("LeaseAgreements")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateReservationRequestRecord([FromBody] LeaseAgreementCreateDto data)
        {
            if (data == null)
                return BadRequest(ModelState);

            if (!_anonymousManagementRepository.IsReservationInquiryExist(data.ReservationInquiryId))
            {
                return NotFound(@$"Reservation Inquiry does not exist for id {data.ReservationInquiryId}");
            }

            #region Create System User
            var newUser = new UserModel
            {
                Username = data.Username,
                Password = data.Password,
                Email = data.Email,
                Role = Data.Enums.UserRole.ChiefOccupant
            };
            var createdUserId = await _userManagementRepository.CreateSystemUser(newUser);

            if (createdUserId == null)
                return BadRequest("New User cannot be created");

            #endregion

            #region Create ChiefOccupant
            var newChiefOccupant = new ChiefOccupant
            {
                FullName = data.FullName,
                Address = data.Address,
                Email = data.Email,
                NICPassportNo = data.NICPassportNo,
                EmergencyContactNo = data.EmergencyContactNo,
                SystemUserId = createdUserId ?? -1
            };
            var createdChiefOccupantId = await _customerManagementRepository.CreateChiefOccupant(newChiefOccupant);

            if (createdChiefOccupantId == null)
            {
                return BadRequest("New chief occupant cannot be created");
            }
            #endregion

            #region Calculate TotalCost
            var selectedApartmentClass = _apartmentManagementRepository.GetApartmentClassDetails(apartmentId: data.ApartmentId);

            if (selectedApartmentClass == null)
            {
                return NotFound("Invalid apartment class");
            }

            var leasePeriodInDays = (data.EndtDate - data.StartDate).Days;
            int leasePeriodInMonths = leasePeriodInDays / 30;
            var refundableAmount = selectedApartmentClass.RefundableDepositAmount;
            var costForLeasePeriod = leasePeriodInMonths * selectedApartmentClass.PricePerMonth;

            if (data.PurchasedParkingSpaceId != null || data.PurchasedParkingSpaceId > 0)
                costForLeasePeriod += 5000.00 * leasePeriodInMonths;
            #endregion

            #region Create Lease Agreement
            var newLeaseAgreement = new LeaseAgreement
            {
                Status = Data.Enums.LeaseAgreementStatus.New,
                IsMonthAdvancePaid = false,
                IsRefundableDepositPaid = false,
                TotalCost = refundableAmount + costForLeasePeriod,
                ApartmentId = data.ApartmentId,
                ChiefOccupantId = createdChiefOccupantId ?? -1,
                StartDate = data.StartDate,
                EndtDate = data.EndtDate,
                PurchasedParkingSpaceId = data.PurchasedParkingSpaceId,
            };
            

            var createdLeaseAgreementId = await _leaseAgreementManagementRepository.CreateLeaseAgreement(newLeaseAgreement);
            if (createdLeaseAgreementId == null)
            {
                return BadRequest("Lease agreement cannot be created");
            }
            #endregion

            //Update Apartment status to Occupied
            if (!_apartmentManagementRepository.UpdateApartmentStatus(apartmentId: data.ApartmentId, statusToUpdate: Data.Enums.ApartmentAvailabilityStatus.Occupied))
            {
                return BadRequest("Apartment status cannot be updated to Occupied");
            }

            //Update Selected Parking space status to Purchased
            if (data.PurchasedParkingSpaceId != null || data.PurchasedParkingSpaceId > 0)
            {
                if (!_apartmentManagementRepository.UpdateParkingSpaceStatus(parkingSpaceId: data.PurchasedParkingSpaceId ?? -1, statusToUpdate: Data.Enums.ParkingSpaceStatus.Purchased))
                {
                    return BadRequest("Apartment status cannot be updated to Occupied");
                }
            }                

            // Update reservation inquiry
            if (!_anonymousManagementRepository.UpdateReservationInquiryToLeaseCreated(inquiryId: data.ReservationInquiryId, leaseAgreementId: createdLeaseAgreementId ?? -1))
            {
                return BadRequest("Inquiry status cannot be updated");
            }

            return Ok(@$"Lease Agreement created successfully with id {createdLeaseAgreementId}");
        }        
    }
}

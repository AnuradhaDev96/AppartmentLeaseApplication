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
            if (createdLeaseAgreementId == null || createdLeaseAgreementId <= 0)
            {
                return BadRequest("Lease agreement cannot be created");
            }
            #endregion

            //Update Apartment status to Reserved
            if (!_apartmentManagementRepository.UpdateApartmentStatus(apartmentId: data.ApartmentId, statusToUpdate: Data.Enums.ApartmentAvailabilityStatus.Reserved))
            {
                return BadRequest("Apartment status cannot be updated to Reserved");
            }

            //Update Selected Parking space status to Purchased
            if (data.PurchasedParkingSpaceId != null || data.PurchasedParkingSpaceId > 0)
            {
                if (!_apartmentManagementRepository.UpdateParkingSpaceStatus(parkingSpaceId: data.PurchasedParkingSpaceId ?? -1, statusToUpdate: Data.Enums.ParkingSpaceStatus.Purchased))
                {
                    return BadRequest("Parrking space status cannot be updated to Purchased");
                }
            }                

            // Update reservation inquiry
            if (!_anonymousManagementRepository.UpdateReservationInquiryToLeaseCreated(inquiryId: data.ReservationInquiryId, leaseAgreementId: createdLeaseAgreementId ?? -1))
            {
                return BadRequest("Inquiry status cannot be updated");
            }

            return Ok(@$"Lease Agreement created successfully with id {createdLeaseAgreementId}");
        }

        [Authorize]
        [HttpGet("LeaseAgreements/User/{userId}")]
        [ProducesResponseType(200, Type = (typeof(IEnumerable<LeaseAgreementSummaryGetDto>)))]
        public IActionResult GetLeaseAgreementsByChiefOccupant(int userId)
        {
            try
            {
                var summaryList = new List<LeaseAgreementSummaryGetDto>();

                var chiefOccupant = _customerManagementRepository.GetChiefOccupantBySystemUserId(userId);

                if (chiefOccupant == null)
                    return Ok(summaryList);

                var leaseAgreements = _leaseAgreementManagementRepository.GetLeaseAgreementsByChiefOccupantId(chiefOccupant.Id);                

                if (leaseAgreements == null)
                {
                    return Ok(summaryList);
                }

                foreach (var leaseAgreement in leaseAgreements)
                {
                    var assignedApartment = _apartmentManagementRepository.GetApartment(leaseAgreement.ApartmentId);
                    
                    string downPaymentStatus = "Payment Done";
                    if (!leaseAgreement.IsMonthAdvancePaid || !leaseAgreement.IsRefundableDepositPaid)
                    {
                        downPaymentStatus = "Pending Payment";
                    }

                    var summary = new LeaseAgreementSummaryGetDto
                    {
                        AgreementId = leaseAgreement.Id,
                        Status = leaseAgreement.Status,
                        DownPaymentStatus = downPaymentStatus,
                        LeaseEndDate = leaseAgreement.EndtDate,
                        LeaseStartDate = leaseAgreement.StartDate,
                        ApartmentId = leaseAgreement.ApartmentId,
                        ApartmentClassName = assignedApartment.ApartmentClass.Name,
                        BuildingName = assignedApartment.Building.Name,
                        BuildingLocation = assignedApartment.Building.Location,
                        MaximumOccupantCount = assignedApartment.ApartmentClass.MaximumOccupantCount
                    };

                    summaryList.Add(summary);

                }
                
                return Ok(summaryList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("LeaseAgreements/User/{userId}/Dependants/{activeLeaseAgreementId}")]
        [ProducesResponseType(200, Type = (typeof(IEnumerable<DependantGetDto>)))]
        public IActionResult GetDependantsByChiefOccupant(int userId, int activeLeaseAgreementId)
        {
            try
            {
                var dependantList = new List<DependantGetDto>();
                #region Lease agreement access validation
                var selectedLeaseAgreement = _leaseAgreementManagementRepository.GetLeaseAgreementByAgreementId(activeLeaseAgreementId);

                // return empty list of status of agreement is Extended or Ended. 
                // Because dependants can be edited only if the agreement is New or Started
                if (selectedLeaseAgreement == null || 
                    selectedLeaseAgreement.Status == Data.Enums.LeaseAgreementStatus.Extended || 
                    selectedLeaseAgreement.Status == Data.Enums.LeaseAgreementStatus.Ended)
                {
                    return Ok(dependantList);
                }
                #endregion

                var chiefOccupant = _customerManagementRepository.GetChiefOccupantBySystemUserId(userId);

                if (chiefOccupant == null)
                    return Ok(dependantList);

                var dependants = _customerManagementRepository.GetDependantsByChiefOccupantId(chiefOccupant.Id);
                var mappedDependants = _mapper.Map<List<DependantGetDto>>(dependants);

                return Ok(mappedDependants);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("LeaseAgreements/{leaseAgreementId}/User/{userId}/Dependants")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult AddDependantByChiefOccupantUserId(int leaseAgreementId, int userId, [FromBody] DependantCreateDto dependantCreateDto)
        {
            try 
            {
                var chiefOccupant = _customerManagementRepository.GetChiefOccupantBySystemUserId(userId);
                if (chiefOccupant == null)
                    return NotFound("Chief occupant does not exist for this user.");

                #region Maximum occupant count validation

                var selectedLeaseAgreement = _leaseAgreementManagementRepository.GetLeaseAgreementByAgreementId(leaseAgreementId);

                if (selectedLeaseAgreement == null || selectedLeaseAgreement.Status == Data.Enums.LeaseAgreementStatus.Extended ||
                    selectedLeaseAgreement.Status == Data.Enums.LeaseAgreementStatus.Ended)
                    return NotFound("You are not allowed to edit dependants under this lease agreement");

                var assignedApartment = _apartmentManagementRepository.GetApartment(selectedLeaseAgreement.ApartmentId);

                if (assignedApartment == null)
                    return NotFound("Apartment does not exist for this lease agreement");

                var currentDependants = _customerManagementRepository.GetDependantsByChiefOccupantId(chiefOccupant.Id);
                // Add 1 because chief occupant is also included
                int currentSavedOccupantCount = currentDependants == null ? 1 : currentDependants.Count + 1;
                if (currentSavedOccupantCount >= assignedApartment.ApartmentClass.MaximumOccupantCount)
                    return NotFound("Maximum Occupant count exceeds. You can't add more dependants.");
                #endregion

                var createData = _mapper.Map<Dependant>(dependantCreateDto);
                createData.ChiefOccupantId = chiefOccupant.Id;

                if (!_customerManagementRepository.CreateDependantByChiefOccupantId(createData))
                {
                    return NotFound("Error occured while saving");
                }

                return Ok("Dependant added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("LeaseAgreements/User/{userId}/Dependants")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult UpdateDependant(int userId, [FromBody] DependantCreateDto dependantUpdateData)
        {
            try
            {
                var chiefOccupant = _customerManagementRepository.GetChiefOccupantBySystemUserId(userId);
                if (chiefOccupant == null)
                    return NotFound("Chief occupant does not exist for this user.");

                if (!_customerManagementRepository.IsDependantExistByDependantId(dependantId: dependantUpdateData.Id))
                {
                    return NotFound("Dependant does not exist.");
                }

                var updateData = _mapper.Map<Dependant>(dependantUpdateData);

                if (!_customerManagementRepository.UpdateDependant(updateData))
                {
                    return NotFound("Dependant update failed.");
                }

                return Ok("Dependant updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("LeaseAgreements/User/{userId}/Dependants/{dependantId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult DeleteDependant(int userId, int dependantId)
        {
            try
            {
                var chiefOccupant = _customerManagementRepository.GetChiefOccupantBySystemUserId(userId);
                if (chiefOccupant == null)
                    return NotFound("Chief occupant does not exist for this user.");

                if (!_customerManagementRepository.IsDependantExistByDependantId(dependantId: dependantId))
                {
                    return NotFound("Dependant does not exist.");
                }

                if (!_customerManagementRepository.DeleteDependant(dependantId))
                {
                    return NotFound("Dependant delete failed.");
                }

                return Ok("Dependant deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("LeaseAgreements/{leaseAgreementId}/LeaseExtentionRequests")]
        [ProducesResponseType(200, Type = (typeof(IEnumerable<LeaseExtentionRequest>)))]
        public IActionResult GetLeaseExtentionRequestsByLeaseAgreementId(int leaseAgreementId)
        {
            try
            {
                var requests = _leaseAgreementManagementRepository.GetLeaseExtentionRequestsByLeaseeAgreementId(leaseAgreementId);
                
                return Ok(requests);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("LeaseAgreements/LeaseExtentionRequests")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult CreateLeaseExtentionRequestByLeaseAgreementId([FromBody] LeaseExtentionRequestCreateDto data)
        {
            try
            {
                var leaseAgreement = _leaseAgreementManagementRepository.GetLeaseAgreementByAgreementId(data.LeaseAgreementId);
                if (leaseAgreement == null)
                    return NotFound("Lease agreement not found");

                if (leaseAgreement.Status != Data.Enums.LeaseAgreementStatus.Started)
                {
                    return NotFound("Lease agreement is not Started yet");
                }

                // If Started check whether difference between lease agreement End date and current date
                // is greater than 60 days.
                var dateDifferenceToOriginalEndDateFromNow = (leaseAgreement.EndtDate - DateTime.Now).Days;

                if (dateDifferenceToOriginalEndDateFromNow < 60)
                {
                    return NotFound("Sorry! You should request 2 months before the end date");
                }

                // Check the new start date in request is ahead the end date in lease agreement
                // < 0 -> d1 earlier than d2
                // == 0 -> same date
                // > 0 => d1 is later than d2
                var isNewStartDateIsAfterOriginalEndDate = DateTime.Compare(leaseAgreement.EndtDate.Date, data.StartDate.Date) > 0;
                if (isNewStartDateIsAfterOriginalEndDate)
                    return NotFound("Sorry! New Start Date is earlier than End Date of Lease Agreement. Please select later date.");

                var createData = _mapper.Map<LeaseExtentionRequest>(data);
                createData.Status = Data.Enums.LeaseExtentionRequestStatus.Pending;

                if (!_leaseAgreementManagementRepository.CreateLeaseExtentionRequestByLeaseAgreementId(createData))
                {
                    return NotFound("Lease extention cannot be created");
                }

                return Ok("Lease extention request created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("LeaseAgreements/LeaseExtentionRequests")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult UpdateLeaseExtentionRequest([FromBody] LeaseExtentionRequest data)
        {
            try
            {
                if (!_leaseAgreementManagementRepository.IsLeaseExtentionRequestExist(data.Id))
                {
                    return NotFound("Lease extention is not found");
                }

                if (!_leaseAgreementManagementRepository.UpdateLeaseExtentionRequest(data))
                {
                    return NotFound("Lease extention cannot be edited");
                }

                return Ok("Lease extention updated sucessfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("LeaseAgreements/LeaseExtentionRequests/ConfirmTerms/{requestId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult ConfirmTermsOfApprovedLeaseExtentions(int requestId)
        {
            try
            {
                if (!_leaseAgreementManagementRepository.IsLeaseExtentionRequestExist(requestId))
                {
                    return NotFound("Lease extention is not found");
                }

                var leaseExtentionDbValue = _leaseAgreementManagementRepository.GetLeaseExtentionRequestById(requestId);

                if (leaseExtentionDbValue!.Status != Data.Enums.LeaseExtentionRequestStatus.Approved)
                {
                    return NotFound("Sorry! Only approved lease extention requests can be confirmed.");
                }

                if (!_leaseAgreementManagementRepository.ConfirmTermsOfApprovedLeaseExtention(leaseExtentionDbValue))
                {
                    return NotFound("Lease extention cannot be confirmed");
                }

                return Ok("Lease extention confirmed and Fresh Lease agreement created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

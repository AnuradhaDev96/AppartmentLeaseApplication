namespace AppartmentLeaseAPI.Dtos
{
    public class LeaseAgreementCreateDto
    {        
        /// <summary>
        /// Update the ReservationInquiry table status to LeaseCreated
        /// Update the ForeignKey with this id
        /// </summary>
        public int ReservationInquiryId { get; set; }

        #region LeaseAgreementTable
        public DateTime StartDate { get; set; }

        public DateTime EndtDate { get; set; }

        public int? PurchasedParkingSpaceId { get; set; }

        public int ApartmentId { get; set; }
        #endregion

        #region ChiefOccupant Details
        public string FullName { get; set; }

        public string Address { get; set; }

        public string NICPassportNo { get; set; }

        public string EmergencyContactNo { get; set; }
        #endregion

        #region  Sysem User Conversion
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        #endregion

    }
}

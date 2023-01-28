using AppartmentLeaseApp.Models.AnonymousModels;
using AppartmentLeaseApp.Models.Apartments;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.ViewModels
{
    public class CreateLeaseAgreementForCustomerViewModel : Screen
    {
        private ReservationRequestResponse _selectedRequiry;
        private ParkingSpaceResponse? _selectedParkingSpaceForPurchase;
        private ApartmentsResponse _selectedApartment;

        public CreateLeaseAgreementForCustomerViewModel(ReservationRequestResponse selectedRequiry, ApartmentsResponse selectedApartment, ParkingSpaceResponse? selectedParkingSpaceForPurchase)
        {
            _selectedRequiry = selectedRequiry;
            _selectedParkingSpaceForPurchase = selectedParkingSpaceForPurchase;
            _selectedApartment = selectedApartment;
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            //Customer details
            FullName = _selectedRequiry.FullName;
            InquiryId = _selectedRequiry.Id;
            EmergencyContactNo = _selectedRequiry.TelephoneNo;
            Email = _selectedRequiry.Email;

            //Apartment details
            ApartmentId = _selectedApartment.Id;
            ApartmentClassName = _selectedApartment.ApartmentClassName;
            ApartmentBuildingLocation = $@"{_selectedApartment.BuildingName} - {_selectedApartment.BuildingLocation}";
            ReservedParkingSpace = _selectedApartment.ReservedParkingSpace;

            AdditionalParkingSpace = _selectedParkingSpaceForPurchase == null ? "Not requested" : _selectedParkingSpaceForPurchase.LotNo;
        }

        #region Customer Details Fields
        //Auto fill fields section
        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set 
            {
                _fullName = value;
                NotifyOfPropertyChange(() => FullName);
            }
        }

        private int _inquiryId;
        public int InquiryId
        {
            get { return _inquiryId; }
            set
            {
                _inquiryId = value;
                NotifyOfPropertyChange(() => InquiryId);
            }
        }

        private string _emergencyContactNo;
        public string EmergencyContactNo
        {
            get { return _emergencyContactNo; }
            set
            {
                _emergencyContactNo = value;
                NotifyOfPropertyChange(() => EmergencyContactNo);
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyOfPropertyChange(() => Email);
            }
        }

        // Blank Fields
        private string _nICPassportNo;
        public string NICPassportNo
        {
            get { return _nICPassportNo; }
            set
            {
                _nICPassportNo = value;
                NotifyOfPropertyChange(() => NICPassportNo);
            }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                NotifyOfPropertyChange(() => Address);
            }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                NotifyOfPropertyChange(() => Username);
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }
        #endregion

        #region Selected Apartment Details
        //Auto fill fields
        private int _apartmentId;
        public int ApartmentId
        {
            get { return _apartmentId; }
            set
            {
                _apartmentId = value;
                NotifyOfPropertyChange(() => ApartmentId);
            }
        }

        private string _apartmentClassName;
        public string ApartmentClassName
        {
            get { return _apartmentClassName; }
            set
            {
                _apartmentClassName = value;
                NotifyOfPropertyChange(() => ApartmentClassName);
            }
        }

        private string _apartmentBuildingLocation;
        public string ApartmentBuildingLocation
        {
            get { return _apartmentBuildingLocation; }
            set
            {
                _apartmentBuildingLocation = value;
                NotifyOfPropertyChange(() => ApartmentBuildingLocation);
            }
        }

        private string _reservedParkingSpace;
        public string ReservedParkingSpace
        {
            get { return _reservedParkingSpace; }
            set
            {
                _reservedParkingSpace = value;
                NotifyOfPropertyChange(() => ReservedParkingSpace);
            }
        }

        private string _additionalParkingSpace;
        public string AdditionalParkingSpace
        {
            get { return _additionalParkingSpace; }
            set
            {
                _additionalParkingSpace = value;
                NotifyOfPropertyChange(() => AdditionalParkingSpace);
            }
        }

        private DateTime _leaseStartDate = DateTime.Now;

        public DateTime LeaseStartDate
        {
            get { return _leaseStartDate; }
            set 
            { 
                _leaseStartDate = value;
                NotifyOfPropertyChange(() => LeaseStartDate);
            }
        }

        private DateTime _leaseEndDate = DateTime.Now.AddDays(30);

        public DateTime LeaseEndDate
        {
            get { return _leaseEndDate; }
            set
            {
                _leaseEndDate = value;
                NotifyOfPropertyChange(() => LeaseEndDate);
            }
        }
        #endregion

        #region Pricing Details
        private double _pricePerMonth = 0;
        public double PricePerMonth
        {
            get { return _pricePerMonth; }
            set
            {
                _pricePerMonth = value;
                NotifyOfPropertyChange(() => PricePerMonth);
            }
        }

        private double _refundableDepositAmount = 0;
        public double RefundableDepositAmount
        {
            get { return _refundableDepositAmount; }
            set
            {
                _refundableDepositAmount = value;
                NotifyOfPropertyChange(() => RefundableDepositAmount);
            }
        }

        private double _additionalParkingUnitPrice = 0;
        public double AdditionalParkingUnitPrice
        {
            get { return _additionalParkingUnitPrice; }
            set
            {
                _additionalParkingUnitPrice = value;
                NotifyOfPropertyChange(() => AdditionalParkingUnitPrice);
            }
        }

        private int _leasePeriod = 0;
        public int LeasePeriod
        {
            get { return _leasePeriod; }
            set
            {
                _leasePeriod = value;
                NotifyOfPropertyChange(() => LeasePeriod);
            }
        }

        private double _totalCost = 0;
        public double TotalCost
        {
            get { return _totalCost; }
            set
            {
                _totalCost = value;
                NotifyOfPropertyChange(() => TotalCost);
            }
        }
        #endregion
    }
}

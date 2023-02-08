using AppartmentLeaseApp.Interfaces;
using AppartmentLeaseApp.Models.AnonymousModels;
using AppartmentLeaseApp.Models.Apartments;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.ViewModels
{
    public class CreateWaitingApplicationForInquiryViewModel : Screen
    {
        private ReservationRequestResponse _selectedInquiry;
        private IDialogWindowHelper _dialogWindowHelper;
        private IAnonymousManagementEndpoint _anonymousManagementEndpoint;
        private IApartmentManagementEndpoint _apartmentManagementEndpoint;

        public CreateWaitingApplicationForInquiryViewModel(ReservationRequestResponse selectedInquiry, IAnonymousManagementEndpoint anonymousManagementEndpoint,
            IApartmentManagementEndpoint apartmentManagementEndpoint, IDialogWindowHelper dialogWindowHelper)
        {
            _selectedInquiry = selectedInquiry;
            _anonymousManagementEndpoint = anonymousManagementEndpoint;
            _apartmentManagementEndpoint = apartmentManagementEndpoint;
            _dialogWindowHelper = dialogWindowHelper;
        }


        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            FullName = _selectedInquiry.FullName;
            InquiryId = _selectedInquiry.Id;
            TelephoneNumber = _selectedInquiry.TelephoneNo;
            Email = _selectedInquiry.Email;

            var allApartments = await _apartmentManagementEndpoint.GetAllApartmentsWithDetails();
            AllApartmentList = new BindingList<ApartmentsResponse>(allApartments);

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

        private string _telephoneNumber;
        public string TelephoneNumber
        {
            get { return _telephoneNumber; }
            set
            {
                _telephoneNumber = value;
                NotifyOfPropertyChange(() => TelephoneNumber);
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
        #endregion

        #region Filter Attrbiutes
        private string _byLocation;

        public string ByLocation
        {
            get { return _byLocation; }
            set
            {
                _byLocation = value;
                NotifyOfPropertyChange(() => ByLocation);
            }
        }

        private string _byApartmentType;

        public string ByApartmentType
        {
            get { return _byApartmentType; }
            set
            {
                _byApartmentType = value;
                NotifyOfPropertyChange(() => ByApartmentType);
            }
        }

        public async Task FilterApartmentList()
        {
            var filteredList = await _apartmentManagementEndpoint.FilterAllApartments(location: ByLocation ?? "", apartmentType: ByApartmentType ?? "");
            AllApartmentList = new BindingList<ApartmentsResponse>(filteredList);
        }
        #endregion

        #region Apartment List

        private BindingList<ApartmentsResponse> _allApartmentList;

        public BindingList<ApartmentsResponse> AllApartmentList
        {
            get { return _allApartmentList; }
            set
            {
                _allApartmentList = value;
                NotifyOfPropertyChange(() => AllApartmentList);
            }
        }

        private ApartmentsResponse? _selectedApartment;

        public ApartmentsResponse? SelectedApartment
        {
            get { return _selectedApartment; }
            set
            {
                _selectedApartment = value;
                SelectedApartmentClass = _selectedApartment?.ApartmentClassName == null ? null : _selectedApartment.ApartmentClassName;
                SelectedLocation = _selectedApartment?.BuildingLocation == null ? null : _selectedApartment.BuildingLocation;
                NotifyOfPropertyChange(() => SelectedApartment);
            }
        }
        #endregion

        #region Create Waiting Application
        private string? _selectedApartmentClass;
        public string? SelectedApartmentClass
        {
            get { return _selectedApartmentClass; }
            set
            {
                _selectedApartmentClass = value;
                NotifyOfPropertyChange(() => SelectedApartmentClass);
            }
        }

        private string? _selectedLocation;
        public string? SelectedLocation
        {
            get { return _selectedLocation; }
            set
            {
                _selectedLocation = value;
                NotifyOfPropertyChange(() => SelectedLocation);
            }
        }

        private DateTime? _requiredDate;

        public DateTime? RequiredDate
        {
            get { return _requiredDate; }
            set
            {
                _requiredDate = value;
                NotifyOfPropertyChange(() => RequiredDate);
            }
        }

        public async void CreateWaitingApplication()
        {
            if (string.IsNullOrWhiteSpace(SelectedLocation) || string.IsNullOrEmpty(SelectedApartmentClass) || 
                RequiredDate == null)
            {
                await _dialogWindowHelper.ShowDialogWindow("Please provide required information.");
                return;
            } 
            
            if (RequiredDate != null && DateTime.Compare(DateTime.Now, (DateTime)RequiredDate) > 0)
            {
                await _dialogWindowHelper.ShowDialogWindow("Please select a date after today");
                return;
            }            

            try
            {
                var createData = new CreateWaitingApplicationRequest
                {
                    FullName = _selectedInquiry.FullName,
                    Email = Email,
                    TelephoneNo = TelephoneNumber,
                    ApartmentClassId = SelectedApartment == null ? 0 : SelectedApartment.ApartmentClassId ?? 0,
                    RequiredBuildingLocation = SelectedApartment == null ? "" : SelectedApartment.BuildingLocation ?? "",
                    RequiredStartDate = (DateTime)RequiredDate!,
                    ReservationInquiryId = _selectedInquiry.Id
                };

                var result = await _anonymousManagementEndpoint.CreateWaitingApplication(createData);

                await _dialogWindowHelper.ShowDialogWindow(result ?? "Success");

            }
            catch (Exception ex)
            {
                await _dialogWindowHelper.ShowDialogWindow(ex.Message);
            }
        }
        #endregion
    }
}

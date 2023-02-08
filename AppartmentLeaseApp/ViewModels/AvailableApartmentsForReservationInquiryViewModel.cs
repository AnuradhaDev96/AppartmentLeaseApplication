using AppartmentLeaseApp.Interfaces;
using AppartmentLeaseApp.Models.AnonymousModels;
using AppartmentLeaseApp.Models.Apartments;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AppartmentLeaseApp.ViewModels
{
    public class AvailableApartmentsForReservationInquiryViewModel : Screen
    {
        private IApartmentManagementEndpoint _apartmentManagementEndpoint;
        private ILeaseAgreementManagementEndpoint _leaseAgreementManagementEndpoint;
        private List<ApartmentsResponse>? _apartmentList;
        private ReservationRequestResponse _selectedRequiry;
        private IWindowManager _windowManager;
        private IDialogWindowHelper _dialogWindowHelper;
        private IAnonymousManagementEndpoint _anonymousManagementEndpoint;

        public AvailableApartmentsForReservationInquiryViewModel(IApartmentManagementEndpoint apartmentManagementEndpoint, IDialogWindowHelper dialogWindowHelper,
            ReservationRequestResponse selectedRequiry, IWindowManager windowManager, ILeaseAgreementManagementEndpoint leaseAgreementManagementEndpoint,
            IAnonymousManagementEndpoint anonymousManagementEndpoint)
        {
            _apartmentManagementEndpoint = apartmentManagementEndpoint;
            _leaseAgreementManagementEndpoint = leaseAgreementManagementEndpoint;
            _selectedRequiry = selectedRequiry;
            _windowManager = windowManager;
            _dialogWindowHelper = dialogWindowHelper;
            _anonymousManagementEndpoint = anonymousManagementEndpoint;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            await LoadList();

        }

        public async Task LoadList()
        {
            _apartmentList = await _apartmentManagementEndpoint.GetAvailableApartmentsWithDetails();
            var parkingList = await _apartmentManagementEndpoint.GetAvailableParkingSpaces();
            AvailableApartmentList = new BindingList<ApartmentsResponse>(_apartmentList);
            AvailableParkingSpacesList = new BindingList<ParkingSpaceResponse>(parkingList);
        }

        #region Apartment List

        private BindingList<ApartmentsResponse> _availableApartmentList;

        public BindingList<ApartmentsResponse> AvailableApartmentList
        {
            get { return _availableApartmentList; }
            set
            {
                _availableApartmentList = value;
                NotifyOfPropertyChange(() => AvailableApartmentList);
            }
        }
        
        private ApartmentsResponse? _selectedApartment;

        public ApartmentsResponse? SelectedApartment
        {
            get { return _selectedApartment; }
            set
            {
                _selectedApartment = value;
                NotifyOfPropertyChange(() => SelectedApartment);
            }
        }

        #endregion

        #region Selected Customer details

        private String _customerName;

        public String CustomerName
        {
            get 
            { 
                return _selectedRequiry.FullName; 
            }
        }

        public String TelephoneNumber
        {
            get
            {
                return _selectedRequiry.TelephoneNo;
            }
        }
        #endregion

        #region Parking spaces List
        private BindingList<ParkingSpaceResponse> _availableParkingSpacesList;

        public BindingList<ParkingSpaceResponse> AvailableParkingSpacesList
        {
            get { return _availableParkingSpacesList; }
            set
            {
                _availableParkingSpacesList = value;
                NotifyOfPropertyChange(() => AvailableParkingSpacesList);
            }
        }

        private ParkingSpaceResponse? _selectedParkingSpace;

        public ParkingSpaceResponse? SelectedParkingSpace
        {
            get { return _selectedParkingSpace; }
            set
            {
                _selectedParkingSpace = value;
                NotifyOfPropertyChange(() => SelectedParkingSpace);
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
            var filteredList = await _apartmentManagementEndpoint.FilterAvailableApartments(location: ByLocation ?? "", apartmentType: ByApartmentType ?? "");
            AvailableApartmentList = new BindingList<ApartmentsResponse>(filteredList);
        }
        #endregion

        #region Decision Buttons
        public async void CreateLeaseAgreement()
        {
            if (_selectedRequiry.Status == "LeaseCreated")
            {
                await _windowManager.ShowDialogAsync(new MessageDisplayDialogViewModel(messageToDisplay: "Sorry! This inquiry has Lease Agreement."), null, GetDialogWindowSettings());
                return;
            }

            if (_selectedRequiry.Status != "PendingResponse")
            {
                await _windowManager.ShowDialogAsync(new MessageDisplayDialogViewModel(messageToDisplay: "Sorry! This inquiry has been processed."), null, GetDialogWindowSettings());
                return;
            }

            if (SelectedApartment == null)
            {                
                await _windowManager.ShowDialogAsync(new MessageDisplayDialogViewModel(messageToDisplay: "Select Apartment to Continue"), null, GetDialogWindowSettings());
                return;
            }
            
            await TryCloseAsync();
            await _windowManager.ShowWindowAsync(new CreateLeaseAgreementForCustomerViewModel(
                selectedApartment: SelectedApartment,
                selectedRequiry: _selectedRequiry,
                selectedParkingSpaceForPurchase: SelectedParkingSpace,
                apartmentManagementEndpoint: _apartmentManagementEndpoint,
                leaseAgreementManagementEndpoint: _leaseAgreementManagementEndpoint,
                windowManager: _windowManager));
            
            
        }

        public async void AddToWaitingQueue()
        {
            if (_selectedRequiry.Status != "PendingResponse")
            {
                await _windowManager.ShowDialogAsync(new MessageDisplayDialogViewModel(messageToDisplay: "Sorry! This inquiry has been processed."), null, GetDialogWindowSettings());
                return;
            }

            await TryCloseAsync();
            await _dialogWindowHelper.ShowPopUpWindow(new CreateWaitingApplicationForInquiryViewModel(
                selectedInquiry: _selectedRequiry, 
                anonymousManagementEndpoint: _anonymousManagementEndpoint,
                apartmentManagementEndpoint: _apartmentManagementEndpoint,
                dialogWindowHelper: _dialogWindowHelper));
        }

        private dynamic GetDialogWindowSettings()
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStyle = WindowStyle.ToolWindow;
            settings.ShowInTaskbar = true;
            settings.Title = "Alert";
            settings.WindowState = WindowState.Normal;
            settings.ResizeMode = ResizeMode.NoResize;
            return settings;
        }
        #endregion
    }
}

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
    public class AvailableApartmentsForReservationInquiryViewModel : Screen
    {
        private IApartmentManagementEndpoint _apartmentManagementEndpoint;
        private List<ApartmentsResponse>? _apartmentList;
        private ReservationRequestResponse _selectedRequiry;

        public AvailableApartmentsForReservationInquiryViewModel(IApartmentManagementEndpoint apartmentManagementEndpoint, ReservationRequestResponse selectedRequiry)
        {
            _apartmentManagementEndpoint = apartmentManagementEndpoint;
            _selectedRequiry = selectedRequiry;
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
    }
}

using AppartmentLeaseApp.Interfaces;
using AppartmentLeaseApp.Models.AnonymousModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.ViewModels
{
    public class ReviewReservationRequestsViewModel : Conductor<Screen>
    {
        private IAnonymousManagementEndpoint _anonymousManagementEndpoint;
        private List<ReservationRequestResponse>? requestList;
        private IWindowManager _windowManager;
        private SimpleContainer _simpleContainer;

        public ReviewReservationRequestsViewModel(IAnonymousManagementEndpoint anonymousManagementEndpoint, IWindowManager windowManager, SimpleContainer simpleContainer)
        {
            _anonymousManagementEndpoint = anonymousManagementEndpoint;
            _windowManager = windowManager;
            _simpleContainer = simpleContainer;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadList();

        }

        public async Task LoadList()
        {
            requestList = await _anonymousManagementEndpoint.GetReservationRequests();
            ReservationRequestsList = new BindingList<ReservationRequestResponse>(requestList);
        }

        private BindingList<ReservationRequestResponse> _reservationRequestsList;

        public BindingList<ReservationRequestResponse> ReservationRequestsList
        {
            get { return _reservationRequestsList; }
            set 
            {
                _reservationRequestsList = value;
                NotifyOfPropertyChange(() => ReservationRequestsList);
            }
        }

        private ReservationRequestResponse? _selectedItem;

        public ReservationRequestResponse? SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                //if (_selectedItem < 0)
                //{
                //    FacilityNames = null;
                //}
                //else
                //{
                //    FacilityNames = new BindableCollection<string>(apList?[_selectedItem].AvailableFacilites);
                //}
                NotifyOfPropertyChange(() => SelectedItem);
            }
        }

        #region Open search apartments window

        private bool _isErrorVisible;

        public bool IsErrorVisible
        {
            get
            {
                bool output = false;
                if (ErrorMessage?.Length > 0)
                {
                    output = true;
                }
                return output;
            }
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage);
                NotifyOfPropertyChange(() => IsErrorVisible);
            }
        }

        public async Task OpenApartmentWindow()
        {
            ErrorMessage = "";
            if (_selectedItem == null)
            {
                ErrorMessage = "Please select an inquiry";
                return;
            }                

            await _windowManager.ShowWindowAsync(
                new AvailableApartmentsForReservationInquiryViewModel(
                    selectedRequiry: _selectedItem, 
                    apartmentManagementEndpoint: _simpleContainer.GetInstance<IApartmentManagementEndpoint>(),
                    windowManager: _simpleContainer.GetInstance<IWindowManager>()));
        }
        #endregion
    }
}

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
    public class ReviewReservationRequestsViewModel : Screen
    {
        private IAnonymousManagementEndpoint _anonymousManagementEndpoint;
        private List<ReservationRequestResponse>? requestList;

        public ReviewReservationRequestsViewModel(IAnonymousManagementEndpoint anonymousManagementEndpoint)
        {
            _anonymousManagementEndpoint = anonymousManagementEndpoint;
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

        private ReservationRequestResponse _selectedItem;

        public ReservationRequestResponse SelectedItem
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


    }
}

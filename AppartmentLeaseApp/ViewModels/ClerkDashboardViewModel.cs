using AppartmentLeaseApp.EventModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.ViewModels
{
    public class ClerkDashboardViewModel : Conductor<Screen>
    {
        private SystemUsersViewModel _systemUsersViewModel;
        private ApartmentClassesViewModel _apartmentClassesViewModel;
        private SimpleContainer _simpleContainer;
        private IEventAggregator _events;

        public ClerkDashboardViewModel(SimpleContainer simpleContainer, SystemUsersViewModel systemUsersViewModel,
            ApartmentClassesViewModel apartmentClassesViewModel, IEventAggregator events)
        {
            _systemUsersViewModel = systemUsersViewModel;
            _apartmentClassesViewModel = apartmentClassesViewModel;
            _simpleContainer = simpleContainer;
            _events = events;
        }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            {
                _currentView = value; 
                NotifyOfPropertyChange(() => CurrentView);
            }
        }

        public async void UsersPage()
        {
            //CurrentView = _systemUsersViewModel;
            await ActivateItemAsync(_systemUsersViewModel);
        }

        public async void ApartmentClassesPage()
        {
            //CurrentView = _systemUsersViewModel;
            await ActivateItemAsync(_apartmentClassesViewModel);
        }

        public async void ReviewReservationInquiriesPage()
        {
            //CurrentView = _systemUsersViewModel;
            await ActivateItemAsync(_simpleContainer.GetInstance<ReviewReservationRequestsViewModel>());
        }

        public async void ReviewWaitingApplications()
        {
            await ActivateItemAsync(_simpleContainer.GetInstance<ClerkReviewWaitingQueueViewModel>());
        }

        public async Task LogOut()
        {
            await _events.PublishOnUIThreadAsync(new LogoutEventModel());
        }
    }
}

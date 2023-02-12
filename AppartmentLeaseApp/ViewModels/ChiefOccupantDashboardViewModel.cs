using AppartmentLeaseApp.EventModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.ViewModels
{
    public class ChiefOccupantDashboardViewModel : Conductor<Screen>
    {
        private SimpleContainer _simpleContainer;
        private IEventAggregator _events;


        public ChiefOccupantDashboardViewModel(SimpleContainer simpleContainer, IEventAggregator events)
        {
            _simpleContainer = simpleContainer;
            _events = events;
        }

        public async void MyLeaseAgreementsPage()
        {
            await ActivateItemAsync(_simpleContainer.GetInstance<ChiefAccoupantMyLeaseAgreementsViewModel>());
        }

        public async Task LogOut()
        {
            await _events.PublishOnUIThreadAsync(new LogoutEventModel());
        }

    }
}

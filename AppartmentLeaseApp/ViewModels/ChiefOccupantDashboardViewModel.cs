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


        public ChiefOccupantDashboardViewModel(SimpleContainer simpleContainer)
        {
            _simpleContainer = simpleContainer;
        }

        public async void MyLeaseAgreementsPage()
        {
            await ActivateItemAsync(_simpleContainer.GetInstance<ChiefAccoupantMyLeaseAgreementsViewModel>());
        }

    }
}

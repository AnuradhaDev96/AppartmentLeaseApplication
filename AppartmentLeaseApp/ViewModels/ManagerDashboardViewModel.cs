using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.ViewModels
{
    public class ManagerDashboardViewModel : Conductor<Screen>
    {
        private SimpleContainer _simpleContainer;


        public ManagerDashboardViewModel(SimpleContainer simpleContainer)
        {
            _simpleContainer = simpleContainer;
        }

        public async void ReportsViewer()
        {
            await ActivateItemAsync(_simpleContainer.GetInstance<ManagerReportsArenaViewModel>());
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.Interfaces
{
    public interface IDialogWindowHelper
    {
        protected dynamic GetDialogWindowSettings();

        Task ShowDialogWindow(string message);

        Task ShowPopUpWindow(object destinationViewModel);
    }
}

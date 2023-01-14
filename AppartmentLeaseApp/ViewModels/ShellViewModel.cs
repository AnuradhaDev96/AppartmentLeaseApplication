using AppartmentLeaseApp.Interfaces;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private ICalculations _calculations;
        private LoginViewModel _loginViewModel;

        public ShellViewModel(ICalculations calculations, LoginViewModel loginViewModel)
        {
            _calculations = calculations;
            _loginViewModel = loginViewModel;
            LoadPage();            
        }

        public async void LoadPage()
        {
            await ActivateItem(_loginViewModel);
        }

        protected async Task ActivateItem(Object item)
        {
            await ActivateItemAsync(item);
        }
    }
}

using AppartmentLeaseApp.Interfaces;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _userName;
        private readonly IAPIHelper _apiHelper;
        //private readonly string _password;

        public LoginViewModel(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public string UserName
        {
            get { return _userName; }
            set 
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
                //NotifyOfPropertyChange(() => CanLogin);
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set 
            { 
                _password = value;
                NotifyOfPropertyChange(() => Password);
                //NotifyOfPropertyChange(() => CanLogin);
            }
        }

        public bool CanLogin
        {
            get
            {
                if (UserName.Length > 0 && Password.Length > 0)
                {
                    return true;
                }
                return false;
            }
            
        }

        public async Task LogIn(string userName, string password)
        {
            try
            {
                var result = await _apiHelper.Authenticate(userName, password);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
    }
}

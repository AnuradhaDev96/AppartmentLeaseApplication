using AppartmentLeaseApp.EventModels;
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
        private readonly IAPIHelper _apiHelper;
        private IEventAggregator _events;
        //private readonly string _password;

        public LoginViewModel(IAPIHelper apiHelper, IEventAggregator events)
        {
            _apiHelper = apiHelper;
            _events = events;
        }

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogin);
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
                NotifyOfPropertyChange(() => CanLogin);
            }
        }

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
            set {
                _errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage);
                NotifyOfPropertyChange(() => IsErrorVisible);
            }
        }


        public bool CanLogin
        {
            get                
            {
                bool output = false;
                if (UserName?.Length > 0 && Password?.Length > 0)
                {
                    return output;
                }
                return output;
            }
            
        }

        public async Task LogIn(string userName, string password)
        {
            try
            {
                ErrorMessage = "";
                var result = await _apiHelper.Authenticate(userName, password);

                await _apiHelper.SyncLoggedInUser(result);

                await _events.PublishOnUIThreadAsync(new LoginEventModel());
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message == "NotFound" ? "Incorrect Username or Password" : "Unexpected error occured";
            }
        }
    }
}

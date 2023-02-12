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
    public class CreateReservationRequestViewModel : Screen
    {
        private readonly IAPIHelper _apiHelper;
        private IEventAggregator _events;
        private IAnonymousManagementEndpoint _anonymousManagementEndpoint;

        public CreateReservationRequestViewModel(IAPIHelper apiHelper, IAnonymousManagementEndpoint anonymousManagementEndpoint, IEventAggregator events)
        {
            _apiHelper = apiHelper;
            _anonymousManagementEndpoint = anonymousManagementEndpoint;
            _events = events;
        }

        #region View private properties

        private string _fullName;

        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                NotifyOfPropertyChange(() => FullName);
            }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyOfPropertyChange(() => Email);
            }
        }

        private string _telephoneNumber;

        public string TelephoneNumber
        {
            get { return _telephoneNumber; }
            set
            {
                _telephoneNumber = value;
                NotifyOfPropertyChange(() => TelephoneNumber);
            }
        }
        #endregion

        #region Error display properties
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

        #endregion

        public async Task Request()
        {
            try
            {
                ErrorMessage = "";
                var result = await _anonymousManagementEndpoint.CreateReservationRequest(fullName: FullName, email: Email, telephoneNumber: TelephoneNumber);
                ErrorMessage = result ?? "Created";

            }
            catch (Exception e)
            {
                ErrorMessage = "Unexpected error occured";
            }
        }

        public async Task GoBack() 
        {
            await _events.PublishOnUIThreadAsync(new GoBackToLoginEventModel());
        }
    }
}

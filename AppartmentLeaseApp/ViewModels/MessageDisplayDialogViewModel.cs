using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.ViewModels
{
    public class MessageDisplayDialogViewModel : Screen
    {
        public MessageDisplayDialogViewModel(string messageToDisplay)
        {
            _messageToDisplay = messageToDisplay;
        }

        private string _messageToDisplay;

        public string MessageToDisplay
        {
            get { return _messageToDisplay; }
            set 
            { 
                _messageToDisplay = value;
                NotifyOfPropertyChange(() => MessageToDisplay);
            }
        }

        public async Task OkButton()
        {
            await TryCloseAsync();
        }

    }
}

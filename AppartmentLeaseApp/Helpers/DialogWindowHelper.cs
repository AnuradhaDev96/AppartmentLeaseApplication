using AppartmentLeaseApp.Interfaces;
using AppartmentLeaseApp.ViewModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AppartmentLeaseApp.Helpers
{
    public class DialogWindowHelper : IDialogWindowHelper
    {
        private IWindowManager _windowManager;

        public DialogWindowHelper(IWindowManager windowManager)
        {
            _windowManager = windowManager;
        }

        public dynamic GetDialogWindowSettings()
        {
            dynamic settings = new ExpandoObject();
            settings.WindowStyle = WindowStyle.ToolWindow;
            settings.ShowInTaskbar = true;
            settings.Title = "Alert";
            settings.WindowState = WindowState.Normal;
            settings.ResizeMode = ResizeMode.NoResize;
            return settings;
        }

        public async Task ShowDialogWindow(string message)
        {
            await _windowManager.ShowDialogAsync(new MessageDisplayDialogViewModel(messageToDisplay: message), null, GetDialogWindowSettings());
        }

        public async Task ShowPopUpWindow(object destinationViewModel)
        {
            await _windowManager.ShowWindowAsync(destinationViewModel);
        }
    }
}

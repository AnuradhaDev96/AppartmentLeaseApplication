﻿using AppartmentLeaseApp.EventModels;
using AppartmentLeaseApp.Interfaces;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LoginEventModel>
    {
        private ICalculations _calculations;
        //private SystemUsersViewModel _systemUsersViewModel;
        private ClerkDashboardViewModel _clerkDashboardViewModel;
        IEventAggregator _events;

        private SimpleContainer _simpleContainer;

        public ShellViewModel(ICalculations calculations, IEventAggregator events,
            ClerkDashboardViewModel clerkDashboardViewModel, SimpleContainer simpleContainer)
        {
            _events = events;
            _events.Subscribe(this);
            _calculations = calculations;
            _clerkDashboardViewModel = clerkDashboardViewModel;
            _simpleContainer = simpleContainer;

            // to retrieve single instance per request
            LoadPage(_simpleContainer.GetInstance<LoginViewModel>());            
        }

        public async void LoadPage(object viewModel)
        {
            await ActivateItem(viewModel);
        }

        protected async Task ActivateItem(Object item)
        {
            await ActivateItemAsync(item);
        }

        public Task? HandleAsync(LoginEventModel message, CancellationToken cancellationToken)
        {
            LoadPage(_clerkDashboardViewModel);
            return null;
        }
    }
}

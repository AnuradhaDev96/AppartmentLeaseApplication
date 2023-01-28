using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AppartmentLeaseApp.ApiProviders;
using AppartmentLeaseApp.Helpers;
using AppartmentLeaseApp.Interfaces;
using AppartmentLeaseApp.Models;
using AppartmentLeaseApp.ViewModels;
using Caliburn.Micro;

namespace AppartmentLeaseApp
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _simpleContainer = new();

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _simpleContainer.Instance(_simpleContainer)
                .PerRequest<IUserManagementEndpoint, UserManagementEndpoint>()
                .PerRequest<IApartmentManagementEndpoint, ApartmentManagementEndpoint>()
                .PerRequest<IAnonymousManagementEndpoint, AnonymousManagementEndpoint>()
                .PerRequest<ILeaseAgreementManagementEndpoint, LeaseAgreementManagementEndpoint>();

            // return instances per request

            //Dependency Injection
            _simpleContainer.Singleton<IWindowManager, WindowManager>();
            _simpleContainer.Singleton<IEventAggregator, EventAggregator>();
            _simpleContainer.Singleton<ILoggedInUser, LoggedInUser>();
            _simpleContainer.Singleton<IAPIHelper, APIHelper>();


            _simpleContainer.RegisterPerRequest(typeof(ShellViewModel), null, typeof(ShellViewModel));

            // Dependency injection of functional classes
            _simpleContainer.PerRequest<ICalculations, Calculations>();

            //Dependency Injection based on the class names of ViewModels
            // Every time you get instance, a new instance returns
            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _simpleContainer.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
            // On starup ShellViewModel launches
        }

        protected Task DisplayRootViewFor<T>(IDictionary<string, object>? settings = null)
        {
            return DisplayRootViewForAsync(typeof(T), settings);
        }

        protected override object GetInstance(Type service, string key)
        {
            return _simpleContainer.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _simpleContainer.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _simpleContainer.BuildUp(instance);
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[] {Assembly.GetExecutingAssembly()};
        }
    }
}

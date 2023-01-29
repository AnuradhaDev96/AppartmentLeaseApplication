using AppartmentLeaseApp.Interfaces;
using AppartmentLeaseApp.Models.LeaseAgreement;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.ViewModels
{
    public class ChiefAccoupantMyLeaseAgreementsViewModel : Screen
    {
        private ILoggedInUser _loggedInUser;
        private ILeaseAgreementManagementEndpoint _leaseAgreementManagementEndpoint;

        public ChiefAccoupantMyLeaseAgreementsViewModel(ILoggedInUser loggedInUser, ILeaseAgreementManagementEndpoint leaseAgreementManagementEndpoint)
        {
            _loggedInUser = loggedInUser;
            _leaseAgreementManagementEndpoint = leaseAgreementManagementEndpoint;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadList();
        }

        public async Task LoadList()
        {
            var chiefOccupantId = _loggedInUser.Id;
            var list = await _leaseAgreementManagementEndpoint.GetLeaseAgreementsByChiefOccupant(chiefOccupantId);
            MyLeaseAgreementsList = new BindingList<LeaseAgreementSummaryResponse>(list);
        }

        private BindingList<LeaseAgreementSummaryResponse> _myLeaseAgreementsList;

        public BindingList<LeaseAgreementSummaryResponse> MyLeaseAgreementsList
        {
            get { return _myLeaseAgreementsList; }
            set
            {
                _myLeaseAgreementsList = value;
                NotifyOfPropertyChange(() => MyLeaseAgreementsList);
            }
        }

        private LeaseAgreementSummaryResponse? _selectedLeaseAgreement;

        public LeaseAgreementSummaryResponse? SelectedLeaseAgreement
        {
            get { return _selectedLeaseAgreement; }
            set 
            { 
                _selectedLeaseAgreement = value;
                NotifyOfPropertyChange(() => SelectedLeaseAgreement);
            }
        }

    }
}

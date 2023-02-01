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
        private IDialogWindowHelper _dialogWindowHelper;

        public ChiefAccoupantMyLeaseAgreementsViewModel(ILoggedInUser loggedInUser, ILeaseAgreementManagementEndpoint leaseAgreementManagementEndpoint,
            IDialogWindowHelper dialogWindowHelper)
        {
            _loggedInUser = loggedInUser;
            _leaseAgreementManagementEndpoint = leaseAgreementManagementEndpoint;
            _dialogWindowHelper = dialogWindowHelper;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadList();
        }

        public async Task LoadList()
        {
            var loggedUserId = _loggedInUser.Id;
            var list = await _leaseAgreementManagementEndpoint.GetLeaseAgreementsByLoggedUser(loggedUserId);
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

        public async Task ManageDependants() 
        {
            if (SelectedLeaseAgreement == null)
            {
                await _dialogWindowHelper.ShowDialogWindow("Please select lease agreement to continue");
                return;
            }

            if (SelectedLeaseAgreement.Status == "Extended" || SelectedLeaseAgreement.Status == "Ended")
            {
                await _dialogWindowHelper.ShowDialogWindow(@$"Sorry! Dependant management is not allowed for {SelectedLeaseAgreement.Status} lease agreements.");
                return;
            }

            await _dialogWindowHelper.ShowPopUpWindow(new ChiefOccupantDependantManagementViewModel(
                selectedLeaseAgreementSummary: SelectedLeaseAgreement,
                loggedInUser: _loggedInUser,
                leaseAgreementManagementEndpoint: _leaseAgreementManagementEndpoint,
                dialogWindowHelper: _dialogWindowHelper));

        }
        //ChiefOccupantLeaseExtentionsManagementViewModel

        public async Task LeaseExtentions()
        {
            if (SelectedLeaseAgreement == null)
            {
                await _dialogWindowHelper.ShowDialogWindow("Please select lease agreement to continue");
                return;
            }

            if (SelectedLeaseAgreement != null && SelectedLeaseAgreement.Status != "Started")
            {
                await _dialogWindowHelper.ShowDialogWindow("Lease Agreement is not in Started mode. You are not allowed to manage extentions.");
                return;
            }

            await _dialogWindowHelper.ShowPopUpWindow(new ChiefOccupantLeaseExtentionsManagementViewModel(
                selectedLeaseAgreementSummary: SelectedLeaseAgreement,
                leaseAgreementManagementEndpoint: _leaseAgreementManagementEndpoint,
                dialogWindowHelper: _dialogWindowHelper));
        }

    }
}

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
    // In this view control the creating of extention requests if the end date - now >= 60 days
    public class ChiefOccupantLeaseExtentionsManagementViewModel : Screen
    {
        private LeaseAgreementSummaryResponse _selectedLeaseAgreementSummary;
        private ILeaseAgreementManagementEndpoint _leaseAgreementManagementEndpoint;
        private IDialogWindowHelper _dialogWindowHelper;

        public ChiefOccupantLeaseExtentionsManagementViewModel(LeaseAgreementSummaryResponse selectedLeaseAgreementSummary,
            ILeaseAgreementManagementEndpoint leaseAgreementManagementEndpoint, IDialogWindowHelper dialogWindowHelper)
        {
            _selectedLeaseAgreementSummary = selectedLeaseAgreementSummary;
            _leaseAgreementManagementEndpoint = leaseAgreementManagementEndpoint;
            _dialogWindowHelper = dialogWindowHelper;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            //set Active Lease agreement details
            AgreementId = _selectedLeaseAgreementSummary.AgreementId;
            AgreementStatus = _selectedLeaseAgreementSummary.Status;
            LeaseStartDate = _selectedLeaseAgreementSummary.LeaseStartDate;
            LeaseEndDate = _selectedLeaseAgreementSummary.LeaseEndDate;

            await LoadList();

        }

        public async Task LoadList()
        {
            var list = await _leaseAgreementManagementEndpoint.GetLeaseExtentionsByLeaseAgreementId(_selectedLeaseAgreementSummary.AgreementId);
            ExtentionRequestsList = new BindingList<LeaseExtentionGetResponse>(list);
        }

        #region Started Lease Agreement
        private int _agreementId;
        public int AgreementId
        {
            get { return _agreementId; }
            set
            {
                _agreementId = value;
                NotifyOfPropertyChange(() => AgreementId);
            }
        }

        private string _agreementStatus;
        public string AgreementStatus
        {
            get { return _agreementStatus; }
            set
            {
                _agreementStatus = value;
                NotifyOfPropertyChange(() => AgreementStatus);
            }
        }

        private DateTime _leaseStartDate;
        public DateTime LeaseStartDate
        {
            get { return _leaseStartDate; }
            set
            {
                _leaseStartDate = value;
                NotifyOfPropertyChange(() => LeaseStartDate);
            }
        }

        private DateTime _leaseEndDate;
        public DateTime LeaseEndDate
        {
            get { return _leaseEndDate; }
            set
            {
                _leaseEndDate = value;
                NotifyOfPropertyChange(() => LeaseEndDate);
            }
        }
        #endregion

        #region Request List column
        private BindingList<LeaseExtentionGetResponse>? _extentionRequestsList;

        public BindingList<LeaseExtentionGetResponse>? ExtentionRequestsList
        {
            get { return _extentionRequestsList; }
            set
            {
                _extentionRequestsList = value;
                NotifyOfPropertyChange(() => ExtentionRequestsList);
            }
        }

        private LeaseExtentionGetResponse? _selectedExtentionRequest;

        public LeaseExtentionGetResponse? SelectedExtentionRequest
        {
            get { return _selectedExtentionRequest; }
            set
            {
                _selectedExtentionRequest = value;
                SelectedRequestId = _selectedExtentionRequest == null ? null : _selectedExtentionRequest.Id;
                SelectedRequestStatus = _selectedExtentionRequest == null ? null : _selectedExtentionRequest.Status;
                NewStartDate = _selectedExtentionRequest == null ? null : _selectedExtentionRequest.StartDate;
                NewEndDate = _selectedExtentionRequest == null ? null : _selectedExtentionRequest.EndDate;
                NotifyOfPropertyChange(() => SelectedExtentionRequest);
                //NotifyOfPropertyChange(() => IsEditMode);
                //NotifyOfPropertyChange(() => IsCreateMode);
            }
        }
        #endregion

        #region ManageRequest Section
        private int? _selectedRequestId;

        public int? SelectedRequestId
        {
            get { return _selectedRequestId; }
            set
            {
                _selectedRequestId = value;
                NotifyOfPropertyChange(() => SelectedRequestId);
            }
        }

        private string? _selectedRequestStatus;

        public string? SelectedRequestStatus
        {
            get { return _selectedRequestStatus; }
            set
            {
                _selectedRequestStatus = value;
                NotifyOfPropertyChange(() => SelectedRequestStatus);
            }
        }

        private DateTime? _newStartDate = DateTime.Now.AddDays(61);

        public DateTime? NewStartDate
        {
            get { return _newStartDate; }
            set
            {
                _newStartDate = value;
                NotifyOfPropertyChange(() => NewStartDate);
            }
        }

        private DateTime? _newEndDate = DateTime.Now.AddDays(122);

        public DateTime? NewEndDate
        {
            get { return _newEndDate; }
            set
            {
                _newEndDate = value;
                NotifyOfPropertyChange(() => NewEndDate);
            }
        }
        #endregion
    }
}

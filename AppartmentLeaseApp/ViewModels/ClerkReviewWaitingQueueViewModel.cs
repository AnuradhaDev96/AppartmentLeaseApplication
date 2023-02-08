using AppartmentLeaseApp.Interfaces;
using AppartmentLeaseApp.Models.AnonymousModels;
using AppartmentLeaseApp.Models.Apartments;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.ViewModels
{
    public class ClerkReviewWaitingQueueViewModel : Screen
    {
        private IApartmentManagementEndpoint _apartmentManagementEndpoint;
        private IDialogWindowHelper _dialogWindowHelper;
        public ClerkReviewWaitingQueueViewModel(IApartmentManagementEndpoint apartmentManagementEndpoint, IDialogWindowHelper dialogWindowHelper)
        {
            _apartmentManagementEndpoint = apartmentManagementEndpoint;
            _dialogWindowHelper = dialogWindowHelper;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            var availableApartments = await _apartmentManagementEndpoint.GetAvailableApartmentsWithMatchingWaitingApplicationInfo();
            AvaialbleApartmentsWithWaitingInfoList = new BindingList<ApartmentsWithMatchingWaitingApplicationsResponse>(availableApartments);

        }

        #region Available apartments with application count
        private BindingList<ApartmentsWithMatchingWaitingApplicationsResponse> _avaialbleApartmentsWithWaitingInfoList;

        public BindingList<ApartmentsWithMatchingWaitingApplicationsResponse> AvaialbleApartmentsWithWaitingInfoList
        {
            get { return _avaialbleApartmentsWithWaitingInfoList; }
            set
            {
                _avaialbleApartmentsWithWaitingInfoList = value;
                NotifyOfPropertyChange(() => AvaialbleApartmentsWithWaitingInfoList);
            }
        }

        private ApartmentsWithMatchingWaitingApplicationsResponse? _selectedApartment;

        public ApartmentsWithMatchingWaitingApplicationsResponse? SelectedApartment
        {
            get { return _selectedApartment; }
            set
            {
                _selectedApartment = value;
                //SelectedApartmentClass = _selectedApartment?.ApartmentClassName == null ? null : _selectedApartment.ApartmentClassName;
                //SelectedLocation = _selectedApartment?.BuildingLocation == null ? null : _selectedApartment.BuildingLocation;
                if (_selectedApartment != null && _selectedApartment.MatchingWaitingApplicationsList != null && _selectedApartment.MatchingWaitingApplicationsList.Count > 0)
                {
                    MatchingWaitingApplicationList = new BindingList<WaitingApplicationResponse>(_selectedApartment.MatchingWaitingApplicationsList);
                    EligibleWaitingApplicationToProceed = _selectedApartment.MatchingWaitingApplicationsList[0];
                }
                else
                {
                    MatchingWaitingApplicationList = null;
                }
                NotifyOfPropertyChange(() => SelectedApartment);
            }
        }
        #endregion

        #region Matching WaitingApplication List
        //MatchingWaitingApplicationList
        private BindingList<WaitingApplicationResponse>? _matchingWaitingApplicationList;

        public BindingList<WaitingApplicationResponse>? MatchingWaitingApplicationList
        {
            get { return _matchingWaitingApplicationList; }
            set
            {
                _matchingWaitingApplicationList = value;
                NotifyOfPropertyChange(() => MatchingWaitingApplicationList);
            }
        }

        private WaitingApplicationResponse? _selectedWaitingApplication;

        public WaitingApplicationResponse? SelectedWaitingApplication
        {
            get { return _selectedWaitingApplication; }
            set
            {
                _selectedWaitingApplication = value;
                //SelectedApartmentClass = _selectedApartment?.ApartmentClassName == null ? null : _selectedApartment.ApartmentClassName;
                //SelectedLocation = _selectedApartment?.BuildingLocation == null ? null : _selectedApartment.BuildingLocation;
                NotifyOfPropertyChange(() => SelectedWaitingApplication);
            }
        }
        #endregion

        #region Eligible waiting application based on First-Come-First-Serve


        private WaitingApplicationResponse? _eligibleWaitingApplicationToProceed;

        public WaitingApplicationResponse? EligibleWaitingApplicationToProceed
        {
            get { return _eligibleWaitingApplicationToProceed; }
            set
            {
                _eligibleWaitingApplicationToProceed = value;
                EligibleWaitingApplicationId = _eligibleWaitingApplicationToProceed == null ? null : _eligibleWaitingApplicationToProceed.Id;
                EligibleApplicationCreatedDate = _eligibleWaitingApplicationToProceed == null ? null : _eligibleWaitingApplicationToProceed.CreatedOn;
             
                NotifyOfPropertyChange(() => EligibleWaitingApplicationToProceed);
            }
        }

        private int? _eligibleWaitingApplicationId;

        public int? EligibleWaitingApplicationId
        {
            get { return _eligibleWaitingApplicationId; }
            set
            {
                _eligibleWaitingApplicationId = value;
                NotifyOfPropertyChange(() => EligibleWaitingApplicationId);
            }
        }

        private DateTime? _eligibleApplicationCreatedDate;

        public DateTime? EligibleApplicationCreatedDate
        {
            get { return _eligibleApplicationCreatedDate; }
            set
            {
                _eligibleApplicationCreatedDate = value;
                NotifyOfPropertyChange(() => EligibleApplicationCreatedDate);
            }
        }
        #endregion
    }
}

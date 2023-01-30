﻿using AppartmentLeaseApp.Interfaces;
using AppartmentLeaseApp.Models.Apartments;
using AppartmentLeaseApp.Models.Customers;
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
    public class ChiefOccupantDependantManagementViewModel : Screen
    {
        private LeaseAgreementSummaryResponse _selectedLeaseAgreementSummary;
        private ILoggedInUser _loggedInUser;
        private ILeaseAgreementManagementEndpoint _leaseAgreementManagementEndpoint;
        private List<string> _relationships = new()
        {
            "Spouse",
            "Child",
            "Parent",
            "Servant",
            "Friend"
        };

        public ChiefOccupantDependantManagementViewModel(LeaseAgreementSummaryResponse selectedLeaseAgreementSummary, ILoggedInUser loggedInUser,
            ILeaseAgreementManagementEndpoint leaseAgreementManagementEndpoint)
        {
            _selectedLeaseAgreementSummary = selectedLeaseAgreementSummary;
            _loggedInUser = loggedInUser;
            _leaseAgreementManagementEndpoint = leaseAgreementManagementEndpoint;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            //set Active Lease agreement details
            AgreementId = _selectedLeaseAgreementSummary.AgreementId;
            AgreementStatus = _selectedLeaseAgreementSummary.Status;
            DownPaymentStatus = _selectedLeaseAgreementSummary.DownPaymentStatus;
            ApartmentBuildingLocation = @$"{_selectedLeaseAgreementSummary.BuildingName} - {_selectedLeaseAgreementSummary.BuildingLocation}";
            MaximumOccupants = _selectedLeaseAgreementSummary.MaximumOccupantCount;

            RelationshipsToLoggedOccupant = new BindableCollection<string>(_relationships);

            await LoadList();
        }

        private async Task LoadList()
        {
            var loggedUserId = _loggedInUser.Id;
            var list = await _leaseAgreementManagementEndpoint.GetDependantsByLoggedUser(loggedUserId, _selectedLeaseAgreementSummary.AgreementId);
            DependantsList = new BindingList<DependantResponseModel>(list);
        }

        #region Active Lease Agreement section
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

        private string _downPaymentStatus;
        public string DownPaymentStatus
        {
            get { return _downPaymentStatus; }
            set
            {
                _downPaymentStatus = value;
                NotifyOfPropertyChange(() => DownPaymentStatus);
            }
        }

        private string _apartmentBuildingLocation;
        public string ApartmentBuildingLocation
        {
            get { return _apartmentBuildingLocation; }
            set
            {
                _apartmentBuildingLocation = value;
                NotifyOfPropertyChange(() => ApartmentBuildingLocation);
            }
        }

        private int _maximumOccupants;
        public int MaximumOccupants
        {
            get { return _maximumOccupants; }
            set
            {
                _maximumOccupants = value;
                NotifyOfPropertyChange(() => MaximumOccupants);
            }
        }
        #endregion

        #region Dependants List column
        private BindingList<DependantResponseModel>? _dependantsList;

        public BindingList<DependantResponseModel>? DependantsList
        {
            get { return _dependantsList; }
            set
            {
                _dependantsList = value;
                NotifyOfPropertyChange(() => DependantsList);
            }
        }

        private DependantResponseModel? _selectedDependant;

        public DependantResponseModel? SelectedDependant
        {
            get { return _selectedDependant; }
            set
            {
                _selectedDependant = value;
                SelectedDependantId = _selectedDependant == null ? null : _selectedDependant.Id;
                DependantFullName = _selectedDependant == null ? null : _selectedDependant.FullName;
                SelectedRelationship = _selectedDependant == null ? null : _selectedDependant.Relationship;
                NotifyOfPropertyChange(() => SelectedDependant);
                NotifyOfPropertyChange(() => IsEditMode);
                NotifyOfPropertyChange(() => IsCreateMode);
            }
        }
        #endregion

        #region Manage Dependant column
        private string? _dependantFullName;
        public string? DependantFullName
        {
            get { return _dependantFullName; }
            set
            {
                _dependantFullName = value;
                NotifyOfPropertyChange(() => DependantFullName);
            }
        }

        private BindableCollection<string>? _relationshipsToLoggedOccupant;

        public BindableCollection<string>? RelationshipsToLoggedOccupant
        {
            get { return _relationshipsToLoggedOccupant; }
            set
            {
                _relationshipsToLoggedOccupant = value;
                NotifyOfPropertyChange(() => RelationshipsToLoggedOccupant);
            }
        }

        private string? _selectedRelationship;

        public string? SelectedRelationship
        {
            get { return _selectedRelationship; }
            set 
            {
                _selectedRelationship = value;
                NotifyOfPropertyChange(() => SelectedRelationship);
            }
        }

        private int? _selectedDependantId;

        public int? SelectedDependantId
        {
            get { return _selectedDependantId; }
            set
            {
                _selectedDependantId = value;
                NotifyOfPropertyChange(() => SelectedDependantId);
            }
        }
        
        public bool IsEditMode
        {
            get 
            { 
                bool output = false;
                if (SelectedDependant != null)
                {
                    output = true;
                }

                return output;
            }
        }

        public bool IsCreateMode
        {
            get
            {
                bool output = true;
                if (SelectedDependant != null)
                {
                    output = false;
                }

                return output;
            }
        }

        public void AddDependant()
        {
            var value = SelectedRelationship;//null
            var val2 = SelectedDependant;//null
            var val3 = SelectedDependantId;//0
            var val4 = DependantFullName;//null
        }

        public void ClearSelection()
        {
            SelectedDependant = null;
        }
        #endregion
    }
}
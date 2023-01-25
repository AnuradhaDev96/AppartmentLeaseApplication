using AppartmentLeaseApp.Interfaces;
using AppartmentLeaseApp.Models.Apartments;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppartmentLeaseApp.ViewModels
{
    public class ApartmentClassesViewModel : Screen
    {
        private IApartmentManagementEndpoint _apartmentManagementEndpoint;
        private List<ApartmentClassFacilitiesResponse>? apList;

        public ApartmentClassesViewModel(IApartmentManagementEndpoint apartmentManagementEndpoint)
        {
            _apartmentManagementEndpoint = apartmentManagementEndpoint;

        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadList();
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            return base.OnActivateAsync(cancellationToken);
        }

        protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            return base.OnDeactivateAsync(close, cancellationToken);
        }

        public async Task LoadList()
        {
            //var list = await _apartmentManagementEndpoint.GetAppartmentClassesWithFacilities();
            apList = await _apartmentManagementEndpoint.GetAppartmentClassesWithFacilities();
            ApartmentClassList = new BindingList<ApartmentClassFacilitiesResponse>(apList);
            //FacilityNames = new BindableCollection<string>(list[0].AvailableFacilites);
        }

        private BindingList<ApartmentClassFacilitiesResponse> _apartmentClassList;

        public BindingList<ApartmentClassFacilitiesResponse> ApartmentClassList
        {
            get { return _apartmentClassList; }
            set
            {
                _apartmentClassList = value;
                NotifyOfPropertyChange(() => ApartmentClassList);
            }
        }

        private BindableCollection<string>? _facilityNames;

        public BindableCollection<string>? FacilityNames
        {
            get { return _facilityNames; }
            set 
            {
                _facilityNames = value;
                NotifyOfPropertyChange(() => FacilityNames);
            }
        }

        private int _selectedItem = 0;

        public int SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                if (_selectedItem < 0)
                {
                    FacilityNames = null;
                } else
                {
                    FacilityNames = new BindableCollection<string>(apList?[_selectedItem].AvailableFacilites);
                }
                NotifyOfPropertyChange(() => SelectedItem);                
            }
        }

    }
}

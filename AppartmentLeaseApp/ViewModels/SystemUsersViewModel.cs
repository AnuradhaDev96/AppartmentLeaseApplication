using AppartmentLeaseApp.Interfaces;
using AppartmentLeaseApp.Models;
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
    public class SystemUsersViewModel : Screen
    {
        private IUserManagementEndpoint _userManagementEndpoint;

        public SystemUsersViewModel(IUserManagementEndpoint userManagementEndpoint)
        {
            _userManagementEndpoint = userManagementEndpoint;            
            
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadList();
        }

        public async Task LoadList()
        {
            var list = await _userManagementEndpoint.GetAllUsers();
            UsersList = new BindingList<SystemUser>(list);
        }

        private BindingList<SystemUser> _usersList2;

        public BindingList<SystemUser> UsersList2
        {
            get { return _usersList2; }
            set 
            {
                _usersList2 = value;
                NotifyOfPropertyChange(() => UsersList2);
            }
        }

        private BindingList<SystemUser> _usersList;

        public BindingList<SystemUser> UsersList
        {
            get { return _usersList; }
            set
            {
                _usersList = value;
                UsersList2 = value;
                NotifyOfPropertyChange(() => UsersList);
            }
        }

        private SystemUser _selectedUser;

        public SystemUser SelectedUser
        {
            get { return _selectedUser; }
            set 
            {
                _selectedUser = value;
                NotifyOfPropertyChange(() => SelectedUser);
            }
        }
    }
}

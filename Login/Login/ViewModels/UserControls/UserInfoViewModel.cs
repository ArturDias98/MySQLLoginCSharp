using Caliburn.Micro;
using Login.Commom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.ViewModels.UserControls
{
    public class UserInfoViewModel : Screen
    {
        private bool _editEnable;
        private string? _userProfileBtn;

        private string _userName;
        private string _email;
        private string _profile;

        private DatabaseModel _databaseModel;
        public UserInfoViewModel(DatabaseModel model)
        {
            _databaseModel = model;

            UserProfileBtn = EditEnable ? "Save" : "Edit";

            UserName = _databaseModel.UserName;
            Email = _databaseModel.Email;
            Profile = _databaseModel.Profile;

        }
        public async void EditClick()
        {
            if (EditEnable)
            {
                _databaseModel.UserName = UserName;
                _databaseModel.Email = Email;
                _databaseModel.Profile = Profile;
                var result = await Service.Service.UpdateAccount(_databaseModel);
            }
            EditEnable = !EditEnable;
        }

        public bool EditEnable
        {
            get { return _editEnable; }
            set
            {
                _editEnable = value;
                UserProfileBtn = _editEnable ? "Save" : "Edit";
                NotifyOfPropertyChange(() => EditEnable);
            }
        }

        public string? UserProfileBtn
        {
            get { return _userProfileBtn; }
            set
            {
                _userProfileBtn = value;
                NotifyOfPropertyChange(() => UserProfileBtn);
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyOfPropertyChange(() => Email);
            }
        }

        public string Profile
        {
            get { return _profile; }
            set
            {
                _profile = value;
                NotifyOfPropertyChange(() => Profile);
            }
        }


    }
}

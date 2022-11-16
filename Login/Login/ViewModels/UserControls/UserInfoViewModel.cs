using Caliburn.Micro;
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
        public UserInfoViewModel()
        {
            UserProfileBtn = EditEnable ? "Save" : "Edit";
        }
        public void EditClick()
        {
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


    }
}

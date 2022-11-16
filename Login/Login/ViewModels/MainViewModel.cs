using Caliburn.Micro;
using Login.ViewModels.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.ViewModels
{
    public class MainViewModel : Conductor<object>.Collection.OneActive
    {
        private bool _isCreatingNewAccount;
        private bool _showingUserInfo;

        public MainViewModel()
        {

        }

        protected async override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await ActivateItemAsync(new LogInViewModel());
        }

        public async void NewAccountClick()
        {
            IsCreatingNewAccount = true;
            await ActivateItemAsync(new CreateAccountViewModel());
        }
        public async void BackClick()
        {
            IsCreatingNewAccount = false;
            ShowingUserInfo = false;
            await ActivateItemAsync(new LogInViewModel());
        }

        public async void LogInClcik()
        {
            ShowingUserInfo = true;
            await ActivateItemAsync(new UserInfoViewModel());
        }

        public bool IsCreatingNewAccount
        {
            get { return _isCreatingNewAccount; }
            set
            {
                _isCreatingNewAccount = value;
                NotifyOfPropertyChange(() => IsCreatingNewAccount);
            }
        }
        public bool ShowingUserInfo
        {
            get { return _showingUserInfo; }
            set
            {
                _showingUserInfo = value;
                NotifyOfPropertyChange(() => ShowingUserInfo);
            }
        }

    }
}

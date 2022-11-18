using Caliburn.Micro;
using Login.Commom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Login.ViewModels.UserControls
{
    public class LogInViewModel : Screen
    {
        private string _email;
        private string _password;

        public LogInViewModel()
        {

        }

        public void NewAccountClick()
        {
            OnCreateNewAccount?.Invoke();
        }
        public async void LogInClcik(PasswordBox passwordBox)
        {
            Password = passwordBox.Password;

            var model = await Service.Service.SelectAccount(new AccountModel
            {
                Email = Email,
                Password = Password
            });

            if (model != null)
            {
                OnLogIn?.Invoke(model);
            }
            
        }
        public void Clear()
        {
            Email = string.Empty;
            Password = string.Empty;    
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

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }

        public event System.Action OnCreateNewAccount;
        public event System.Action<DatabaseModel> OnLogIn;
    }

}

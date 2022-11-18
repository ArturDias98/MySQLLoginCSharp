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
    public class CreateAccountViewModel : Screen
    {
        private string _userName;
        private string _email;
        private string _password;

        public CreateAccountViewModel()
        {

        }

        public async void SaveAccount(PasswordBox passwordBox)
        {
            Password = passwordBox.Password;

            var model = new AccountModel
            {
                UserName = UserName,
                Email = Email,
                Password = Password,
            };

            var result = await Service.Service.CreateAccount(model);

            if (result != null)
            {
                OnCreateAccount?.Invoke();
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

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }

        public event System.Action OnCreateAccount;
    }
}

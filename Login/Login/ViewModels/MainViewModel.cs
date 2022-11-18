using Caliburn.Micro;
using Login.Commom.Models;
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
        private readonly LogInViewModel logInViewModel;
        private readonly CreateAccountViewModel createAccountViewModel;
        public MainViewModel()
        {
            LoggerVM = new LoggerViewModel();

            logInViewModel = new LogInViewModel();
            logInViewModel.OnCreateNewAccount += OnCreateNewAccount;
            logInViewModel.OnLogIn += LogInClcik;

            createAccountViewModel = new CreateAccountViewModel();
            createAccountViewModel.OnCreateAccount += CreateAccountViewModel_OnCreateAccount;

            Service.Service.OnServiceReport += Service_OnServiceReport;
        }

        private void Service_OnServiceReport(LoggerModel obj)
        {
            LoggerVM.Update(obj);
        }

        private async void CreateAccountViewModel_OnCreateAccount()
        {
            logInViewModel.Clear(); 
            await ActivateItemAsync(logInViewModel);
        }
        private async void OnCreateNewAccount()
        {
            await ActivateItemAsync(createAccountViewModel);
        }
        private async void LogInClcik(DatabaseModel model)
        {
            await ActivateItemAsync(new UserInfoViewModel(model));
        }

        protected async override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            await ActivateItemAsync(logInViewModel);

            await Service.Service.OpenConnection();
        }


        public async void BackClick()
        {
            await ActivateItemAsync(logInViewModel);
        }

        public LoggerViewModel LoggerVM { get; set; }


    }
}

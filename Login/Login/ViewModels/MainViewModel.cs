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
        public MainViewModel()
        {

        }

        protected async override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await ActivateItemAsync(new LogInViewModel());
        }
    }
}

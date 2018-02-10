using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Bizland.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
        }

        public Command ClosePageCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await NavigationService.GoBackAsync(useModalNavigation: true);
                });
            }
        }
    }
}

using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Bizland.ViewModels
{
    public class MenuPageViewModel : ViewModelBase
    {
        public MenuPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }
        public Command NavigateCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await NavigationService.NavigateAsync("BaseNavigationPage/MainPage");
                });
            }
        }
    }
}

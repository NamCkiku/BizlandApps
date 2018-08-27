using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace Bizland.ViewModels
{
    public class SelectAddressPageViewModel : ViewModelBase
    {
        public INavigationService _navigationService { get; }
        public SelectAddressPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Nhập địa chỉ của bạn";
            _navigationService = navigationService;

        }
    }
}

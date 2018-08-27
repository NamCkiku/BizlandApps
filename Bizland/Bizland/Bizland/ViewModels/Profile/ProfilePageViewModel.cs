using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bizland.ViewModels
{
	public class ProfilePageViewModel : ViewModelBase
	{
        public ProfilePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Thông tin cá nhân";
        }
    }
}

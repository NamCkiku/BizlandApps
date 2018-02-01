using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bizland.ViewModels
{
    public class RootPageViewModel : ViewModelBase
    {
        public RootPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }
    }
}

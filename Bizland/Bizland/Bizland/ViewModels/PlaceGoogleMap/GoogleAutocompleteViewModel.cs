using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bizland.ViewModels
{
	public class GoogleAutocompleteViewModel : ViewModelBase
    {
        public GoogleAutocompleteViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Google";

        }
    }
}

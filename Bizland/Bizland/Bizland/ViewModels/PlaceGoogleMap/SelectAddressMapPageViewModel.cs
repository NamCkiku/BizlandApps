﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bizland.ViewModels
{
	public class SelectAddressMapPageViewModel : ViewModelBase
    {
        public SelectAddressMapPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Bản đồ";

        }
    }
}

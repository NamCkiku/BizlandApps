using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bizland.Views
{
    public partial class BaseNavigationPage
       : NavigationPage, INavigationPageOptions, IDestructible
    {
        public bool ClearNavigationStackOnNavigation
        {
            get { return true; }
        }

        public BaseNavigationPage()
        {
            InitializeComponent();
        }

        public void Destroy()
        {
        }
    }
}
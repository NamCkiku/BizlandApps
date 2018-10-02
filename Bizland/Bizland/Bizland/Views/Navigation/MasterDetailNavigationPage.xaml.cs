using Prism.Navigation;
using Xamarin.Forms;

namespace Bizland.Views
{
    public partial class MasterDetailNavigationPage : NavigationPage, INavigationPageOptions, IDestructible
    {
        public bool ClearNavigationStackOnNavigation
        {
            get { return true; }
        }
        public MasterDetailNavigationPage()
        {
            InitializeComponent();
        }

        public void Destroy()
        {

        }
    }
}

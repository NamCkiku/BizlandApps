using Prism.Navigation;
using Xamarin.Forms;

namespace Bizland.Views
{
    public partial class RootPage : MasterDetailPage, IMasterDetailPageOptions
    {
        public RootPage()
        {
            InitializeComponent();
        }
        public bool IsPresentedAfterNavigation
        {
            get { return false; }
        }
    }
}
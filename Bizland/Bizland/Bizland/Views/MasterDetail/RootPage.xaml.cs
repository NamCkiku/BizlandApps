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
        public bool IsPresentedAfterNavigation => Device.Idiom != TargetIdiom.Phone;

        private void Menu_Button_ClickedOnMenu(object sender, System.EventArgs e)
        {
            IsPresented = false;
        }
    }
}
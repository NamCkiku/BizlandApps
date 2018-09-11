using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace Bizland.Views
{
    public partial class RoomTypePage : PopupPage
    {
        public RoomTypePage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        protected override bool OnBackgroundClicked()
        {
            return false;
        }
    }
}

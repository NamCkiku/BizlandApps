using Rg.Plugins.Popup.Pages;

namespace Bizland.Views
{
    public partial class SelectDatetimePage : PopupPage
    {
        public SelectDatetimePage()
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

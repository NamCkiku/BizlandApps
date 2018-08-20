using Bizland.Core;
using Xamarin.Forms;

namespace Bizland.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            btnLogin.Clicked += BtnLogin_Clicked;
        }

        private void BtnLogin_Clicked(object sender, System.EventArgs e)
        {
            DependencyService.Get<IAccountKitService>().LoginWithAccountKit();
        }
    }
}

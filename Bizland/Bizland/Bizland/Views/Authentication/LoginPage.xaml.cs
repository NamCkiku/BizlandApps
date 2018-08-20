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
            var result = DependencyService.Get<IAccountKitService>().LoginWithAccountKit(LoginType.Phone, ResponseType.AuthorizationCode);
            if (result.IsCompleted)
            {
                string a = "";
            };
        }
    }
}

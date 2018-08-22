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

        private async void BtnLogin_Clicked(object sender, System.EventArgs e)
        {
            //var result = await DependencyService.Get<IAccountKitService>().LoginWithAccountKit(LoginType.Phone, ResponseType.AuthorizationCode);
            //if (result.IsSuccessful)
            //{
            //};

            var path = await CameraHelper.TakePhotoPathAsync();

            if (path != null)
            {
                image.Source = path;
            }
        }
    }
}

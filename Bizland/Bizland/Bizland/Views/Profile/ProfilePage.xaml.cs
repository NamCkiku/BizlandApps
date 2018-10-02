using Bizland.Core.Helpers;
using Xamarin.Forms;

namespace Bizland.Views
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            string localPath = await Core.Helpers.UploadImageHelper.UploadImage(ImageType.Avatar);

            txtAvatar.Source = localPath;
        }
    }
}

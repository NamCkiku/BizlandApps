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

        private void TapGestureRecognizer_Tapped_1(object sender, System.EventArgs e)
        {
            date.IsOpen = !date.IsOpen;
        }

        private void TapGestureRecognizer_Tapped_2(object sender, System.EventArgs e)
        {
            pickerSex.IsOpen = !pickerSex.IsOpen;
        }
    }
}

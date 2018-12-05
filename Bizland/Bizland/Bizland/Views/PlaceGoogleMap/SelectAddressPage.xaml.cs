using Bizland.Core;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace Bizland.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectAddressPage : ContentPage
    {
        public SelectAddressPage()
        {
            InitializeComponent();

            map.InitialCameraUpdate = CameraUpdateFactory.NewPositionZoom(new Xamarin.Forms.GoogleMaps.Position(Settings.Latitude, Settings.Longitude), 14d);
            map.UiSettings.MapToolbarEnabled = true;
            map.UiSettings.ZoomControlsEnabled = false;

            if (Device.RuntimePlatform == Device.Android)
            {
                map.UiSettings.MyLocationButtonEnabled = false;
            }
            if (Device.RuntimePlatform == Device.iOS)
            {
                map.UiSettings.MyLocationButtonEnabled = true;
            }
            map.Padding = new Thickness(0, 0, 0, 70);
            map.CameraMoving += Map_CameraMoving;
            map.CameraIdled += Map_CameraIdled;
        }

        private void Map_CameraIdled(object sender, CameraIdledEventArgs e)
        {
            btnSelectAddressMap.BackgroundColor = (Color)App.Current.Resources["Color_Button"];
        }

        private void Map_CameraMoving(object sender, CameraMovingEventArgs e)
        {
            btnSelectAddressMap.BackgroundColor = (Color)App.Current.Resources["Color_Text"];
        }
    }
}
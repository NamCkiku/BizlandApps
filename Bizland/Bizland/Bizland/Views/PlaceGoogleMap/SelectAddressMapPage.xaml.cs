using Bizland.Core;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace Bizland.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectAddressMapPage : ContentPage
    {
        public SelectAddressMapPage()
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
        }
    }
}
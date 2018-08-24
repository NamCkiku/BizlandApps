using Bizland.Core;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Bizland.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            //map.InitialCameraUpdate = CameraUpdateFactory.NewPositionZoom(new Xamarin.Forms.GoogleMaps.Position(Settings.Latitude, Settings.Longitude), 14d);
            //map.UiSettings.MapToolbarEnabled = true;
            //map.UiSettings.ZoomControlsEnabled = false;
            //map.UiSettings.MyLocationButtonEnabled = false;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}

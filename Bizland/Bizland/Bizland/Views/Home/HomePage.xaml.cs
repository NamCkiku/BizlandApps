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
            map.InitialCameraUpdate = CameraUpdateFactory.NewPositionZoom(new Xamarin.Forms.GoogleMaps.Position(Settings.Latitude, Settings.Longitude), 14d);
            map.UiSettings.MapToolbarEnabled = true;
            map.UiSettings.ZoomControlsEnabled = false;
            map.UiSettings.MyLocationButtonEnabled = false;

            string a = "[\n" +
                "  {\n" +
                "    \"elementType\": \"labels\",\n" +
                "    \"stylers\": [\n" +
                "      {\n" +
                "        \"visibility\": \"on\"\n" +
                "      }\n" +
                "    ]\n" +
                "  },\n" +
                "  {\n" +
                "    \"featureType\": \"water\",\n" +
                "    \"elementType\": \"geometry.fill\",\n" +
                "    \"stylers\": [\n" +
                "      {\n" +
                "        \"color\": \"#037cb3\"\n" +
                "      }\n" +
                "    ]\n" +
                "  }" +
                "]";

            map.MapStyle = MapStyle.FromJson(a);

            map.Padding = new Thickness(0, 0, 0, 170);

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }
        static bool isShowPage = false;
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (!isShowPage)
            {
                Plugin.Geolocator.Abstractions.Position position = await LocationHelper.GetGpsLocation();
                if (position != null)
                {
                    isShowPage = true;
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromMiles(10)).WithZoom(16));
                }
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        /// <summary>
        /// Bỏ qua khi nhấn nút Back cứng trên Android
        /// </summary>
        /// <Modified>
        /// Name     Date         Comments
        /// TrungTQ  23/11/2017   created
        /// </Modified>
        /// 
        bool bExit = true;
        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            if (!bExit)
                return false;

            // don't exit the app when back button is pressed
            Device.BeginInvokeOnMainThread(async () =>
            {
                bExit = await DisplayAlert("Đóng ứng dụng", "Bạn có thực sự muốn đóng ứng dụng không", "BỎ QUA", "ĐỒNG Ý");
                if (!bExit)
                    this.OnBackButtonPressed();
            });
            return bExit;
        }
    }
}

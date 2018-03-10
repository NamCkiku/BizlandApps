using Bizland.Core;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace Bizland.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            map.UiSettings.MyLocationButtonEnabled = true;
            map.UiSettings.ZoomControlsEnabled = true;
            map.UiSettings.CompassEnabled = true;
            map.UiSettings.ZoomControlsEnabled = true;
            map.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "VisibleRegion" && map.VisibleRegion != null) CalculateBoundingCoordinates(map.VisibleRegion);
            };
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
        private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (ToolbarItems.Count > 0)
            {
                DependencyService.Get<IToolbarItemBadgeService>().SetBadge(this, ToolbarItems.First(), $"{e.NewValue}", Color.Red, Color.White);
            }


        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        static void CalculateBoundingCoordinates(MapSpan region)
        {
            // WARNING: I haven't tested the correctness of this exhaustively!  
            var center = region.Center;
            var halfheightDegrees = region.LatitudeDegrees / 2;
            var halfwidthDegrees = region.LongitudeDegrees / 2;
            var left = center.Longitude - halfwidthDegrees;
            var right = center.Longitude + halfwidthDegrees;
            var top = center.Latitude + halfheightDegrees;
            var bottom = center.Latitude - halfheightDegrees;
            // Adjust for Internation Date Line (+/- 180 degrees longitude)  
            if (left < -180) left = 180 + (180 + left);
            if (right > 180) right = (right - 180) - 180;
            // I don't wrap around north or south; I don't think the map control allows this anyway  
        }
    }
}

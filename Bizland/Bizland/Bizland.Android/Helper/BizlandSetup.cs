
using Android.App;
using Android.OS;
using Plugin.Media;
using Xamarin.Forms.GoogleMaps.Android;

namespace Bizland.Droid.Helper
{
    public static class BizlandSetup
    {
        public static Activity Activity;

        public async static void Initialize(Activity _Activity, Bundle bundle)
        {
            Activity = _Activity;

            Rg.Plugins.Popup.Popup.Init(Activity, bundle);
            XamEffects.Droid.Effects.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);

            // Override default BitmapDescriptorFactory by your implementation. 
            var platformConfig = new PlatformConfig
            {
                BitmapDescriptorFactory = new CachingNativeBitmapDescriptorFactory()
            };

            Xamarin.FormsGoogleMaps.Init(Activity, bundle, platformConfig); // initialize for Xamarin.Forms.GoogleMaps

            Xamarin.FormsGoogleMapsBindings.Init(); // Add this line

            await CrossMedia.Current.Initialize();

            Plugin.Iconize.Iconize.Init(Resource.Id.toolbar, Resource.Id.sliding_tabs);


        }
    }
}
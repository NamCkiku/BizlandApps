using Bizland.Core;
using Lottie.Forms.iOS.Renderers;
using SegmentedControl.FormsPlugin.iOS;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.SfPicker.XForms.iOS;
using Xamarin.Forms.GoogleMaps.iOS;
using Xamarin.Forms.Platform.iOS;

namespace Bizland.iOS.Helpers
{
    public static class BizlandSetup
    {
        public static FormsApplicationDelegate AppDelegate;

        public static void Initialize(FormsApplicationDelegate _AppDelegate)
        {
            AppDelegate = _AppDelegate;

            Rg.Plugins.Popup.Popup.Init();
            // Override default ImageFactory by your implementation. 
            var platformConfig = new PlatformConfig
            {
                ImageFactory = new CachingImageFactory()
            };
            Xamarin.FormsGoogleMaps.Init(ServerConfig.GoogleMapKeyiOS, platformConfig);
            Xamarin.FormsGoogleMapsBindings.Init(); // Add this line
            AnimationViewRenderer.Init();

            SfPickerRenderer.Init();

            SfListViewRenderer.Init();

            Syncfusion.XForms.iOS.TabView.SfTabViewRenderer.Init();

            XamEffects.iOS.Effects.Init(); //write here

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();



            //Init segment
            SegmentedControlRenderer.Init();
        }
    }
}
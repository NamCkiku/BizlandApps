using Bizland.Core;
using Foundation;
using Lottie.Forms.iOS.Renderers;
using Naxam.Controls.Platform.iOS;
using Prism;
using Prism.Ioc;
using SegmentedControl.FormsPlugin.iOS;
using SupportWidgetXF.iOS;
using Syncfusion.SfPicker.XForms.iOS;
using UIKit;


namespace Bizland.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();
            global::Xamarin.Forms.Forms.Init();
            //Xamarin.FormsMaps.Init();
            Xamarin.FormsGoogleMaps.Init(ServerConfig.GoogleMapKeyiOS);
            Xamarin.FormsGoogleMapsBindings.Init(); // Add this line
            AnimationViewRenderer.Init();

            SfPickerRenderer.Init();
            SupportWidgetXFSetup.Initialize(this);

            LoadApplication(new App(new iOSInitializer()));

            //Init segment
            SegmentedControlRenderer.Init();

            TopTabbedRenderer.Init();

            UINavigationBar.Appearance.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            UINavigationBar.Appearance.ShadowImage = new UIImage();
            UINavigationBar.Appearance.BackgroundColor = UIColor.Clear;
            UINavigationBar.Appearance.TintColor = UIColor.Clear;
            UINavigationBar.Appearance.BarTintColor = UIColor.Clear;
            UINavigationBar.Appearance.Translucent = false;
            UINavigationBar.Appearance.BarStyle = UIBarStyle.Black;
            // Set the status bar to light
            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, true);

            return base.FinishedLaunching(app, options);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry container)
        {

        }
    }
}

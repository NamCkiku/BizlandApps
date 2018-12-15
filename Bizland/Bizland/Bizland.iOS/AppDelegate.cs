using Bizland.Core;
using Bizland.Interfaces;
using Bizland.iOS.DependencyService;
using Bizland.iOS.Helpers;
using Foundation;
using Prism;
using Prism.Ioc;
using UIKit;

namespace Bizland.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {

        static readonly HUDService hUDService = new HUDService();
        static readonly DisplayMessageService displayMessageService = new DisplayMessageService();
        static readonly TooltipService tooltipService = new TooltipService();

        static readonly BadgeService badgeService = new BadgeService();
        static readonly MediaService mediaService = new MediaService();
        static readonly PushLocalNotificationService pushLocalNotificationService = new PushLocalNotificationService();


        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {

            global::Xamarin.Forms.Forms.Init();

            BizlandSetup.Initialize(this);


            LoadApplication(new App(new iOSInitializer()));

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
        public class iOSInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry container)
            {
                container.RegisterInstance<IHUDProvider>(hUDService);
                container.RegisterInstance<IDisplayMessage>(displayMessageService);
                container.RegisterInstance<ITooltipService>(tooltipService);
                container.RegisterInstance<IBadge>(badgeService);
                container.RegisterInstance<IMediaService>(mediaService);
                container.RegisterInstance<IPushLocalNotification>(pushLocalNotificationService);
            }
        }
    }


}

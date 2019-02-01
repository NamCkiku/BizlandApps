using Android.App;
using Android.Content.PM;
using Android.OS;
using Bizland.Core;
using Bizland.Droid.DependencyService;
using Bizland.Droid.Helper;
using Bizland.Interfaces;
using Prism;
using Prism.Ioc;

namespace Bizland.Droid
{
    [Activity(Label = "Bizland", Icon = "@mipmap/ic_launcher",
        Theme = "@style/MainTheme",
        MainLauncher = false,
        LaunchMode = LaunchMode.SingleTop,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        static readonly HUDService hUDService = new HUDService();
        static readonly DisplayMessageService displayMessageService = new DisplayMessageService();
        static readonly TooltipService tooltipService = new TooltipService();
        static readonly BadgeService badgeService = new BadgeService();
        static readonly MediaService mediaService = new MediaService();
        static readonly PushLocalNotificationService pushLocalNotificationService = new PushLocalNotificationService();
        static AppVersionService appVersionService = new AppVersionService();


        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            BizlandSetup.Initialize(this, bundle);

            LoadApplication(new App(new AndroidInitializer()));
        }
        public class AndroidInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry container)
            {
                container.RegisterInstance<IHUDProvider>(hUDService);
                container.RegisterInstance<IDisplayMessage>(displayMessageService);
                container.RegisterInstance<ITooltipService>(tooltipService);
                container.RegisterInstance<IBadge>(badgeService);
                container.RegisterInstance<IMediaService>(mediaService);
                container.RegisterInstance<IPushLocalNotification>(pushLocalNotificationService);
                container.RegisterInstance<IAppVersionService>(appVersionService);
            }
        }
    }


}


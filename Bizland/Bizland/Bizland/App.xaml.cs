using Bizland.Core;
using Bizland.Interfaces;
using Bizland.ViewModels;
using Bizland.Views;
using BizlandApiService.IService;
using BizlandApiService.Service;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Bizland
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */

        public IHUDProvider _hud;
        static App _instance;

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer)
        {

            _instance = this;
        }

        public static App Instance
        {
            get
            {
                return _instance;
            }
        }

        public IHUDProvider Hud
        {
            get
            {
                return _hud ?? (_hud = DependencyService.Get<IHUDProvider>());
            }
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("/RootPage/BaseNavigationPage/HomePage");
        }
        protected async override void OnStart()
        {
            var mylocation = await LocationHelper.GetGpsLocation();
            // Handle when your app starts  
            CrossConnectivity.Current.ConnectivityChanged += HandleConnectivityChanged;
        }
        void HandleConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.IsConnected)
            {
                "Kết nối mạng ổn định".ToToast(ToastNotificationType.Info, null, 10);
            }
            else if (!e.IsConnected)
            {
                "Kết nối mạng không ổn định".ToToast(ToastNotificationType.Info, null, 10);
            }
        }
        protected override void OnResume()
        {

        }
        protected override void OnSleep()
        {

        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IRequestProvider, RequestProvider>();
            containerRegistry.Register<IChatServices, ChatServices>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MenuPage, MenuPageViewModel>("MenuPage");
            containerRegistry.RegisterForNavigation<RootPage, RootPageViewModel>("RootPage");
            containerRegistry.RegisterForNavigation<BaseNavigationPage, BaseNavigationPageViewModel>("BaseNavigationPage");
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>("MainPage");
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>("LoginPage");
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>("HomePage");
            containerRegistry.RegisterForNavigation<ChatPage, ChatPageViewModel>("ChatPage");
        }
    }
}

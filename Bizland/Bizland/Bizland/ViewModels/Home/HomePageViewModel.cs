using Bizland.Core;
using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Bindings;

namespace Bizland.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly IPageDialogService _dialogService;
        public HomePageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            Title = "Trang chủ";
            _dialogService = dialogService;

        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            getMylocation.Execute(null);
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {

        }
        public AnimateCameraRequest AnimateCameraRequest { get; } = new AnimateCameraRequest();
        private ObservableCollection<Pin> _Pins;
        private Pin _pin;
        public ObservableCollection<Pin> Pins
        {
            get { return _Pins; }
            set
            {
                _Pins = value;
                RaisePropertyChanged(() => Pins);
            }
        }
        public Pin Pin
        {
            get => _pin;
            set => SetProperty(ref _pin, value);
        }
        public Command clickaleart
        {
            get
            {
                return new Command(async () =>
                {

                    Pin = new Pin()
                    {
                        Type = PinType.Place,
                        Label = "Tokyo SKYTREE",
                        Address = "Sumida-ku, Tokyo, Japan",
                        Position = new Position(35.71d, 139.81d),
                        Icon = BitmapDescriptorFactory.FromBundle("ic_marker.png")
                    };

                    var pin2 = new Pin()
                    {
                        Type = PinType.Place,
                        Label = "Tokyo SKYTREE",
                        Address = "Sumida-ku, Tokyo, Japan",
                        Position = new Position(35.72d, 139.81d),
                        Icon = BitmapDescriptorFactory.FromBundle("ic_marker.png")
                    };
                    var pin3 = new Pin()
                    {
                        Type = PinType.Place,
                        Label = "Tokyo SKYTREE",
                        Address = "Sumida-ku, Tokyo, Japan",
                        Position = new Position(35.73d, 139.81d),
                        Icon = BitmapDescriptorFactory.FromBundle("ic_marker.png")
                    };
                    Pins?.Add(Pin);

                    Pins?.Add(pin2);

                    Pins?.Add(pin3);
                    await AnimateCameraRequest.AnimateCamera(CameraUpdateFactory.NewPosition(Pin.Position));
                });
            }
        }

        public Command<PinClickedEventArgs> PinSelectedCommand =>
        new Command<PinClickedEventArgs>(async (args) =>
        {
            await _dialogService.DisplayAlertAsync("adasdasdasda", "đâsdasdasdasd", "Oke");
        });

        public Command getMylocation
        {
            get
            {
                return new Command(async () =>
                {
                    var mylocation = await LocationHelper.GetGpsLocation();
                    Settings.Latitude = (float)mylocation.Latitude;
                    Settings.Longitude = (float)mylocation.Longitude;
                    await AnimateCameraRequest.AnimateCamera(CameraUpdateFactory.NewPosition(new Position(mylocation.Latitude, mylocation.Longitude)));
                });
            }
        }
        public Command PushToSelectAddress
        {
            get
            {
                return new Command(async () =>
                {
                    await NavigationService.NavigateAsync("/BaseNavigationPage/SelectAddressPage?createTab=GoogleAutocomplete&createTab=SelectAddressMapPage", useModalNavigation: true);
                });
            }
        }
    }
}

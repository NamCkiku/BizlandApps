using Bizland.Core;
using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;
using System.Linq;
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
            GetAddressesForPositionCommand.Execute(null);
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        #region Property
        public AnimateCameraRequest AnimateCameraRequest { get; } = new AnimateCameraRequest();
        private ObservableCollection<Pin> _Pins;
        public ObservableCollection<Pin> Pins
        {
            get { return _Pins; }
            set
            {
                _Pins = value;
                RaisePropertyChanged(() => Pins);
            }
        }

        private string _myAddress;
        public string MyAddress
        {
            get { return _myAddress; }
            set
            {
                _myAddress = value;
                RaisePropertyChanged(() => MyAddress);
            }
        }

        #endregion

        #region Command
        public Command clickaleart
        {
            get
            {
                return new Command(async () =>
                {


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

                    Pins?.Add(pin2);

                    Pins?.Add(pin3);
                    await AnimateCameraRequest.AnimateCamera(CameraUpdateFactory.NewPosition(pin2.Position));
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
                    await NavigationService.NavigateAsync("MasterDetailNavigationPage/SelectAddressPage?createTab=GoogleAutocomplete&createTab=SelectAddressMapPage", useModalNavigation: true);
                });
            }
        }


        public Command GetAddressesForPositionCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Geocoder geoCoder = new Geocoder();
                    var position = new Position(Settings.Latitude, Settings.Longitude);
                    var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);
                    MyAddress = possibleAddresses.ToList()[0] + "\n";
                });
            }
        }

        #endregion
    }
}

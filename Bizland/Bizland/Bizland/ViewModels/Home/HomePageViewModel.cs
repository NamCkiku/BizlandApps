using Bizland.Core;
using Bizland.Events;
using Bizland.Model;
using BizlandApiService.Service;
using Prism.Events;
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
        private readonly INavigationService _navigationService;
        private readonly IPlacesGeocode _placesGeocode;
        public HomePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IPlacesGeocode placesGeocode, IEventAggregator eventAggregator)
            : base(navigationService)
        {
            Title = "Trang chủ";
            this._dialogService = dialogService;
            this._navigationService = navigationService;
            this._placesGeocode = placesGeocode;

            eventAggregator.GetEvent<SelectMapAddressEvent>().Subscribe(UpdateMyAddress);

            eventAggregator.GetEvent<SelectRoomTypeEvent>().Subscribe(UpdateRoomType);

            GetAddressesForPositionCommand.Execute(null);
        }

        public async void UpdateMyAddress(SelectAddress param)
        {
            MyAddress = param.Address;
            Pins?.Clear();
            var pin2 = new Pin()
            {
                Type = PinType.Place,
                Label = param.Address,
                Address = param.Address,
                Position = param.Position,
                Icon = BitmapDescriptorFactory.FromBundle("ic_marker.png")
            };
            Pins?.Add(pin2);
            await AnimateCameraRequest.AnimateCamera(CameraUpdateFactory.NewPosition(param.Position));
        }

        public void UpdateRoomType(RoomType param)
        {
            if (param != null)
            {
                RoomType = param;
            }
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {

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

        private RoomType _roomType;
        public RoomType RoomType
        {
            get { return _roomType; }
            set
            {
                _roomType = value;
                RaisePropertyChanged(() => RoomType);
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
                return new Command(() =>
                {
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

        public Command PushModalRoomType
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigationService.NavigateAsync("RoomTypePage");
                });
            }
        }

        public Command GetAddressesForPositionCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var possibleAddresses = await _placesGeocode.GetAddressesForPosition(Settings.Latitude.ToString(), Settings.Longitude.ToString());
                    if (possibleAddresses != null && possibleAddresses.status == "OK")
                    {
                        var bestItem = 0;
                        foreach (var item in possibleAddresses.results)
                        {
                            if (item.formatted_address.Length > bestItem)
                            {
                                bestItem = item.formatted_address.Length;
                                MyAddress = item.formatted_address;
                            }
                        }
                    }
                    else
                    {
                        await _dialogService.DisplayAlertAsync("Thông báo !", "Không lấy được địa chỉ của bạn", "Đồng ý");
                    }
                });
            }
        }
        #endregion
    }
}

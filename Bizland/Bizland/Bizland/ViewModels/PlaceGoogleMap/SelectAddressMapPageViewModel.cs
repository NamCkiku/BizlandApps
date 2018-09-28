using Bizland.Core;
using Bizland.Events;
using Bizland.Model;
using BizlandApiService.Service;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Bindings;

namespace Bizland.ViewModels
{
    public class SelectAddressMapPageViewModel : ViewModelBase
    {
        private readonly IPageDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
        private readonly IPlacesGeocode _placesGeocode;
        public SelectAddressMapPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IEventAggregator eventAggregator, IPlacesGeocode placesGeocode)
            : base(navigationService)
        {
            Title = "Bản đồ";
            _navigationService = navigationService;
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
            this._placesGeocode = placesGeocode;
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }
        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            Pins?.Clear();
            var pin = new Pin()
            {
                Type = PinType.Place,
                Label = "",
                Address = "",
                IsDraggable = true,
                Position = new Position(Settings.Latitude, Settings.Longitude),
                Icon = BitmapDescriptorFactory.FromBundle("ic_my_marker.png")
            };
            Pins?.Add(pin);
            var result = GetAddressesForPosition(pin.Position);
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }
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


        public Command<CameraChangedEventArgs> MapCameraChanged
        {
            get
            {
                return new Command<CameraChangedEventArgs>(async (arg) =>
                {
                    try
                    {
                        if (arg != null)
                        {
                            var positon = new Xamarin.Forms.GoogleMaps.Position(arg.Position.Target.Latitude, arg.Position.Target.Longitude);
                            var result = await GetAddressesForPosition(positon);
                            if (!string.IsNullOrEmpty(result))
                            {
                                SetPinMap(positon);
                            }
                            else
                            {
                                "Không tìm thấy địa chỉ của bạn".ToToast(ToastNotificationType.Info, null, 10);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Logger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);
                    }
                });
            }
        }


        public Command<PinDragEventArgs> PinDragEndToCommand
        {
            get
            {
                return new Command<PinDragEventArgs>(async args =>
                {
                    try
                    {
                        var result = await GetAddressesForPosition(args.Pin.Position);
                        if (!string.IsNullOrEmpty(result))
                        {
                            SetPinMap(args.Pin.Position);
                        }
                        else
                        {
                            "Không tìm thấy địa chỉ của bạn".ToToast(ToastNotificationType.Info, null, 10);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);
                    }
                });
            }
        }

        public Command<MapClickedEventArgs> MapClickedToCommand
        {
            get
            {
                return new Command<MapClickedEventArgs>(async args =>
                {
                    try
                    {
                        var result = await GetAddressesForPosition(args.Point);
                        if (!string.IsNullOrEmpty(result))
                        {
                            SetPinMap(args.Point);
                        }
                        else
                        {
                            "Không tìm thấy địa chỉ của bạn".ToToast(ToastNotificationType.Info, null, 10);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);
                    }
                });
            }
        }

        public Command SelectAddress
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        var data = new SelectAddress
                        {
                            Address = MyAddress,
                            Position = Pins[0].Position
                        };
                        _eventAggregator.GetEvent<SelectMapAddressEvent>().Publish(data);
                        await _navigationService.GoBackAsync(useModalNavigation: true);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);
                    }
                });
            }
        }

        public Command getMylocation
        {
            get
            {
                return new Command(async () =>
                {
                    var mylocation = await LocationHelper.GetGpsLocation();
                    Pins?.Clear();
                    var pin = new Pin()
                    {
                        Type = PinType.Place,
                        Label = "",
                        Address = "",
                        IsDraggable = true,
                        Position = new Position(Settings.Latitude, Settings.Longitude),
                        Icon = BitmapDescriptorFactory.FromBundle("ic_my_marker.png")
                    };
                    Pins?.Add(pin);
                    var result = await GetAddressesForPosition(pin.Position);
                    await AnimateCameraRequest.AnimateCamera(CameraUpdateFactory.NewPosition(new Position(mylocation.Latitude, mylocation.Longitude)));
                });
            }
        }

        public Command<MyLocationButtonClickedEventArgs> MyLocationButtonClickedToCommand
        {
            get
            {
                return new Command<MyLocationButtonClickedEventArgs>(args =>
                {
                    try
                    {
                        getMylocation.Execute(null);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);
                    }
                });
            }
        }


        public async Task<string> GetAddressesForPosition(Position position)
        {
            var result = string.Empty;
            try
            {
                if (position.Latitude > 0 && position.Longitude > 0)
                {
                    Geocoder geoCoder = new Geocoder();
                    var possibleAddresses = await _placesGeocode.GetAddressesForPosition(position.Latitude.ToString(), position.Longitude.ToString());
                    if (possibleAddresses != null && possibleAddresses.status == "OK")
                    {
                        var bestItem = 0;
                        foreach (var item in possibleAddresses.results)
                        {
                            if (item.formatted_address.Length > bestItem)
                            {
                                bestItem = item.formatted_address.Length;
                                MyAddress = item.formatted_address;
                                result = item.formatted_address;
                            }
                        }
                    }
                    else
                    {
                        await _dialogService.DisplayAlertAsync("Thông báo !", "Không lấy được địa chỉ của bạn", "Đồng ý");
                    }

                }

            }
            catch (Exception ex)
            {
                Logger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);
            }
            return result;
        }

        private void SetPinMap(Position position)
        {
            Pins?.Clear();
            var pin = new Pin()
            {
                Type = PinType.Place,
                Label = "",
                Address = "",
                Position = position,
                IsDraggable = true,
                Icon = BitmapDescriptorFactory.FromBundle("ic_my_marker.png")
            };
            Pins?.Add(pin);
        }
    }
}

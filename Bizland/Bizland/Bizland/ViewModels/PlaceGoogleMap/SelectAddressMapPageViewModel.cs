using Bizland.Core;
using Bizland.Events;
using Bizland.Model;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
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
        public SelectAddressMapPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IEventAggregator eventAggregator)
            : base(navigationService)
        {
            Title = "Bản đồ";
            _navigationService = navigationService;
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
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
                            Pins?.Clear();
                            var pin = new Pin()
                            {
                                Type = PinType.Place,
                                Label = "",
                                Address = "",
                                IsDraggable = true,
                                Position = args.Pin.Position,
                                Icon = BitmapDescriptorFactory.FromBundle("ic_my_marker.png")
                            };
                            Pins?.Add(pin);
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
                            Pins?.Clear();
                            var pin = new Pin()
                            {
                                Type = PinType.Place,
                                Label = "",
                                Address = "",
                                Position = args.Point,
                                IsDraggable = true,
                                Icon = BitmapDescriptorFactory.FromBundle("ic_my_marker.png")
                            };
                            Pins?.Add(pin);
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
                    var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);
                    if (possibleAddresses != null && possibleAddresses.ToList().Count > 0)
                    {
                        var bestItem = 0;
                        foreach (var item in possibleAddresses)
                        {
                            if (item.Length > bestItem)
                            {
                                bestItem = item.Length;
                                MyAddress = item;
                                result = item;
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
    }
}

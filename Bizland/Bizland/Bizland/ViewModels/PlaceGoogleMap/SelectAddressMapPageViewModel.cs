using Bizland.Core;
using Prism.Navigation;
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
        public SelectAddressMapPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Bản đồ";

        }
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            var pin = new Pin()
            {
                Type = PinType.Place,
                Label = "",
                Address = "",
                Position = new Position(Settings.Latitude, Settings.Longitude),
                Icon = BitmapDescriptorFactory.FromBundle("ic_my_marker.png")
            };
            Pins?.Add(pin);
            await GetAddressesForPosition(pin.Position);
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
                                Position = new Position(Settings.Latitude, Settings.Longitude),
                                Icon = BitmapDescriptorFactory.FromBundle("ic_my_marker.png")
                            };
                            Pins?.Add(pin);

                            MyAddress = result;
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
                                Position = args.Pin.Position,
                                Icon = BitmapDescriptorFactory.FromBundle("ic_my_marker.png")
                            };
                            Pins?.Add(pin);

                            MyAddress = result;
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
                                Icon = BitmapDescriptorFactory.FromBundle("ic_my_marker.png")
                            };
                            Pins?.Add(pin);

                            MyAddress = result;
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
                        //var data = new SelectAddress
                        //{
                        //    Address = MyAddress,
                        //    Position = PinLocation.Position
                        //};
                        //string key = isStartAddress ? MessengerKeys.DataSelectStartAddress : MessengerKeys.DataSelectEndAddress;
                        //MessagingCenter.Send<SelectAddressViewModel, SelectAddress>(this, key, data);
                        //if (NavigationOnPage.ModalStack.Count > 0)
                        //{
                        //    await NavigationOnPage.PopModalAsync(true);
                        //}
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
                    Settings.Latitude = (float)mylocation.Latitude;
                    Settings.Longitude = (float)mylocation.Longitude;
                    var pin = new Pin()
                    {
                        Type = PinType.Place,
                        Label = "",
                        Address = "",
                        Position = new Position(Settings.Latitude, Settings.Longitude),
                        Icon = BitmapDescriptorFactory.FromBundle("ic_my_marker.png")
                    };
                    Pins?.Add(pin);
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
                    result = possibleAddresses.ToList()[0] + "\n";
                    MyAddress = result;
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

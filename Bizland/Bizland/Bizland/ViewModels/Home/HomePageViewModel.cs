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
        private readonly IEventAggregator _eventAggregator;
        public HomePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IPlacesGeocode placesGeocode, IEventAggregator eventAggregator)
            : base(navigationService)
        {
            Title = "Trang chủ";
            this._dialogService = dialogService;
            this._navigationService = navigationService;
            this._placesGeocode = placesGeocode;
            this._eventAggregator = eventAggregator;

            eventAggregator.GetEvent<SelectMapAddressEvent>().Subscribe(UpdateMyAddress);

            eventAggregator.GetEvent<SelectRoomTypeEvent>().Subscribe(UpdateRoomType);

            //GetAddressesForPositionCommand.Execute(null);

            Pins?.Clear();
            var pin1 = new Pin()
            {
                Type = PinType.Place,
                Position = new Position(Settings.Latitude, Settings.Longitude),
                Icon = BitmapDescriptorFactory.FromBundle("ic_marker.png")
            };
            var pin2 = new Pin()
            {
                Type = PinType.Place,
                Position = new Position(20.980283, 105.846792),
                Icon = BitmapDescriptorFactory.FromBundle("ic_marker.png")
            };
            var pin3 = new Pin()
            {
                Type = PinType.Place,
                Position = new Position(20.984891, 105.835033),
                Icon = BitmapDescriptorFactory.FromBundle("ic_marker.png")
            };
            var pin4 = new Pin()
            {
                Type = PinType.Place,
                Position = new Position(20.992104, 105.839238),
                Icon = BitmapDescriptorFactory.FromBundle("ic_marker.png")
            };
            Pins?.Add(pin1);
            Pins?.Add(pin2);
            Pins?.Add(pin3);
            Pins?.Add(pin4);
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
            _eventAggregator.GetEvent<SelectRoomTypeEvent>().Unsubscribe(null);

            _eventAggregator.GetEvent<SelectMapAddressEvent>().Unsubscribe(null);
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
                return new Command(async () =>
                {
                    await _navigationService.NavigateAsync("BaseNavigationPage/RoomDetailPage", null, useModalNavigation: true);
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
                    await AnimateCameraRequest.AnimateCamera(CameraUpdateFactory.NewPosition(new Position(Settings.Latitude, Settings.Longitude)));
                });
            }
        }
        public Command PushToSelectAddress
        {
            get
            {
                return new Command(async () =>
                {
                    if (IsBusy)
                    {
                        return;
                    }
                    IsBusy = true;
                    try
                    {
                        //await NavigationService.NavigateAsync("MasterDetailNavigationPage/SelectAddressPage?createTab=GoogleAutocomplete&createTab=SelectAddressMapPage", useModalNavigation: true);
                        await NavigationService.NavigateAsync("MasterDetailNavigationPage/SelectAddressPage", useModalNavigation: true);
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);
                    }
                    finally
                    {
                        IsBusy = false;
                    }

                });
            }
        }

        public Command PushModalRoomType
        {
            get
            {
                return new Command(async () =>
                {
                    if (IsBusy)
                    {
                        return;
                    }
                    IsBusy = true;
                    try
                    {
                        await _navigationService.NavigateAsync("RoomTypePage");
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);
                    }
                    finally
                    {
                        IsBusy = false;
                    }

                });
            }
        }

        public Command GetAddressesForPositionCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var mylocation = await LocationHelper.GetGpsLocation();
                    Settings.Latitude = (float)mylocation.Latitude;
                    Settings.Longitude = (float)mylocation.Longitude;
                    var possibleAddresses = await _placesGeocode.GetAddressesForPosition(mylocation.Latitude.ToString(), mylocation.Longitude.ToString());
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


        public Command FindRoomCommand
        {
            get
            {
                return new Command(() =>
                {
                    ShowMessage("Xin chào NamCkiku", 5);

                    ShowMessageInfo("Xin chào NamCkiku", 5);

                    Xamarin.Forms.DependencyService.Get<IBadge>().SetBadge(1,"đâsdasdasdas");

                });
            }
        }


        /** thời gian di chuyển mặc định của marker*/
        public double MARKER_MOVE_TIME = 2000;

        /** số lượng quay mặc định */
        public double MAX_ROTATE_STEP = 10;

        /**thời gian mỗi lần quay */
        public double MARKER_ROTATE_TIME_STEP = 50;

        //gán lại vòng quay
        private double mRotateIndex = 0;

        private void Rotate(double latitude,
         double longitude,
         Pin _pin,
         Action callback)
        {

            //gán lại vòng quay
            this.mRotateIndex = 0;

            // * tính góc quay giữa 2 điểm location
            var angle = GeoHelper.ComputeHeading(_pin.Position.Latitude, _pin.Position.Longitude, latitude, longitude);
            if (angle == 0)
            {
                callback();
                return;
            }

            //tính lại độ lệch góc
            var deltaAngle = GeoHelper.GetRotaion(_pin.Rotation, angle);

            var startRotaion = _pin.Rotation;

            Device.StartTimer(TimeSpan.FromMilliseconds(this.MARKER_ROTATE_TIME_STEP), () =>
            {
                //góc quay tiếp theo
                var fractionAngle = GeoHelper.ComputeRotation(
                  this.mRotateIndex / this.MAX_ROTATE_STEP,
                  startRotaion,
                  deltaAngle);
                this.mRotateIndex = mRotateIndex + 1;

                _pin.Rotation = (float)fractionAngle;
                //gán giá trị quay xe
                //SetRotateState(fractionAngle);

                if (this.mRotateIndex > this.MAX_ROTATE_STEP)
                {
                    callback();
                    return false;
                }

                return true;
            });
        }

        private void MarkerAnimation(double latitude, double longitude, Pin _pin, Action callback)
        {

            //gán lại vòng quay
            double mMoveIndex = 0;
            double MAX_MOVE_STEP = 20;
            var startPosition = _pin.Position;

            var finalPosition = new Position(latitude, longitude);
            double elapsed = 0;
            double t;
            double v;

            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                // Calculate progress using interpolator
                elapsed = elapsed + 100;
                t = elapsed / 2000;
                v = GeoHelper.GetetInterpolation(t);

                var postionnew = GeoHelper.Interpolate(v,
                    new Plugin.Geolocator.Abstractions.Position(startPosition.Latitude, startPosition.Longitude),
                    new Plugin.Geolocator.Abstractions.Position(latitude, longitude));

                mMoveIndex = mMoveIndex + 1;
                _pin.Position = new Position(postionnew.Latitude, postionnew.Longitude);
                if (mMoveIndex > MAX_MOVE_STEP)
                {
                    callback();
                    return false;
                }

                return true;
            });
        }
        #endregion
    }
}

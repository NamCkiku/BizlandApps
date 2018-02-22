using Bizland.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Bindings;

namespace Bizland.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        public HomePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Trang chủ";
        }


        private MapSpan _visibleRegion;
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
        public MapSpan VisibleRegion
        {
            get => _visibleRegion;
            set => SetProperty(ref _visibleRegion, value);
        }

        public MoveToRegionRequest Request { get; } = new MoveToRegionRequest();

        public AnimateCameraRequest AnimateCameraRequest { get; } = new AnimateCameraRequest();
        public Command MoveToTokyoCommand => new Command(() =>
        {
            //AnimateCameraRequest.AnimateCamera(CameraUpdateFactory.NewCameraPosition(
            //   new CameraPosition(
            //       new Position(35.681298, 139.766247), // Tokyo
            //       17d, // zoom
            //       45d, // bearing(rotation)
            //       60d // tilt
            //   )), TimeSpan.FromSeconds(VisibleRegion.Radius.Kilometers));

            Request.MoveToRegion(
                        MapSpan.FromCenterAndRadius(
                        new Position(35.681298, 139.766247),
                        Distance.FromKilometers(VisibleRegion.Radius.Kilometers)));

            var pin = new Pin()
            {
                Type = PinType.Place,
                Label = "dsadasdasd",
                Position = new Position(35.681298, 139.766247),
                IsDraggable = true,
                Tag = "dsadasda",
                Icon = BitmapDescriptorFactory.FromView(new BindingPinView("car0.png"))
            };
            Pins?.Add(pin);
        });
    }
}

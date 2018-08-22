﻿using Bizland.Interfaces;
using Bizland.Model;
using Bizland.Views;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Bindings;

namespace Bizland.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly IChatServices _chatServices;
        public HomePageViewModel(INavigationService navigationService, IChatServices chatservice)
            : base(navigationService)
        {
            Title = "Trang chủ";
            this._chatServices = chatservice;
            _chatServices.Connect();
            _chatServices.OnMessageReceived += _chatServices_OnMessageReceived;


            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private int _i = 0;

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _i++;
            Message = _i.ToString();
            var obj = (Timer)sender;
            if (obj != null && _i > 30)
            {
                obj.Stop();
                obj.Dispose();
            }
        }

        void _chatServices_OnMessageReceived(object sender, ChatMessage e)
        {
            Message2 = e.Message;
        }


        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged(() => Message);
            }
        }

        private string _message2;

        public string Message2
        {
            get { return _message2; }
            set
            {
                _message2 = value;
                RaisePropertyChanged(() => Message2);
            }
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
        public Command MoveToTokyoCommand => new Command(async () =>
        {
            using (new HUD("Xin chờ..."))
            {
                await _chatServices.Send(new ChatMessage { Name = "NamCkiku", Message = Message }, "NamCkiku");

                await AnimateCameraRequest.AnimateCamera(CameraUpdateFactory.NewPosition(new Position(35.681298, 139.766247)));


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
            }
        });


        public Command SendMessage => new Command(async () =>
        {
            await _chatServices.Send(new ChatMessage { Name = "NamCkiku", Message = Message }, "NamCkiku");
        });

        #region Join Room Command

        Command joinRoomCommand;

        /// <summary>
        /// Command to Send Message
        /// </summary>
        public Command JoinRoomCommand
        {
            get
            {
                return joinRoomCommand ??
                    (joinRoomCommand = new Command(ExecuteJoinRoomCommand));
            }
        }

        async void ExecuteJoinRoomCommand()
        {
            await _chatServices.JoinRoom("NamCkiku");
        }

        #endregion
    }
}

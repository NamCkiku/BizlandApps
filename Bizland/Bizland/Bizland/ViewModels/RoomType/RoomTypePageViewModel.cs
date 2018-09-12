using Bizland.ApiService;
using Bizland.Core;
using Bizland.Events;
using Bizland.Model;
using Prism.Events;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using Xamarin.Forms;

namespace Bizland.ViewModels
{
    public class RoomTypePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IRoomTypeService _roomTypeService;
        private readonly IEventAggregator _eventAggregator;
        public RoomTypePageViewModel(INavigationService navigationService, IRoomTypeService roomTypeService, IEventAggregator eventAggregator)
             : base(navigationService)
        {
            Title = "Thêm phòng";
            this._navigationService = navigationService;
            this._roomTypeService = roomTypeService;
            this._eventAggregator = eventAggregator;
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            //GetRoomTypeCommand.Execute(null);
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        private ObservableCollection<RoomType> _listRoomType = new ObservableCollection<RoomType>();
        public ObservableCollection<RoomType> ListRoomType
        {
            get { return _listRoomType; }
            set
            {
                _listRoomType = value;
                RaisePropertyChanged(() => ListRoomType);
            }
        }

        public Command GetRoomTypeCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        var listData = await _roomTypeService.GetAllRoomType();
                        if (listData != null && listData.Count > 0)
                        {
                            ListRoomType = listData.ToObservableCollection();
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);
                    }
                });
            }
        }
        public Command ClosePagePopupCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        await _navigationService.GoBackAsync();
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);
                    }
                });
            }
        }


        public Command SelectedCommand
        {
            get
            {
                return new Command<RoomType>(async (item) =>
                {
                    if (item != null)
                    {
                        try
                        {
                            _eventAggregator.GetEvent<SelectRoomTypeEvent>().Publish(item);
                            await _navigationService.GoBackAsync();
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);
                        }
                    }
                });
            }
        }
    }
}

using Bizland.ApiService;
using Bizland.Core;
using Bizland.Core.Helpers;
using Bizland.Events;
using Bizland.Model;
using Bizland.Views;
using Prism.Events;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using Xamarin.Forms;

namespace Bizland.ViewModels
{
    public class CreateRoomPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRoomTypeService _roomTypeService;
        public CreateRoomPageViewModel(INavigationService navigationService, IRoomTypeService roomTypeService, IEventAggregator eventAggregator)
             : base(navigationService)
        {
            this._navigationService = navigationService;
            this._eventAggregator = eventAggregator;
            this._roomTypeService = roomTypeService;
            Title = "Đăng phòng";

            eventAggregator.GetEvent<SelectProvinceEvent>().Subscribe(UpdateProvince);
            eventAggregator.GetEvent<SelectDistrictEvent>().Subscribe(UpdateDistrict);

            _roomName = new ValidatableObject<string>();
            _priceRoom = new ValidatableObject<int>();
            AddValidations();
        }
        #region private Method
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            GetRoomTypeCommand.Execute(null);
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        private void UpdateProvince(Province param)
        {
            Province = param;
        }
        private void UpdateDistrict(District param)
        {
            District = param;
        }

        private void AddValidations()
        {
            _roomName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Điền đầy đủ các thông tin để đăng phòng" });
            _priceRoom.Validations.Add(new IsNotNullOrEmptyRule<int> { ValidationMessage = "Điền đầy đủ các thông tin để đăng phòng" });
        }

        private bool Validate()
        {

            bool isValidRoomName = _roomName.Validate();
            bool isValidPrice = _priceRoom.Validate();

            return isValidRoomName && isValidPrice;
        }
        #endregion


        #region Property
        private Province _province;
        public Province Province
        {
            get { return _province; }
            set
            {
                _province = value;
                RaisePropertyChanged(() => Province);
            }
        }
        private District _district;
        public District District
        {
            get { return _district; }
            set
            {
                _district = value;
                RaisePropertyChanged(() => District);
            }
        }
        private Room _room;
        public Room Room
        {
            get { return _room; }
            set
            {
                _room = value;
                RaisePropertyChanged(() => Room);
            }
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


        private ValidatableObject<string> _roomName;
        public ValidatableObject<string> RoomName
        {
            get
            {
                return _roomName;
            }
            set
            {
                _roomName = value;
                RaisePropertyChanged(() => RoomName);
            }
        }
        private ValidatableObject<int> _priceRoom;
        public ValidatableObject<int> PriceRoom
        {
            get
            {
                return _priceRoom;
            }
            set
            {
                _priceRoom = value;
                RaisePropertyChanged(() => PriceRoom);
            }
        }
        #endregion

        #region Command

        public Command GetRoomTypeCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (RoomTypePage._listRoomTypeModel != null && RoomTypePage._listRoomTypeModel.Count > 0)
                        {
                            ListRoomType = RoomTypePage._listRoomTypeModel.ToObservableCollection();
                        }
                        else
                        {
                            var listData = await _roomTypeService.GetAllRoomType();
                            if (listData != null && listData.Count > 0)
                            {
                                RoomTypePage._listRoomTypeModel = listData;
                                ListRoomType = listData.ToObservableCollection();
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
        public Command PushToProvincePage
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
                        await _navigationService.NavigateAsync("BaseNavigationPage/ProvincePage", null, useModalNavigation: true);
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


        public Command PushToDistrictPage
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
                        NavigationParameters navigationParameters = new NavigationParameters();
                        navigationParameters.Add("provinceID", _province.Id);
                        await _navigationService.NavigateAsync("BaseNavigationPage/DistrictPage", navigationParameters, useModalNavigation: true);
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

        public Command UploadImageRoom
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
                        string localPath = await Core.Helpers.UploadImageHelper.UploadImage(ImageType.Room);
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



        public Command CreateRoom
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
                        var isvalid = Validate();
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


        #endregion



    }
}

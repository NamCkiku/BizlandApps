using Bizland.ApiService;
using Bizland.Core;
using Bizland.Core.Helpers;
using Bizland.Events;
using Bizland.Model;
using Bizland.Views;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
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
        private readonly IPageDialogService _dialogService;
        public CreateRoomPageViewModel(INavigationService navigationService, IRoomTypeService roomTypeService, IEventAggregator eventAggregator, IPageDialogService dialogService)
             : base(navigationService)
        {
            this._navigationService = navigationService;
            this._eventAggregator = eventAggregator;
            this._roomTypeService = roomTypeService;
            this._dialogService = dialogService;
            Title = "Đăng phòng";

            eventAggregator.GetEvent<SelectProvinceEvent>().Subscribe(UpdateProvince);
            eventAggregator.GetEvent<SelectDistrictEvent>().Subscribe(UpdateDistrict);

            _roomName = new ValidatableObject<string>();
            _priceRoom = new ValidatableObject<int>();
            _roomType = new ValidatableObject<RoomType>();
            _province = new ValidatableObject<Province>();
            _acreage = new ValidatableObject<int>();
            _phonenumber = new ValidatableObject<int>();
            _address = new ValidatableObject<string>();
            _note = new ValidatableObject<string>();
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
            Province.Value = param;
        }
        private void UpdateDistrict(District param)
        {
            District = param;
        }

        private void AddValidations()
        {
            _roomName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Điền đầy đủ các thông tin để đăng phòng" });
            _priceRoom.Validations.Add(new IntRule<int> { ValidationMessage = "Điền đầy đủ các thông tin để đăng phòng" });
            _roomType.Validations.Add(new ObjectNullRule<RoomType> { ValidationMessage = "Điền đầy đủ các thông tin để đăng phòng" });
            _province.Validations.Add(new ObjectNullRule<Province> { ValidationMessage = "Điền đầy đủ các thông tin để đăng phòng" });
            _acreage.Validations.Add(new IntRule<int> { ValidationMessage = "Điền đầy đủ các thông tin để đăng phòng" });
            _phonenumber.Validations.Add(new IntRule<int> { ValidationMessage = "Điền đầy đủ các thông tin để đăng phòng" });
            _address.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Điền đầy đủ các thông tin để đăng phòng" });
            _note.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Điền đầy đủ các thông tin để đăng phòng" });
        }

        private bool Validate()
        {

            bool isValidRoomName = _roomName.Validate();
            bool isValidPrice = _priceRoom.Validate();
            bool isValidRoomType = _roomType.Validate();
            bool isValidprovince = _province.Validate();
            bool isValidacreage = _acreage.Validate();
            bool isValidphonenumber = _phonenumber.Validate();
            bool isValidaddress = _address.Validate();
            bool isValidnote = _note.Validate();

            return isValidRoomName && isValidPrice && isValidRoomType && isValidprovince
                && isValidacreage && isValidphonenumber && isValidaddress && isValidnote;
        }
        #endregion


        #region Property
        private ValidatableObject<Province> _province;
        public ValidatableObject<Province> Province
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


        private ValidatableObject<RoomType> _roomType = null;
        public ValidatableObject<RoomType> RoomType
        {
            get { return _roomType; }
            set
            {
                _roomType = value;
                RaisePropertyChanged(() => RoomType);
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
        private ValidatableObject<int> _acreage;
        public ValidatableObject<int> Acreage
        {
            get
            {
                return _acreage;
            }
            set
            {
                _acreage = value;
                RaisePropertyChanged(() => Acreage);
            }
        }

        private ValidatableObject<int> _phonenumber;
        public ValidatableObject<int> PhoneNumber
        {
            get
            {
                return _phonenumber;
            }
            set
            {
                _phonenumber = value;
                RaisePropertyChanged(() => PhoneNumber);
            }
        }

        private ValidatableObject<string> _address;
        public ValidatableObject<string> Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                RaisePropertyChanged(() => Address);
            }
        }

        private ValidatableObject<string> _note;
        public ValidatableObject<string> Note
        {
            get
            {
                return _note;
            }
            set
            {
                _note = value;
                RaisePropertyChanged(() => Note);
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
                        if (Province != null && Province.Value.Id > 0)
                        {
                            NavigationParameters navigationParameters = new NavigationParameters();
                            navigationParameters.Add("provinceID", Province.Value.Id);
                            await _navigationService.NavigateAsync("BaseNavigationPage/DistrictPage", navigationParameters, useModalNavigation: true);
                        }
                        else
                        {
                            await _dialogService.DisplayAlertAsync("Thông báo !", "Hãy họn tỉnh thành trước khi chọn quận huyện", "Đồng ý");
                        }
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


        public Command SelectItemRoomTypeCommand
        {
            get
            {
                return new Command<object>((args) =>
                {
                    if (args != null)
                    {
                        if (IsBusy)
                        {
                            return;
                        }
                        IsBusy = true;
                        try
                        {
                            var eventArgs = (args as Syncfusion.ListView.XForms.ItemTappedEventArgs).ItemData as RoomType;
                            foreach (var item in ListRoomType)
                            {
                                if (item.Selected == Color.FromHex("#FE9D1A"))
                                {
                                    item.Selected = Color.White;
                                }
                            }
                            eventArgs.Selected = Color.FromHex("#FE9D1A");
                            RoomType.Value = eventArgs;
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);
                        }
                        finally
                        {
                            IsBusy = false;
                        }
                    }
                });
            }
        }

        #endregion



    }
}

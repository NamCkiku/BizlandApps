using Bizland.ApiService;
using Bizland.Core;
using Bizland.Model;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Bizland.Views
{
    public partial class RoomTypePage : PopupPage
    {
        private readonly IRoomTypeService _roomTypeService;
        public RoomTypePage(IRoomTypeService roomTypeService)
        {
            InitializeComponent();
            this._roomTypeService = roomTypeService;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (_listRoomTypeModel != null && _listRoomTypeModel.Count > 0)
            {
                listviewRoomType.ItemsSource = _listRoomTypeModel;
            }
            else
            {
                var lstRoomType = await GetListRoomType();
                if (lstRoomType != null && lstRoomType.Count > 0)
                {
                    _listRoomTypeModel = lstRoomType;
                    listviewRoomType.ItemsSource = lstRoomType;
                }
            }

        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        protected override bool OnBackgroundClicked()
        {
            return false;
        }

        static List<RoomType> _listRoomTypeModel { get; set; }

        public static List<RoomType> ListRoomType
        {
            get
            {
                if (_listRoomTypeModel != null)
                {
                    return _listRoomTypeModel;
                }
                else
                {
                    return new List<RoomType>();
                }
            }
        }
        public async Task<List<RoomType>> GetListRoomType()
        {
            List<RoomType> result = new List<RoomType>();
            try
            {
                var listData = await _roomTypeService.GetAllRoomType();
                if (listData != null && listData.Count > 0)
                {
                    result = listData;
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

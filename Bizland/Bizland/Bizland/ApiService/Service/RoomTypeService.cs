﻿using Bizland.Core;
using Bizland.Interfaces;
using Bizland.Model;
using BizlandApiService.IService;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Bizland.ApiService
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRequestProvider _IRequestProvider;
        public RoomTypeService(IRequestProvider IRequestProvider)
        {
            this._IRequestProvider = IRequestProvider;
        }
        public async Task<List<RoomType>> GetAllRoomType()
        {
            List<RoomType> result = new List<RoomType>();
            try
            {
                using (new HUD(""))
                {
                    var data = await _IRequestProvider.GetAsync<List<RoomType>>(ApiUri.GET_ROOMTYPE);
                    if (data != null)
                    {
                        result = data;

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

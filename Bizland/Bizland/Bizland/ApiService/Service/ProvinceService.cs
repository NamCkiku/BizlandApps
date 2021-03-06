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
    public class ProvinceService : IProvinceService
    {
        private readonly IRequestProvider _IRequestProvider;
        public ProvinceService(IRequestProvider IRequestProvider)
        {
            this._IRequestProvider = IRequestProvider;
        }

        public async Task<List<Province>> GetProvince()
        {
            List<Province> result = new List<Province>();
            try
            {
                using (new HUD(""))
                {
                    var data = await _IRequestProvider.GetAsync<List<Province>>(ApiUri.GET_PROVINCE);
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

        public async Task<List<District>> GetAllDistrict()
        {
            List<District> result = new List<District>();
            try
            {
                using (new HUD(""))
                {
                    var data = await _IRequestProvider.GetAsync<List<District>>(ApiUri.GET_DISTRICT);
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

        public async Task<List<Ward>> GetAllWard()
        {
            List<Ward> result = new List<Ward>();
            try
            {
                using (new HUD(""))
                {
                    var data = await _IRequestProvider.GetAsync<List<Ward>>(ApiUri.GET_WARD);
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

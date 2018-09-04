using Bizland.Core;
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

                    string uri = $"/api/management/getallprovince";
                    var data = await _IRequestProvider.GetAsync<List<Province>>(uri, "");
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

using Bizland.Core;
using Bizland.Interfaces;
using Bizland.Model;
using BizlandApiService.IService;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Bizland.ApiService
{
    public class ProfileService : IProfileService
    {
        private readonly IRequestProvider _IRequestProvider;
        public ProfileService(IRequestProvider IRequestProvider)
        {
            this._IRequestProvider = IRequestProvider;
        }

        public async Task<IdentityResult> UpdateInfomationUser(AppUser model)
        {
            IdentityResult result = new IdentityResult();
            try
            {
                using (new HUD(""))
                {
                    var data = await _IRequestProvider.PostAsync<AppUser, IdentityResult>(ApiUri.POST_UPDATEUSER, model, Settings.AuthAccessToken);
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

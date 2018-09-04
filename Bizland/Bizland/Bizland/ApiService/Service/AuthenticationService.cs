using Bizland.Core;
using Bizland.Interfaces;
using Bizland.Model;
using BizlandApiService.IService;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Bizland.ApiService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRequestProvider _IRequestProvider;
        public AuthenticationService(IRequestProvider IRequestProvider)
        {
            this._IRequestProvider = IRequestProvider;
        }
        public async Task<UserToken> GetTokenAsync(string userName, string password)
        {
            UserToken token = null;
            try
            {
                using (new HUD("Xin chờ..."))
                {
                    string data = string.Format("grant_type=password&username={0}&password={1}", userName, password);
                    var result = await _IRequestProvider.PostAsync<UserToken>(ApiUri.GET_TOKEN, data, "xamarin", "secret");
                    if (result != null)
                    {
                        token = result;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);
            }
            return token;
        }

        public Task<bool> Logout()
        {
            throw new System.NotImplementedException();
        }
    }
}

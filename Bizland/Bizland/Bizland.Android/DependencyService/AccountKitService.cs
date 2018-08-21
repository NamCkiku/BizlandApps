using Bizland.Core;
using Bizland.Droid.DependencyService;
using Bizland.Droid.FBAccountkit;
using Plugin.CurrentActivity;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(AccountKitService))]

namespace Bizland.Droid.DependencyService
{
    public class AccountKitService : IAccountKitService
    {
        readonly AccountKitAuth auth;

        public AccountKitService()
        {
            this.auth = new AccountKitAuth(() => CrossCurrentActivity.Current.Activity);
        }

        public Task<LoginAccount> GetCurrentAccount(ResponseType responseType)
        {
            return auth.GetCurrentAccount(responseType);
        }

        public Task<LoginResult> LoginWithAccountKit(LoginType loginType, ResponseType responseType)
        {
            return auth.LoginWithAccountKit(loginType, responseType);
        }
    }
}
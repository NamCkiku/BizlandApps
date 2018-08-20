using System.Threading.Tasks;
using Bizland.Core;
using Bizland.Droid.DependencyService;
using Bizland.Droid.FBAccountkit;
using Xamarin.Forms;

[assembly: Dependency(typeof(AccountKitService))]

namespace Bizland.Droid.DependencyService
{
    public class AccountKitService : IAccountKitService
    {
        readonly AccountKitAuth auth;

        public AccountKitService()
        {
            this.auth = new AccountKitAuth();
        }

        public Task<LoginAccount> GetCurrentAccount(ResponseType responseType)
        {
            throw new System.NotImplementedException();
        }

        public void LoginWithAccountKit()
        {
            //auth.LoginWithAccountKit(LoginType.Phone, AccountKitActivity.ResponseType.Code);

        }

        public Task<LoginResult> LoginWithAccountKit(LoginType loginType, ResponseType responseType)
        {
            throw new System.NotImplementedException();
        }
    }
}
using Bizland.Core;
using Bizland.iOS.DependencyService;
using Bizland.iOS.FBAccountkit;
using Facebook.Accountkit;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using NXLoginAccount = Bizland.Core.LoginAccount;
using NXLoginResult = Bizland.Core.LoginResult;
using NXLoginType = Bizland.Core.LoginType;
using NXResponseType = Bizland.Core.ResponseType;

[assembly: Dependency(typeof(AccountKitService))]
namespace Bizland.iOS.DependencyService
{
    public class AccountKitService : IAccountKitService
    {
        AccountKitAuth auth;
        public AccountKitService()
        {
            var theme = AKFTheme.ThemeWithPrimaryColor(
                UIColor.Purple,
                UIColor.White,
                UIColor.Magenta,
                UIColor.Purple,
                UIStatusBarStyle.LightContent
            );
            theme.BackgroundColor = UIColor.Yellow;
            this.auth = new AccountKitAuth(theme);

        }

        public Task<NXLoginAccount> GetCurrentAccount(NXResponseType responseType)
        {
            return auth.GetCurrentAccount(responseType);
        }


        public Task<NXLoginResult> LoginWithAccountKit(NXLoginType loginType, NXResponseType responseType)
        {
            return auth.LoginWithAccountKit(loginType, responseType);
        }
    }
}
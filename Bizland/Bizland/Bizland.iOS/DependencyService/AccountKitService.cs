using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bizland.Core.DependencyService;
using Bizland.iOS.DependencyService;
using Bizland.iOS.FBAccountkit;
using Facebook.Accountkit;
using Foundation;
using UIKit;
using Xamarin.Forms;

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
        public void LoginWithAccountKit()
        {

            auth.LoginWithAccountKit(AKFLoginType.Phone, AKFResponseType.AuthorizationCode);

        }
    }
}
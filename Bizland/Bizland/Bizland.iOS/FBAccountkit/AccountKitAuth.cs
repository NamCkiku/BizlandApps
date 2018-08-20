using Acr.Support.iOS;
using Facebook.Accountkit;
using Foundation;
using System.Threading.Tasks;
using UIKit;
using INXAccountKitAuth = Bizland.Core.IAccountKitService;
using NXLoginAccount = Bizland.Core.LoginAccount;
using NXLoginResult = Bizland.Core.LoginResult;
using NXLoginType = Bizland.Core.LoginType;
using NXResponseType = Bizland.Core.ResponseType;

namespace Bizland.iOS.FBAccountkit
{
    public class AccountKitAuth : NSObject, INXAccountKitAuth, IAKFViewControllerDelegate
    {
        readonly AKFTheme theme;

        AKFAccountKit accountKit;
        UIViewController pendingLoginViewController;

        public AccountKitAuth(AKFTheme theme)
        {
            this.theme = theme;
        }

        public Task<NXLoginAccount> GetCurrentAccount(NXResponseType responseType)
        {
            var taskCompletionSource = new TaskCompletionSource<NXLoginAccount>();

            InitAK(responseType == NXResponseType.AccessToken
                    ? AKFResponseType.AccessToken
                    : AKFResponseType.AuthorizationCode);

            accountKit.RequestAccount((obj, error) =>
            {
                if (error != null)
                {
                    taskCompletionSource.SetResult(null);
                    return;
                }

                var account = obj as IAKFAccount;

                var phoneNumber = account?.PhoneNumber?.stringRepresentation()?.ToString();

                taskCompletionSource.SetResult(new NXLoginAccount(obj == null, phoneNumber, account?.EmailAddress));
            });

            return taskCompletionSource.Task;
        }

        public Task<NXLoginResult> LoginWithAccountKit(NXLoginType type, NXResponseType responseType)
        {
            InitAK(responseType == NXResponseType.AccessToken
                   ? AKFResponseType.AccessToken
                   : AKFResponseType.AuthorizationCode, true);

            loginTaskCompletionSource?.TrySetCanceled();
            loginTaskCompletionSource = new TaskCompletionSource<NXLoginResult>();
            pendingLoginViewController = type == NXLoginType.Phone
                                                             ? accountKit.ViewControllerForPhoneLogin()
                                                             : accountKit.ViewControllerForEmailLogin();

            var loginViewController = (pendingLoginViewController as IAKFViewController);

            if (loginViewController != null)
            {
                loginViewController.WeakDelegate = this;
                loginViewController.EnableSendToFacebook = true;

                loginViewController.SetTheme(theme);
            }

            NSOperationQueue.MainQueue.BeginInvokeOnMainThread(() =>
            {
                var vcInContext = GetTopMostController();

                vcInContext.PresentViewController(pendingLoginViewController, true, null);
            });

            return loginTaskCompletionSource.Task;
        }

        UIViewController GetTopMostController()
        {
            var vc = UIApplication.SharedApplication.GetTopViewController();

            if (vc is AKFNavigationController)
            {
                vc = vc.PresentingViewController;
            }

            return vc;
        }

        void InitAK(AKFResponseType responseType, bool forced = false)
        {
            if (accountKit == null || forced)
            {
                accountKit = new AKFAccountKit(responseType);
            }
        }

        TaskCompletionSource<NXLoginResult> loginTaskCompletionSource;

        [Export("viewControllerDidCancel:")]
        public void DidCancel(UIViewController viewController)
        {
            loginTaskCompletionSource?.SetResult(new NXLoginResult(false, true));
            loginTaskCompletionSource = null;
        }

        [Export("viewController:didCompleteLoginWithAuthorizationCode:state:")]
        public void DidCompleteLoginWithAuthorizationCode(UIViewController viewController, string code, string state)
        {
            loginTaskCompletionSource?.SetResult(new NXLoginResult(true, false, code));
            loginTaskCompletionSource = null;
        }

        [Export("viewController:didCompleteLoginWithAccessToken:state:")]
        public void DidCompleteLoginWithAccessToken(UIViewController viewController, IAKFAccessToken accessToken, string state)
        {
            loginTaskCompletionSource?.SetResult(new NXLoginResult(true, false, accessToken.GetTokenString()));
            loginTaskCompletionSource = null;
        }

        [Export("viewController:didFailWithError:")]
        public void DidFailWithError(UIViewController viewController, NSError error)
        {
            loginTaskCompletionSource?.SetResult(new NXLoginResult(false));
            loginTaskCompletionSource = null;
        }
    }
}
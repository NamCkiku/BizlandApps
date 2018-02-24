using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facebook.Accountkit;
using Acr.Support.iOS;
using Foundation;
using UIKit;

namespace Bizland.iOS.FBAccountkit
{
    public class AccountKitAuth : NSObject, IAKFViewControllerDelegate
    {
        readonly AKFTheme theme;

        AKFAccountKit accountKit;
        UIViewController pendingLoginViewController;

        public AccountKitAuth(AKFTheme theme)
        {
            this.theme = theme;
        }

        public Task<IAKFAccount> GetCurrentAccount(AKFResponseType responseType)
        {
            var taskCompletionSource = new TaskCompletionSource<IAKFAccount>();

            InitAK(responseType);

            accountKit.RequestAccount((obj, error) =>
            {
                if (error != null)
                {
                    taskCompletionSource.SetResult(null);
                    return;
                }

                var account = obj as IAKFAccount;
                taskCompletionSource.SetResult(account);
            });

            return taskCompletionSource.Task;
        }

        public Task<string> LoginWithAccountKit(AKFLoginType type, AKFResponseType responseType)
        {
            InitAK(responseType);

            loginTaskCompletionSource?.TrySetCanceled();
            loginTaskCompletionSource = new TaskCompletionSource<string>();
            pendingLoginViewController = type == AKFLoginType.Phone
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

            //if (vc is AKFViewController) {
            //	vc = vc.PresentingViewController;
            //}

            return vc;
        }

        void InitAK(AKFResponseType responseType, bool forced = false)
        {
            if (accountKit == null || forced)
            {
                accountKit = new AKFAccountKit(responseType);
            }
        }

        TaskCompletionSource<string> loginTaskCompletionSource;

        [Export("viewControllerDidCancel:")]
        public void DidCancel(UIViewController viewController)
        {
            loginTaskCompletionSource?.SetResult(null);
            loginTaskCompletionSource = null;
        }

        [Export("viewController:didCompleteLoginWithAuthorizationCode:state:")]
        public void DidCompleteLoginWithAuthorizationCode(UIViewController viewController, string code, string state)
        {
            loginTaskCompletionSource?.SetResult(code);
            loginTaskCompletionSource = null;
        }

        [Export("viewController:didCompleteLoginWithAccessToken:state:")]
        public void DidCompleteLoginWithAccessToken(UIViewController viewController, IAKFAccessToken accessToken, string state)
        {
            var x = accessToken as IAKFAccessToken;

            loginTaskCompletionSource?.SetResult(x.GetTokenString());
            loginTaskCompletionSource = null;
        }

        [Export("viewController:didFailWithError:")]
        public void DidFailWithError(UIViewController viewController, NSError error)
        {
            loginTaskCompletionSource?.SetResult(null);
            loginTaskCompletionSource = null;
        }
    }
}
using BigTed;
using Bizland.Interfaces;
using Bizland.iOS.DependencyService;
using CRToast;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(HUDService))]
namespace Bizland.iOS.DependencyService
{
    public class HUDService : IHUDProvider
    {
        public async void DisplayProgress(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                BTProgressHUD.Show(null, -1, ProgressHUD.MaskType.Black);
            }
            else
            {
                BTProgressHUD.Show(message, -1, ProgressHUD.MaskType.Black);
            }
        }

        public void DisplaySuccess(string message)
        {
            BTProgressHUD.ShowSuccessWithStatus(message);
        }

        public void DisplayError(string message)
        {
            BTProgressHUD.ShowErrorWithStatus(message);
        }

        public void Dismiss()
        {
            BTProgressHUD.Dismiss();
        }

        public void ShowToast(string message, double time)
        {
            //BTProgressHUD.ShowToast(message, toastPosition: ProgressHUD.ToastPosition.Center, timeoutMs: time);
            CRToastManager.ShowNotificationWithOptions(
               Options(message, time),
               () =>
               {
               }, () =>
               {
               }
           );
        }

        NSDictionary Options(string message, double time)
        {
            var keys = new NSString[] {
                Constants.kCRToastNotificationTypeKey,
                Constants.kCRToastNotificationPresentationTypeKey,
                Constants.kCRToastUnderStatusBarKey,
                Constants.kCRToastTextKey,
                Constants.kCRToastTextAlignmentKey,
                Constants.kCRToastTimeIntervalKey,
                Constants.kCRToastAnimationInTypeKey,
                Constants.kCRToastAnimationOutTypeKey,
                Constants.kCRToastAnimationInDirectionKey,
                Constants.kCRToastAnimationOutDirectionKey,
                Constants.kCRToastNotificationPreferredPaddingKey
            };

            var objects = new NSObject[] {
                NSNumber.FromInt64((long) (CRToastType.StatusBar)),
                NSNumber.FromInt64((long) (CRToastPresentationType.Push)),
                NSNumber.FromBoolean(true),
                (NSString) message,
                NSNumber.FromInt64((long)UITextAlignment.Center),
                NSNumber.FromDouble(time),
                NSNumber.FromInt64(1),
                NSNumber.FromInt64(1),
                NSNumber.FromInt64(2),
                NSNumber.FromInt64(2),
                NSNumber.FromDouble(0)
            };

            var options = NSMutableDictionary.FromObjectsAndKeys(objects, keys);

            return NSDictionary.FromDictionary(options);
        }

    }
}
using BigTed;
using Bizland.Interfaces;
using Bizland.iOS.DependencyService;
using CRToast;
using Foundation;
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
               }
           );
        }

        NSDictionary Options(string message, double time)
        {
            var keys = new NSString[] {
                Constants.kCRToastTextKey,
                Constants.kCRToastTimeIntervalKey,
            };

            var objects = new NSObject[] {
                (NSString) message,
                NSNumber.FromDouble(time/1000),
            };

            var options = NSMutableDictionary.FromObjectsAndKeys(objects, keys);

            return NSDictionary.FromDictionary(options);
        }

    }
}
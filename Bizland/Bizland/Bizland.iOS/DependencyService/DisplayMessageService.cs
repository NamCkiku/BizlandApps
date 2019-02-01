using Bizland.Core;
using Bizland.iOS.DependencyService;
using CRToast;
using Foundation;
using GlobalToast;
using GlobalToast.Animation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(DisplayMessageService))]
namespace Bizland.iOS.DependencyService
{
    /// <summary>
    /// Class dùng chung để hiển thị thông báo
    /// </summary>
    /// <Modified>
    /// Name     Date         Comments
    /// namth  12/7/2018   created
    /// </Modified>
    /// <Modified>
    /// Name     Date         Comments
    /// namth  12/7/2018   created
    /// </Modified>
    /// <seealso cref="BA_GPS.Core.IDisplayMessage" />
    public class DisplayMessageService : IDisplayMessage
    {
        public void ShowMessageError(string message, double time)
        {
            // More configurations
            Toast.MakeToast(message)
                 .SetPosition(ToastPosition.Bottom) // Default is Bottom
                 .SetDuration(time) // Default is Regular
                 .SetShowShadow(false) // Default is true
                 .SetAnimator(new ScaleAnimator()) // Default is FadeAnimator
                 .SetBlockTouches(true) // Default is false. If false touches that occur on the toast will be sent down to parent view
                 .SetAutoDismiss(true) // Default is true. If set to false Dismiss button will be shown
                 .SetParentController(null)
                 .SetAppearance(new ToastAppearance
                 {
                     Color = UIColor.Red,
                     CornerRadius = 8,
                     // Other properties removed for brevity
                 }).SetLayout(new ToastLayout
                 {
                     PaddingLeading = 16,
                     PaddingTrailing = 16,
                     Spacing = 6,
                     // Other properties removed for brevity
                 })
                 .Show();
        }

        public void ShowMessageInfo(string message, double time)
        {
            // More configurations
            Toast.MakeToast(message)
                 .SetPosition(ToastPosition.Bottom) // Default is Bottom
                 .SetDuration(time) // Default is Regular
                 .SetShowShadow(false) // Default is true
                 .SetAnimator(new ScaleAnimator()) // Default is FadeAnimator
                 .SetBlockTouches(true) // Default is false. If false touches that occur on the toast will be sent down to parent view
                 .SetAutoDismiss(true) // Default is true. If set to false Dismiss button will be shown
                 .SetParentController(null)
                 .SetAppearance(new ToastAppearance
                 {
                     Color = UIColor.Black,
                     CornerRadius = 8,
                     // Other properties removed for brevity
                 }).SetLayout(new ToastLayout
                 {
                     PaddingLeading = 16,
                     PaddingTrailing = 16,
                     Spacing = 6,
                     // Other properties removed for brevity
                 })
                 .Show();
        }

        public void ShowMessageWarning(string message, double time)
        {
            // More configurations
            Toast.MakeToast(message)
                 .SetPosition(ToastPosition.Bottom) // Default is Bottom
                 .SetDuration(time) // Default is Regular
                 .SetShowShadow(false) // Default is true
                 .SetAnimator(new ScaleAnimator()) // Default is FadeAnimator
                 .SetBlockTouches(true) // Default is false. If false touches that occur on the toast will be sent down to parent view
                 .SetAutoDismiss(true) // Default is true. If set to false Dismiss button will be shown
                 .SetParentController(null)
                 .SetAppearance(new ToastAppearance
                 {
                     Color = UIColor.Orange,
                     CornerRadius = 8,
                     // Other properties removed for brevity
                 }).SetLayout(new ToastLayout
                 {
                     PaddingLeading = 16,
                     PaddingTrailing = 16,
                     Spacing = 6,
                     // Other properties removed for brevity
                 })
                 .Show();
        }

        public void ShowMessageSuccess(string message, double time)
        {
            // More configurations
            Toast.MakeToast(message)
                 .SetPosition(ToastPosition.Bottom) // Default is Bottom
                 .SetDuration(time) // Default is Regular
                 .SetShowShadow(false) // Default is true
                 .SetAnimator(new ScaleAnimator()) // Default is FadeAnimator
                 .SetBlockTouches(true) // Default is false. If false touches that occur on the toast will be sent down to parent view
                 .SetAutoDismiss(true) // Default is true. If set to false Dismiss button will be shown
                 .SetParentController(null)
                 .SetAppearance(new ToastAppearance
                 {
                     Color = UIColor.Green,
                     CornerRadius = 8,
                     // Other properties removed for brevity
                 }).SetLayout(new ToastLayout
                 {
                     PaddingLeading = 16,
                     PaddingTrailing = 16,
                     Spacing = 6,
                     // Other properties removed for brevity
                 })
                 .Show();
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
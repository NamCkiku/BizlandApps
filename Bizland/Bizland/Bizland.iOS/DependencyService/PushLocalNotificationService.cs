using Bizland.Core;
using Bizland.iOS.DependencyService;
using Bizland.iOS.Helpers;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PushLocalNotificationService))]
namespace Bizland.iOS.DependencyService
{
    public class PushLocalNotificationService : IPushLocalNotification
    {
        public void PushLocalNotification(string title, string content, string icon)
        {
            BizlandSetup.AppDelegate.InvokeOnMainThread(() =>
            {
                var notification = new UILocalNotification();
                notification.FireDate = NSDate.Now;
                notification.AlertTitle = title;
                notification.AlertBody = content;
                notification.SoundName = UILocalNotification.DefaultSoundName;
                UIApplication.SharedApplication.ScheduleLocalNotification(notification);
            });
        }
    }
}
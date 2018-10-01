using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Bizland.Core;
using Bizland.Droid.DependencyService;
using Bizland.Droid.Helper;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(PushLocalNotificationService))]

namespace Bizland.Droid.DependencyService
{
    public class PushLocalNotificationService : IPushLocalNotification
    {
        public void PushLocalNotification(string title, string content, string icon)
        {
            Random rnd = new Random();
            int month = rnd.Next(1, 99999);


            var builder = new NotificationCompat.Builder(BizlandSetup.Activity)
                .SetContentTitle(title)
                .SetContentText(content)
                .SetPriority(2)
                .SetDefaults(0)
                .SetAutoCancel(true)
                .SetVibrate(new long[1000]);

            try
            {
                if (icon != null)
                {
                    var image = BizlandSetup.Activity.Resources.GetIdentifier(icon, "drawable", BizlandSetup.Activity.PackageName);
                    builder.SetSmallIcon(image);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            var textStyle = new NotificationCompat.BigTextStyle();
            textStyle.BigText(content);
            builder.SetStyle(textStyle);

            var notigi = builder.Build();
            var notificationManager = BizlandSetup.Activity.GetSystemService(Context.NotificationService) as NotificationManager;

            notificationManager.Notify(month, notigi);
        }
    }
}
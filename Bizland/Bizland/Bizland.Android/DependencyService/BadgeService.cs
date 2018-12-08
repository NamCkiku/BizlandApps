using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Bizland.Core;
using Bizland.Droid.DependencyService;
using Xamarin.Forms;

[assembly: Dependency(typeof(BadgeService))]
namespace Bizland.Droid.DependencyService
{
    public class BadgeService : IBadge
    {
        private const int BadgeNotificationId = int.MinValue;

        /// <summary>
        /// Sets the badge.
        /// </summary>
        /// <param name="badgeNumber">The badge number.</param>
        /// <param name="title">The title. Used only by Android</param>
        public void SetBadge(int badgeNumber, string title = null)
        {
            var notificationManager = getNotificationManager();
            var notification = createNativeNotification(badgeNumber, title ?? string.Format("{0} new messages", badgeNumber));

            notificationManager.Notify(BadgeNotificationId, notification);
        }

        /// <summary>
        /// Clears the badge.
        /// </summary>
        public void ClearBadge()
        {
            var notificationManager = getNotificationManager();
            notificationManager.Cancel(BadgeNotificationId);
        }

        private NotificationManager getNotificationManager()
        {
            var notificationManager = Android.App.Application.Context.GetSystemService(Context.NotificationService) as NotificationManager;
            return notificationManager;
        }

        private Notification createNativeNotification(int badgeNumber, string title)
        {
            var builder = new Notification.Builder(Android.App.Application.Context)
                .SetContentTitle(title)
                .SetTicker(title)
                .SetNumber(badgeNumber)
                .SetSmallIcon(Resource.Drawable.ic_bed);

            var nativeNotification = builder.Build();
            return nativeNotification;
        }
    }
}
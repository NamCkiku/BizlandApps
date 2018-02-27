using Plugin.FirebasePushNotification;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Bizland.Helpers
{
    /// <summary>
    /// Tiện ích làm việc với Firebase
    /// </summary>
    /// <Modified>
    /// Name     Date         Comments
    /// TrungTQ  22/1/2018   created
    /// </Modified>
    public class FirebaseHelper
    {
        /// <summary>
        /// Đăng ký Firebase
        /// </summary>
        /// <Modified>
        /// Name     Date         Comments
        /// TrungTQ  26/12/2017   created
        /// </Modified>
        public static void RegisterFirebase()
        {
            try
            {
                // Handle when your app starts
                CrossFirebasePushNotification.Current.Subscribe("general");
                // Khi Token refresh mới.
                CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
                {
                };

                CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
                {
                };
                CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
                {
                };
            }
            catch (Exception ex)
            {
            }
        }
    }
}

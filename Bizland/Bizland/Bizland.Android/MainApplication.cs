﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.FirebasePushNotification;

namespace Bizland.Droid
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
      : base(handle, transer)
        {
        }
        public override void OnCreate()
        {
            base.OnCreate();

            //If debug you should reset the token each time.
#if DEBUG
            //FirebasePushNotificationManager.Initialize(this, false);
#else
              FirebasePushNotificationManager.Initialize(this,false);
#endif

            //CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            //{
            //    System.Diagnostics.Debug.WriteLine("NOTIFICATION RECEIVED", p.Data);

            //    // Kiểm tra để bật sáng màn hình trong trường hợp màn hình bị tối
            //    TurnOnScreen();
            //};
        }
        /// </summary>
        /// <param name="context">The context.</param>
        /// <Modified>
        /// Name     Date         Comments
        /// TrungTQ  2/1/2018   created
        /// </Modified>
        protected void TurnOnScreen()
        {
            bool isScreenOn = false;
            try
            {
                PowerManager mPowerManager = (PowerManager)GetSystemService(Android.Content.Context.PowerService);
                if (mPowerManager == null)
                {
                    return;
                }
                if (Android.OS.Build.VERSION.SdkInt > BuildVersionCodes.Kitkat)
                {
                    isScreenOn = mPowerManager.IsInteractive;
                }
                else
                {
                    isScreenOn = mPowerManager.IsScreenOn;
                }

                if (!isScreenOn)
                {
                    /** Mở màn hình */
                    PowerManager.WakeLock mWakeLock = mPowerManager.NewWakeLock(WakeLockFlags.AcquireCausesWakeup | WakeLockFlags.OnAfterRelease | WakeLockFlags.Full, "tag");

                    if (mWakeLock == null)
                    {
                        return;
                    }

                    mWakeLock.Acquire();
                    mWakeLock.Release();
                }

                if (!isScreenOn)
                {
                    /** Mở khóa nếu có */
                    Android.Views.Window window = (Xamarin.Forms.Forms.Context as Activity).Window;

                    window.AddFlags(WindowManagerFlags.KeepScreenOn | WindowManagerFlags.DismissKeyguard | WindowManagerFlags.ShowWhenLocked | WindowManagerFlags.TurnScreenOn);
                }
            }
            catch (Exception ex)
            {
                //XCVLogger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);
            }
        }
    }
}
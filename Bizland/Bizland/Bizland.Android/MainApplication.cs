using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Bizland.Core;
using Plugin.CurrentActivity;
using System;
using System.Reflection;

namespace Bizland.Droid
{
    [Application]
    [MetaData("com.google.android.maps.v2.API_KEY", Value = ServerConfig.GoogleMapKeyAndroid)]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
      : base(handle, transer)
        {
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityStopped(Activity activity)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            CrossCurrentActivity.Current.Init(this);
        }
        /// </summary>
        /// <param name="context">The context.</param>
        /// <Modified>
        /// Name     Date         Comments
        /// Namth  2/1/2018   created
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
            catch (Exception)
            {
                Logger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);
            }
        }
    }
}
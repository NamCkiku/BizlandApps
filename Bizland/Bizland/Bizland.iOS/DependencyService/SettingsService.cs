using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bizland.Core;
using Bizland.iOS.DependencyService;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SettingsService))]
namespace Bizland.iOS.DependencyService
{
    public class SettingsService : ISettingsService
    {
        public bool OpenLocationSettings()
        {
            // Opening settings only open in iOS 8+
            if (!UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
                return false;

            try
            {
                UIApplication.SharedApplication.OpenUrl(new NSUrl(UIApplication.OpenSettingsUrlString));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool OpenWifiSettings()
        {
            bool ok = false;
            try
            {
                var WiFiURL = new NSUrl("prefs:root=WIFI");

                if (UIApplication.SharedApplication.CanOpenUrl(WiFiURL))
                {   //Pre iOS 10
                    UIApplication.SharedApplication.OpenUrl(WiFiURL);
                }
                else
                {   //iOS 10
                    UIApplication.SharedApplication.OpenUrl(new NSUrl("App-Prefs:root=WIFI"));
                }

                ok = true;
            }
            catch
            {
                ok = false;
            }
            return ok;
        }
    }
}
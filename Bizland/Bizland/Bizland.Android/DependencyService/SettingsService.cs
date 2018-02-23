using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Bizland.Core.DependencyService;
using Bizland.Droid.DependencyService;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(SettingsService))]
namespace Bizland.Droid.DependencyService
{
    public class SettingsService : ISettingsService
    {
        public bool OpenLocationSettings()
        {
            LocationManager locationManager = (LocationManager)Forms.Context.GetSystemService(Context.LocationService);

            if (locationManager.IsProviderEnabled(LocationManager.GpsProvider) == false)
            {
                Intent gpsSettingIntent = new Intent(Android.Provider.Settings.ActionLocat‌​ionSourceSettings);
                Forms.Context.StartActivity(gpsSettingIntent);
            }
            return true;
        }

        public bool OpenWifiSettings()
        {
            Xamarin.Forms.Forms.Context.StartActivity(new Android.Content.Intent(Android.Provider.Settings.ActionWifiSettings));
            return true;
        }
    }
}
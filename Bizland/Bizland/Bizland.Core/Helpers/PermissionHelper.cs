using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bizland.Core.Helpers
{
    /// <summary>
    /// Class làm việc với quyền
    /// </summary>
    /// <Modified>
    /// Name     Date         Comments
    /// TrungTQ  11/27/2017   created
    /// </Modified>
    public static class PermissionHelper
    {
        /// <summary>
        /// Kiểm tra xem có quyền không thì mới tiếp tục cho phép hoạt động.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// TrungTQ  27/11/2017   created
        /// </Modified>
        public static async Task<bool> CheckLocationPermissions()
        {
            Permission permission = Permission.Location;
            var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
            bool request = false;
            var title = "Quyền truy cập vị trí";
            var question = "Chức năng yêu cầu quyền truy cập vị trí của bạn.";
            var positive = "Cài đặt";
            var negative = "Để sau";

            if (permissionStatus == PermissionStatus.Denied)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                    if (task == null)
                        return false;

                    var result = await task;
                    if (result)
                    {
                        CrossPermissions.Current.OpenAppSettings();
                    }

                    return false;
                }
                request = true;
            }

            if (request || permissionStatus != PermissionStatus.Granted)
            {
                var newStatus = await CrossPermissions.Current.RequestPermissionsAsync(permission);
                if (newStatus.ContainsKey(permission) && newStatus[permission] != PermissionStatus.Granted)
                {
                    var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                    if (task == null)
                        return false;

                    var result = await task;
                    if (result)
                    {
                        CrossPermissions.Current.OpenAppSettings();
                    }
                    return false;
                }
            }

            // Chưa bật định vị
            if (permissionStatus == PermissionStatus.Granted)
            {
                var locator = CrossGeolocator.Current;

                var isGeolocationEnabled = locator.IsGeolocationEnabled;

                if (!isGeolocationEnabled)
                {
                    var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                    if (task == null)
                        return false;

                    var result = await task;

                    if (result)
                    {
                        Xamarin.Forms.DependencyService.Get<ISettingsService>().OpenLocationSettings();
                    }
                    return false;
                }
            }

            return true;
        }

    }
}

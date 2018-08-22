using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bizland.Core
{
    /// <summary>
    /// Class làm việc với quyền
    /// </summary>
    /// <Modified>
    /// Name     Date         Comments
    /// Namth  11/27/2017   created
    /// </Modified>
    public static class PermissionHelper
    {
        private const string POSITIVE = "Cài đặt";
        private const string NEGATIVE = "Để sau";

        /// <summary>
        /// Kiểm tra xem có quyền không thì mới tiếp tục cho phép hoạt động.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// Namth  27/11/2017   created
        /// </Modified>
        public static async Task<bool> CheckLocationPermissions()
        {
            var title = "Quyền truy cập vị trí";
            var question = "Chức năng yêu cầu quyền truy cập vị trí của bạn.";

            await CheckPermission(Permission.Location, title, question);

            var permission = Permission.Location;

            var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);

            // Chưa bật định vị
            if (permissionStatus == PermissionStatus.Granted)
            {
                var locator = CrossGeolocator.Current;

                var isGeolocationEnabled = locator.IsGeolocationEnabled;

                if (!isGeolocationEnabled)
                {
                    var task = Application.Current?.MainPage?.DisplayAlert(title, question, POSITIVE, NEGATIVE);
                    if (task == null)
                        return false;

                    var result = await task;

                    if (result)
                    {
                        DependencyService.Get<ISettingsService>().OpenLocationSettings();
                    }
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Kiểm tra xem có quyền truy cập Camera không thì mới tiếp tục cho phép hoạt động.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// Truongpv  09/08/2018   created
        /// </Modified>
        public static async Task<bool> CheckCameraPermissions()
        {
            var title = "Quyền truy cập camera";
            var question = "Chức năng yêu cầu quyền truy cập camera của bạn.";

            return await CheckPermission(Permission.Camera, title, question);
        }

        /// <summary>
        /// Kiểm tra xem có quyền truy cập thư mục ảnh không thì mới tiếp tục cho phép hoạt động.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// Truongpv  09/08/2018   created
        /// </Modified>
        public static async Task<bool> CheckPhotoPermissions()
        {
            var title = "Quyền truy cập thư mục ảnh";
            var question = "Chức năng yêu cầu quyền truy cập thư mục ảnh của bạn.";

            return await CheckPermission(Permission.Photos, title, question);
        }

        /// <summary>
        /// Kiểm tra xem có quyền không thì mới tiếp tục cho phép hoạt động.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// Truongpv  09/08/2018   created
        /// </Modified>
        public static async Task<bool> CheckPermission(Permission permission, string title, string question)
        {
            return await CheckPermission(permission, title, question, POSITIVE, NEGATIVE);
        }

        /// <summary>
        /// Kiểm tra xem có quyền không thì mới tiếp tục cho phép hoạt động.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// Truongpv  09/08/2018   created
        /// </Modified>
        public static async Task<bool> CheckPermission(Permission permission, string title, string question, string positive, string negative)
        {
            var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
            bool request = false;

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

            return true;
        }
    }
}

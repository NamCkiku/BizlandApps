using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bizland.Core
{
    /// <summary>
    /// Tiện ích dùng cho dịch vụ vị trí
    /// Move từ BaseService của NamTH sang cho tập trung.
    /// </summary>
    /// <Modified>
    /// Name     Date         Comments
    /// Namth  27/11/2017   created
    /// </Modified>
    public class LocationHelper
    {
        /// <summary>
        /// Lấy vị trí người dùng khi mở apps
        /// </summary>
        /// <returns>namth/14/09/2017</returns>
        public static async Task<Position> GetGpsLocation()
        {
            // Namth:  Nếu không có quyền thì lấy mặc định là vị trí công ty Bình Anh
            Position position = new Position(0, 0);
            try
            {
                // Namth: thêm đoạn check quyền location thì mới cho phép tiếp tục hoạt động.
                var hasPermission = await PermissionHelper.CheckLocationPermissions();

                if (hasPermission)
                {
                    var locator = CrossGeolocator.Current;

                    // Thêm lệnh này để chạy location trên Android
                    locator.DesiredAccuracy = 100;

                    var isGeolocationEnabled = locator.IsGeolocationEnabled;

                    if (isGeolocationEnabled)
                    {
                        position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);
                    }
                    // Chưa bật định vị
                    else
                    {
                        await Application.Current?.MainPage?.DisplayAlert("Thông báo", "GPS chưa được bật trên thiết bị của bạn. Vui lòng kiểm tra cài đặt trên điện thoại.", "Đồng ý");
                    }
                }
            }
            catch (GeolocationException geoEx)
            {
                Logger.WriteError(MethodInfo.GetCurrentMethod().Name, geoEx);
            }
            catch (TaskCanceledException ex)
            {
                Logger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);
            }

            catch (Exception ex)
            {
                Logger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);

                Application.Current?.MainPage?.DisplayAlert("Thông báo", "Vui lòng kiểm tra GPS của bạn đã bật chưa?", "Đồng ý");
            }

            return position;
        }
    }
}

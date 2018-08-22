using Plugin.Connectivity;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bizland.Core
{
    /// <summary>
    /// Các hàm tiện ích liên quan tới mạng
    /// </summary>
    /// <Modified>
    /// Name     Date         Comments
    /// Namth  12/4/2017   created
    /// </Modified>
    public class NetworkHelper
    {
        /// <summary>
        /// Kiểm tra mạng có sẵn sàng không 
        /// </summary>
        /// <returns>
        ///  True: mạng ngon, False: mạng chưa kết nối => phải check lại.
        /// </returns>
        public static bool IsNetworkAvailable
        {
            get
            {
                if (!CrossConnectivity.IsSupported)
                    return true;

                // Do this only if you need to and aren't listening to any other events as they will not fire.
                using (var connectivity = CrossConnectivity.Current)
                {
                    return connectivity.IsConnected;
                }
            }
        }

        /// <summary>
        /// Kiểm tra mạng có sẵn sàng không 
        /// </summary>
        /// <returns>
        ///  True: mạng ngon, False: mạng chưa kết nối => phải check lại.
        /// </returns>
        public static bool OpenWifiSettings()
        {
            if (!IsNetworkAvailable)
            {
                return DependencyService.Get<ISettingsService>().OpenWifiSettings();
            }
            return true;
        }

        /// <summary>
        /// Kiểm tra mạng có sẵn sàng không 
        /// </summary>
        /// <returns>
        ///  True: mạng ngon, False: mạng chưa kết nối => phải check lại.
        /// </returns>
        public static async Task<bool> CheckNetwork()
        {
            var title = "Quyền truy cập mạng";
            var question = "Chức năng yêu cầu quyền truy cập mạng.";
            var positive = "Cài đặt";
            var negative = "Để sau";

            if (!IsNetworkAvailable)
            {
                var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                if (task == null)
                    return false;

                var result = await task;

                if (result)
                {
                    return DependencyService.Get<ISettingsService>().OpenWifiSettings();
                }
                return false;
            }

            return true;
        }
    }
}

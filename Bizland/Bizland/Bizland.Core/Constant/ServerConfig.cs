using System;
using System.Collections.Generic;
using System.Text;

namespace Bizland.Core.Constant
{
    /// <summary>
    /// Thông tin cấu hình của server
    /// Tất cả thông tin cấu hình cho vào đây để không bị loãng
    /// </summary>
    /// <Modified>
    /// Name     Date         Comments
    /// TrungTQ  16/1/2018   created
    /// </Modified>
    public class ServerConfig
    {
        public const string ApiEndpoint = "http://api.bizland.vn";
        /// <summary>
        /// Key cho Google Map cho iOS
        /// </summary>
        /// <Modified>
        /// Name     Date         Comments
        /// TrungTQ  16/1/2018   created
        /// </Modified>
        public static string GoogleMapKeyiOS = "";

        /// <summary>
        /// Key cho Google Map cho Android
        /// </summary>
        /// <Modified>
        /// Name     Date         Comments
        /// TrungTQ  16/1/2018   created
        /// </Modified>
        public static string GoogleMapKeyAndroid = "";
    }
}

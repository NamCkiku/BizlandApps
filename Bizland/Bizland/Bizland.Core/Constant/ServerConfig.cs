﻿namespace Bizland.Core
{
    /// <summary>
    /// Thông tin cấu hình của server
    /// Tất cả thông tin cấu hình cho vào đây để không bị loãng
    /// </summary>
    /// <Modified>
    /// Name     Date         Comments
    /// Namth  16/1/2018   created
    /// </Modified>
    public class ServerConfig
    {
        public const string ApiEndpoint = "http://api.bizland.vn";
        /// <summary>
        /// Key cho Google Map cho iOS
        /// </summary>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  16/1/2018   created
        /// </Modified>
        public const string GoogleMapKeyiOS = "AIzaSyDSdW_P8JRfGlL620LM3pL3umSnh0_lUjo";

        /// <summary>
        /// Key cho Google Map cho Android
        /// </summary>
        /// <Modified>
        /// Name     Date         Comments
        /// Namth  16/1/2018   created
        /// </Modified>
        public const string GoogleMapKeyAndroid = "AIzaSyDwhz_8SoIcFYMLVh3rcto1cWGbAPdQfGI";
    }
}

using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Bizland.Core
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        private const string IdLatitude = "latitude";
        private static readonly float LatitudeDefault = 20.973993f;

        private const string IdLongitude = "longitude";
        private static readonly float LongitudeDefault = 105.846421f;

        private const string AccessToken = "access_token";
        private static readonly string AccessTokenDefault = string.Empty;
        private const string UserInfo = "user_info";
        private static readonly string UserInfoDefault = string.Empty;

        public static float Latitude
        {
            get => AppSettings.GetValueOrDefault(IdLatitude, LatitudeDefault);
            set => AppSettings.AddOrUpdateValue(IdLatitude, value);
        }

        public static float Longitude
        {
            get => AppSettings.GetValueOrDefault(IdLongitude, LongitudeDefault);
            set => AppSettings.AddOrUpdateValue(IdLongitude, value);
        }

        public static string AuthAccessToken
        {
            get => AppSettings.GetValueOrDefault(AccessToken, AccessTokenDefault);
            set => AppSettings.AddOrUpdateValue(AccessToken, value);
        }

        public static string UserInfomation
        {
            get => AppSettings.GetValueOrDefault(UserInfo, UserInfoDefault);
            set => AppSettings.AddOrUpdateValue(UserInfo, value);
        }
    }
}

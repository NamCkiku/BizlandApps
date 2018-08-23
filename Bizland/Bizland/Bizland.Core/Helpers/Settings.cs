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
    }
}

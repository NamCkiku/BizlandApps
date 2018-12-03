using Plugin.Geolocator.Abstractions;
using System;

namespace Bizland.Core
{
    public class Point
    {
        public Point(double lat, double lon)
        {
            Lon = lon;
            Lat = lat;
        }
        public double Lon; // Longitude
        public double Lat; // Latitude
    }

    /// <summary>
    /// Các hàm tình toán, hằng số vận tốc
    /// </summary>
    /// <Modified>
    /// Name     Date         Comments
    /// namth  12/3/2018   created
    /// </Modified>
    /// <Modified>
    /// Name     Date         Comments
    /// namth  12/3/2018   created
    /// </Modified>
    public static class GeoHelper
    {
        /// <summary>
        /// Vận tốc giới hạn, để lọc nhiễu
        /// </summary>
        public const int LIMITED_VELOCITY = 200;
        public const double R = 6378.1;

        /// <summary>
        /// Vận tốc xác định xe di chuyển
        /// </summary>
        public const int VELOCITY_MOVING = 0;

        public static double DistanceCalculatorCoordinate(double lng, double lat, double lngPre, double latPrev)
        {
            double P1X = lng * (Math.PI / 180);
            double P1Y = lat * (Math.PI / 180);
            double P2X = lngPre * (Math.PI / 180);
            double P2Y = latPrev * (Math.PI / 180);
            double Kc = 0;
            double Temp = 0;

            Kc = P2X - P1X;
            Temp = Math.Cos(Kc);
            Temp = Temp * Math.Cos(P2Y);
            Temp = Temp * Math.Cos(P1Y);

            Kc = Math.Sin(P1Y);
            Kc = Kc * Math.Sin(P2Y);
            Temp = Temp + Kc;
            Kc = Math.Acos(Temp);
            Kc = Kc * 6376;

            return Kc;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="_OldLongitude"></param>
        /// <param name="_OldLatitude"></param>
        /// <param name="Longitude"></param>
        /// <param name="Latitude"></param>
        /// <returns></returns>
        public static byte Bearing(Double Distance, byte _OldBearing, Double _OldLongitude, Double _OldLatitude, Double Longitude, Double Latitude)
        {
            //If longitude and latitude are not valid, don't change car's direction
            if (Longitude == 0 | Latitude == 0) return _OldBearing;

            //If distance between two cars is too small, retur old Bearing
            if (Distance < 0.02)
            {
                return _OldBearing;
            }

            byte _Bearing = 0;
            //Calculate new direction
            double DeltaX = Latitude - _OldLatitude;
            double DeltaY = Longitude - _OldLongitude;
            double S = Math.Sqrt(Math.Pow(DeltaX, 2) + Math.Pow(DeltaY, 2));
            double G = Math.Acos(DeltaX / S);
            if (DeltaY < 0) G = 2 * Math.PI - G;
            G = Math.Round(4 * G / Math.PI);
            if (G > 7 || G < 0) G = 0;

            _OldLatitude = Latitude;
            _OldLongitude = Longitude;

            try
            { _Bearing = (byte)G; }
            catch
            { _Bearing = 0; }

            return _Bearing;
        }



        /// <summary>
        /// tính lại độ lệch góc.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  12/3/2018   created
        /// </Modified>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  12/3/2018   created
        /// </Modified>
        public static double GetRotaion(double start, double end)
        {
            var normalizeEnd = end - start; // rotate start to 0
            var normalizedEndAbs = (normalizeEnd + 360) % 360;

            // -1 = anticlockwise, 1 = clockwise
            var direction = normalizedEndAbs > 180 ? -1 : 1;
            double rotation;
            if (direction > 0)
            {
                rotation = normalizedEndAbs;
            }
            else
            {
                rotation = normalizedEndAbs - 360;
            }
            return rotation;
        }

        /// <summary>
        /// tính lại góc quay theo chiều đồng hồ.
        /// </summary>
        /// <param name="fraction">The fraction.</param>
        /// <param name="start">The start.</param>
        /// <param name="newRotation">The new rotation.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  12/3/2018   created
        /// </Modified>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  12/3/2018   created
        /// </Modified>
        public static double ComputeRotation(double fraction, double start, double newRotation)
        {
            double result = fraction * newRotation + start;
            //xử lý tính lại khi góc quay giá trị < 0
            return (result + 360) % 360;
        }

        /// <summary>
        /// Tính góc quay giữa 2 điểm location
        /// </summary>
        /// <param name="fromLat">From lat.</param>
        /// <param name="fromLng">From LNG.</param>
        /// <param name="toLat">To lat.</param>
        /// <param name="toLng">To LNG.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  12/3/2018   created
        /// </Modified>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  12/3/2018   created
        /// </Modified>
        public static double ComputeHeading(double fromLat, double fromLng, double toLat, double toLng)
        {
            // http://williams.best.vwh.net/avform.htm#Crs
            double fromLatR = ToRadians(fromLat);

            double fromLngR = ToRadians(fromLng);

            double toLatR = ToRadians(toLat);

            double toLngR = ToRadians(toLng);

            double dLng = toLngR - fromLngR;


            double heading = Math.Atan2(Math.Sin(dLng) * Math.Cos(toLatR),
                Math.Cos(fromLatR) * Math.Sin(toLatR) - Math.Sin(fromLatR) * Math.Cos(toLatR) * Math.Cos(dLng));

            return Wrap(ToDegrees(heading), -180, 180);
        }

        public static double ToRadians(double angdeg)
        {
            return angdeg / 180.0 * Math.PI;
        }

        public static double ToDegrees(double angrad)
        {
            return angrad * 180.0 / Math.PI;
        }

        public static double Wrap(double n, double min, double max)
        {
            return (n >= min && n < max) ? n : (Mod(n - min, max - min) + min);
        }

        public static double Mod(double x, double m)
        {
            return ((x % m) + m) % m;
        }



        public static double ComputeAngleBetween(double fromLat, double fromLng, double toLat, double toLng)
        {
            // Haversine's formula
            double dLat = fromLat - toLat;
            double dLng = fromLng - toLng;
            return 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(dLat / 2), 2) +
                    Math.Cos(fromLat) * Math.Cos(toLat) * Math.Pow(Math.Sin(dLng / 2), 2)));
        }



        /// <summary>
        /// Hàm dùng để chia nhỉ tọa độ giữa 2 điểm.
        /// </summary>
        /// <param name="fraction">The fraction.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  12/3/2018   created
        /// </Modified>
        /// <Modified>
        /// Name     Date         Comments
        /// namth  12/3/2018   created
        /// </Modified>
        public static Position Interpolate(double fraction, Position from, Position to)
        {
            // http://en.wikipedia.org/wiki/Slerp
            double fromLat = ToRadians(from.Latitude);
            double fromLng = ToRadians(from.Longitude);
            double toLat = ToRadians(to.Latitude);
            double toLng = ToRadians(to.Longitude);
            double cosFromLat = Math.Cos(fromLat);
            double cosToLat = Math.Cos(toLat);
            // Computes Spherical interpolation coefficients.
            double angle = ComputeAngleBetween(fromLat, fromLng, toLat, toLng);
            double sinAngle = Math.Sin(angle);
            if (sinAngle < 1E-6)
            {
                return from;
            }
            double a = Math.Sin((1 - fraction) * angle) / sinAngle;
            double b = Math.Sin(fraction * angle) / sinAngle;
            // Converts from polar to vector and interpolate.
            double x = a * cosFromLat * Math.Cos(fromLng) + b * cosToLat * Math.Cos(toLng);
            double y = a * cosFromLat * Math.Sin(fromLng) + b * cosToLat * Math.Sin(toLng);
            double z = a * Math.Sin(fromLat) + b * Math.Sin(toLat);
            // Converts interpolated vector back to polar.
            double lat = Math.Atan2(z, Math.Sqrt(x * x + y * y));
            double lng = Math.Atan2(y, x);
            return new Position(ToDegrees(lat), ToDegrees(lng));
        }

        public static double GetetInterpolation(double input)
        {
            return (float)(Math.Cos((input + 1) * Math.PI) / 2.0f) + 0.5f;
        }
    }
}

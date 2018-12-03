using System;
using System.Collections.Generic;
using System.Reflection;

namespace Bizland.Core
{
    public class GeoRandomHelper
    {
        /// <summary>
        /// Tọa độ đầu, cuối
        /// </summary>
        /// <Modified>
        /// Name     Date         Comments
        /// TrungTQ  12/2/2018   created
        /// </Modified>
        public static string InputCoordinates = "20.968168,107.045780@20.973993,105.846421";

        /// <summary>
        /// Khoảng cách giữa 2 bước nhảy
        /// </summary>
        /// <Modified>
        /// Name     Date         Comments
        /// TrungTQ  12/2/2018   created
        /// </Modified>
        public static int DistanceJump = 100;

        public static int TotalCount { get; set; }

        public static int _CurrentCount = 0;

        public static int CurrentCount
        {
            get
            {
                return _CurrentCount;
            }
            set
            {
                if (value > TotalCount + 1)
                {
                    value = 0;
                }
                _CurrentCount = value;
            }
        }

        private static List<Point> _ResultPoints = null;

        public static List<Point> ResultPoints
        {
            get
            {
                if (_ResultPoints == null)
                {
                    _ResultPoints = new List<Point>();

                    _ResultPoints.AddRange(GenerateData());

                    TotalCount = _ResultPoints.Count;

                }
                return _ResultPoints;
            }
        }

        private static List<Point> ParseListPoints(string strPointBefore)
        {
            List<Point> lstPoint = new List<Point>();

            string[] arr = strPointBefore.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            Point p = null;
            for (int i = 0; i <= arr.Length - 1; i++)
            {
                p = new Point(0, 0);

                string[] arr1 = arr[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                p.Lat = GeneralHelper.ConvertToDouble(arr1[0]);
                p.Lon = GeneralHelper.ConvertToDouble(arr1[1]);

                lstPoint.Add(p);
            }

            return lstPoint;
        }

        /// <summary>
        /// Chia 2 đoạn bất kì ra làm bao nhiêu đoạn.
        /// </summary>
        private static string SplitLine(Point p1, Point p2, int count)
        {
            string result = p1.Lat.ToString().Replace(',', '.') + ',' + p1.Lon.ToString().Replace(',', '.') + '@';
            Point p = new Point(0, 0);
            Double d = Math.Sqrt((p1.Lon - p2.Lon) * (p1.Lon - p2.Lon) + (p1.Lat - p2.Lat) * (p1.Lat - p2.Lat)) / count;
            Double fi = Math.Atan2(p2.Lat - p1.Lat, p2.Lon - p1.Lon);

            List<Point> points = new List<Point>(count + 1);

            for (int i = 0; i <= count; ++i)
            {
                p.Lat = p1.Lat + i * d * Math.Sin(fi);
                p.Lon = p1.Lon + i * d * Math.Cos(fi);
                result += string.Format("{0},{1}@", p.Lat.ToString().Replace(',', '.'), p.Lon.ToString().Replace(',', '.'));
            }
            return result;
        }

        /// <summary>
        /// Sinh dữ liệu, trả về 1 list
        /// </summary>
        private static List<Point> GenerateData()
        {
            List<Point> result = new List<Point>();
            try
            {
                string ResultText = string.Empty;
                List<Point> inputPoints = ParseListPoints(InputCoordinates);

                if (inputPoints != null && inputPoints.Count > 0)
                {
                    for (int i = 0; i < inputPoints.Count - 1; i++)
                    {
                        Point p1 = new Point(inputPoints[i].Lat, inputPoints[i].Lon);
                        Point p2 = new Point(inputPoints[i + 1].Lat, inputPoints[i + 1].Lon);

                        double kc = GeoHelper.DistanceCalculatorCoordinate(p2.Lon, p2.Lat, p1.Lon, p1.Lat);

                        double kcNhay = Convert.ToDouble((double)DistanceJump / 1000);

                        int counts = (int)(kc / kcNhay) + 1;

                        ResultText += SplitLine(p1, p2, counts);
                    }

                    result = ParseListPoints(ResultText);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);
            }

            return result;
        }

        public static Point GetCurrentPoint()
        {
            Point point = new Point(0, 0);
            try
            {
                if (ResultPoints != null && ResultPoints.Count > 0)
                {
                    point = ResultPoints[CurrentCount];

                    // Tăng biến đếm
                    CurrentCount++;
                }
                else
                {
                    point = new Point(0, 0);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError("GetCurrentPoint", ex.Message);
            }
            return point;
        }
    }
}

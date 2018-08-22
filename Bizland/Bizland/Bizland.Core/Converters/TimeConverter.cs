using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace Bizland.Core
{
    public class TimeConverter : IValueConverter
    {
        public object Convert(object value,
                       Type targetType,
                       object parameter,
                       CultureInfo culture)
        {
            if (!(value is DateTime))
            {
                XCVLogger.WriteError(MethodInfo.GetCurrentMethod().Name, "The target must be a DateTime");
            }

            var date = (DateTime)value;
            bool converToLocal = (string)parameter == "ToLocal";

            var result = converToLocal
                            ? date.ToLocalTime().ToString("HH:mm")
                            : date.ToString("HH:mm");
            return result;
        }

        public object ConvertBack(object value,
                                  Type targetType,
                                  object parameter,
                                  CultureInfo culture)
        {
            return DateTime.Parse(value.ToString());
        }
    }
}

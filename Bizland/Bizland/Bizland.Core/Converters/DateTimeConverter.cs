using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Bizland.Core
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = string.Empty;

            if (!(value is DateTime))
            {
                result = "";
            }
            else
            {
                var date = (DateTime)value;
                bool converToLocal = (string)parameter == "ToLocal";

                result = converToLocal
                               ? date.ToLocalTime().ToString("HH:mm dd/MM/yyyy")
                               : date.ToString("HH:mm dd/MM/yyyy");
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = string.Empty;

            if (!(value is DateTime))
            {
                result = "";
            }
            else
            {
                var date = (DateTime)value;
                bool converToLocal = (string)parameter == "ToLocal";

                result = converToLocal
                               ? date.ToLocalTime().ToString("dd/MM/yyyy")
                               : date.ToString("dd/MM/yyyy");
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

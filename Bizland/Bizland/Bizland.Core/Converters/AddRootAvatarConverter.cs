using Bizland.Core;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Bizland.Core
{
    public class AddRootAvatarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(value.ToString()))
                return string.Empty;
            else
                if (!value.ToString().Contains("/"))
                {
                    return value.ToString();
                }
                else
                {
                    return $"{ServerConfig.ApiEndpoint}{value.ToString()}";
                }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

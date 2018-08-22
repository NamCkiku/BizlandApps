using System;
using System.Reflection;

namespace Bizland.Core
{
    public class NullOrEmptyToBoolConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string val = value.ToString();
            if (string.IsNullOrEmpty(val))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            XCVLogger.WriteError(MethodInfo.GetCurrentMethod().Name, new NotImplementedException());
            throw new NotImplementedException();
        }
    }
}

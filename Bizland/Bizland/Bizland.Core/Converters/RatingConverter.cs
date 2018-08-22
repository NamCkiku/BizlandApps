using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace Bizland.Core
{
    public class RatingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var rating = (int)value;
            if (rating == 1)
                return "Thất vọng !";
            if (rating == 2)
                return "Kém !";
            if (rating == 3)
                return "Bình thường !";
            if (rating == 4)
                return "Tôt !";
            if (rating == 5)
                return "Rất tốt !";

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Logger.WriteError(MethodInfo.GetCurrentMethod().Name, new NotImplementedException());
            throw new NotImplementedException();
        }
    }
}

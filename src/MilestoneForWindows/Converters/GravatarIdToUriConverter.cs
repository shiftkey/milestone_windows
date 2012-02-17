using System;
using System.Globalization;
using System.Windows.Data;

namespace MilestoneForWindows.Converters
{
    public class GravatarIdToUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Uri("http://www.gravatar.com/avatar/" + value + "?s=" + parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

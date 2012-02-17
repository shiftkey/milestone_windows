using System;
using System.Globalization;
using System.Windows.Data;

namespace MilestoneForWindows.Converters
{
    public class NumberToEmoteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var num = (int) value;

            if (num > 75)
                return ":'(";

            if (num > 50)
                return ":(";

            if (num > 30)
                return ":S";

            if (num > 10)
                return ":O";

            if (num > 5)
                return ":o";

            return ":)";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
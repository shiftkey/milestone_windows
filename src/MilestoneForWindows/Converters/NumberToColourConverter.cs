using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MilestoneForWindows.Converters
{
    public class NumberToColourConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string color = "#FF199F06";
            var num = (int)value;

            if (num > 75)
                return "#ff0000";

            if (num > 50)
                return "#ff6000";

            if (num > 30)
                return "#ffa200";

            if (num > 5)
                color = "#0084ff";

            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
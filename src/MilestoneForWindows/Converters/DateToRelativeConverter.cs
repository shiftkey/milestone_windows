using System;
using System.Globalization;
using System.Windows.Data;

namespace MilestoneForWindows.Converters
{
    public class DateToRelativeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var t = (DateTime)value;
            var timeSince = DateTime.UtcNow - t.ToUniversalTime();
            if (timeSince < new TimeSpan(0, 0, 15))
                return "just now";

            if (timeSince < new TimeSpan(0, 1, 0))
                return "< 1 min ago";

            if (timeSince < new TimeSpan(1, 0, 0))
            {
                if (timeSince > new TimeSpan(0, 1, 59))
                    return timeSince.Minutes + " mins ago";

                return timeSince.Minutes + " min ago";
            }

            if (timeSince < new TimeSpan(23, 59, 59))
            {
                if (timeSince > new TimeSpan(1, 59, 59))
                    return timeSince.Hours + " hrs ago";
                return timeSince.Hours + " hr ago";
            }

            if (timeSince < new TimeSpan(7, 0, 0, 0))
            {
                if (timeSince > new TimeSpan(1, 23, 59, 59))
                    return timeSince.Days + " days ago";
                return timeSince.Days + " day ago";
            }

            return string.Format("{0} {1} {2}", t.ToShortTimeString(), t.DayOfWeek.ToString().Substring(0, 3), "");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
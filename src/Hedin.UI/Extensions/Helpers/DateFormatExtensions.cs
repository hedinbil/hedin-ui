using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hedin.UI.Extensions.Helpers
{
    public static class DateFormatExtensions
    {
        private static readonly string yearDayMonth = "yyyy-MM-dd";
        private static readonly string hourMinute = "HH:mm";
        
        public static string ToShortDate(this DateTime date)
        {
            return date.ToString(yearDayMonth);
        }
        public static string ToLongDate(this DateTime date) {
            return date.ToString($"{yearDayMonth} {hourMinute}");
        }

        public static string ToShortDate(this DateTimeOffset date)
        {
            return date.ToString(yearDayMonth);
        }
        public static string DateTimeOffset(this DateTime date)
        {
            return date.ToString($"{yearDayMonth} {hourMinute}");
        }
    }
}

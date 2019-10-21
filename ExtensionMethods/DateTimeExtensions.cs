using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace ExtensionMethods
{
    public static class DateTimeExtensions
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static readonly long DatetimeMinTimeTicks = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;


        /// <summary>
        /// Based on the day, it will return st, th, nd and rd
        /// </summary>
        /// <param name="daytime"></param>
        /// <returns></returns>
        public static string GetNthSuffix(DateTime daytime)
        {
            var day = daytime.Day % 10;
            if (day > 3 || day < 1)
            {
                return "th";
            }
            if (day < 2)
            {
                return "st";
            }
            if (day < 3)
            {
                return "nd";
            }
            return "rd";
        }

        /// <summary>
        /// Based on date passed and current date, it will pass the difference of dates 
        /// in seconds, minutes, hours, days, months and year
        /// </summary>
        /// <param name="myDate"></param>
        /// <returns>
        /// Possible reutrn values:
        /// 1) one second ago
        /// 2) X seconds ago
        /// 3) a minute ago
        /// 4) X minutes ago
        /// 5) an hour ago
        /// 6) X hours ago
        /// 7) yesterday
        /// 8) X days ago
        /// 9) one month ago
        /// 10) X months ago
        /// 11) one year ago
        /// 12) X years ago
        /// </returns>
        public static string GetDateTimeAgo(this DateTime myDate)
        {
            myDate = myDate.ToUniversalTime();
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.UtcNow.Ticks - myDate.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

            if (delta < 2 * MINUTE)
                return "a minute ago";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " minutes ago";

            if (delta < 90 * MINUTE)
                return "an hour ago";

            if (delta < 24 * HOUR)
                return ts.Hours + " hours ago";

            if (delta < 48 * HOUR)
                return "yesterday";

            if (delta < 30 * DAY)
                return ts.Days + " days ago";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }
        }

        /// <summary>
        /// DateTime.TryParseExact
        /// custom formats used (in order):
        /// "yyyyMMddTHHmmssZ",
        /// "yyyyMMddTHHmmZ",
        /// "yyyyMMddTHHmmss",
        /// "yyyyMMddTHHmm",
        /// "yyyyMMddHHmmss",
        /// "yyyyMMddHHmm",
        /// "yyyyMMdd",
        /// "yyyy-MM-ddTHH-mm-ss",
        /// "yyyy-MM-dd-HH-mm-ss",
        /// "yyyy-MM-dd-HH-mm",
        /// "yyyy-MM-dd",
        /// "MM-dd-yyyy",
        /// "dd/MM/yyyy"
        /// </summary>
        /// <param name="dateToParse"></param>
        /// <param name="formats"></param>
        /// <param name="provider"></param>
        /// <param name="styles"></param>
        /// <returns></returns>
        public static DateTime? ParseDateTime(
            string dateToParse,
            string[] formats = null,
            IFormatProvider provider = null,
            DateTimeStyles styles = DateTimeStyles.None)
        {
            var CUSTOM_DATE_FORMATS = new string[]
                {
                "yyyyMMddTHHmmssZ",
                "yyyyMMddTHHmmZ",
                "yyyyMMddTHHmmss",
                "yyyyMMddTHHmm",
                "yyyyMMddHHmmss",
                "yyyyMMddHHmm",
                "yyyyMMdd",
                "yyyy-MM-ddTHH-mm-ss",
                "yyyy-MM-dd-HH-mm-ss",
                "yyyy-MM-dd-HH-mm",
                "yyyy-MM-dd",
                "MM-dd-yyyy",
                "dd/MM/yyyy"
                };

            if (formats == null || !formats.Any())
            {
                formats = CUSTOM_DATE_FORMATS;
            }

            DateTime validDate;

            foreach (var format in formats)
            {
                if (format.EndsWith("Z"))
                {
                    if (DateTime.TryParseExact(dateToParse, format,
                             provider,
                             DateTimeStyles.AssumeUniversal,
                             out validDate))
                    {
                        return validDate;
                    }
                }

                if (DateTime.TryParseExact(dateToParse, format,
                         provider, styles, out validDate))
                {
                    return validDate;
                }
            }

            return null;
        }


        /// <summary>
        /// Calculates age based on todays date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int CalculateAge(this DateTime date)
        {

            // Save today's date.
            var today = DateTime.UtcNow;
            // Calculate the age.
            int age = today.Year - date.Year;
            // Go back to the year the person was born in case of a leap year
            if (date.Date > today.AddYears(-age)) age--;

            return age;
        }

        public static long ToTimestamp(this DateTime value)
        {
            TimeSpan elapsedTime = value - Epoch;
            return (long)elapsedTime.TotalSeconds * 1000;
        }

        public static long ToJavaScriptMilliseconds(this DateTime date)
        {
            return (date.ToUniversalTime().Ticks - DatetimeMinTimeTicks) / 10000;
        }

    }
}

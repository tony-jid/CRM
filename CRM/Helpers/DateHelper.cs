using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Helpers
{
    public static class DateHelper
    {
        public const string MAIN_TIMEZONE = "AUS Eastern Standard Time";

        public const string FORMAT_SHORT_MONTH_STR = "d MMM yyyy";

        /// <summary>
        /// Return UTC (Coordinated Universal Time) DateTime 
        /// </summary>
        public static DateTime Now
        {
            get
            {
                return DateTime.UtcNow;
            }
        }

        public static DateTime ConvertToUtc(DateTime dateTime)
        {
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(DateHelper.MAIN_TIMEZONE);
            var convertedDate = TimeZoneInfo.ConvertTimeToUtc(dateTime, timeZone);

            return convertedDate;
        }

        public static DateTime ConvertFromUtc(DateTime dateTime)
        {
            TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(DateHelper.MAIN_TIMEZONE);
            var convertedDate = TimeZoneInfo.ConvertTimeFromUtc(dateTime, timeZone);

            return convertedDate;
        }
    }
}

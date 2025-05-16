using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Finance.API.Common
{
    public static class DateHelper
    {
        public static DateTime GetDateTimePH()
        {
            TimeZoneInfo phTimeZone = TimeZoneInfo.FindSystemTimeZoneById(
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                    ? "Singapore Standard Time"    // Windows
                    : "Asia/Manila"                // Linux/macOS
            );

            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, phTimeZone);
        }

        public static DateTime GetStartOfDay(this DateTime dateTime)
        {
            return dateTime.Date;
        }

        public static DateTime GetEndOfDay(this DateTime dateTime)
        {
            return dateTime.Date.AddDays(1).AddTicks(-1);
        }

        public static DateTime GetStartOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public static DateTime GetEndOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1).AddTicks(1);
        }
    }
}

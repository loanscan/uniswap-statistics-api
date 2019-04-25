using System;
using System.Numerics;

namespace Uniswap.Common
{
    public static class TypeExtensions
    {
        public static long ToUnixTimestamp(this DateTime dateTime)
        {
            var epochTicks = new DateTime(1970, 1, 1).Ticks;
            return (dateTime.Ticks - epochTicks) / TimeSpan.TicksPerSecond;
        }

        public static DateTime ToUnixDateTime(this BigInteger value)
        {
            return ((long) value).ToUnixDateTime();
        }

        public static DateTime ToUnixDateTime(this long value)
        {
            return DateTimeOffset.FromUnixTimeSeconds(value).DateTime;
        }
        
        public static DateTime ToUnixUtcDateTime(this long value)
        {
            var dateTime = DateTimeOffset.FromUnixTimeSeconds(value);
            var utcDateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second,
                dateTime.Millisecond, DateTimeKind.Utc);
            return utcDateTime;
        }
    }
}
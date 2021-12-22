using System;

namespace NorthernLights
{
    public static class Extensions
    {
        /// <summary>
        /// Converts a Unix timestamp to datetime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        private static DateTime GetDateTimeFromUnixTimeStamp(this double timeStamp)
        {
            var date = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            date=date.AddSeconds(timeStamp).ToLocalTime();
            return date;
        }

        public static string GetTime(this double timeStamp)
        {
            return timeStamp.GetDateTimeFromUnixTimeStamp().ToShortTimeString();
        }

        public static string GetDate(this double timeStamp)
        {
            return timeStamp.GetDateTimeFromUnixTimeStamp().ToShortDateString();
        }

    }
}

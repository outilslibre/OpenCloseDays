using System;
using System.Collections.Generic;
using System.Globalization;

namespace OpenCloseDays.Extensions
{
    public static class Extensions
    {
        public static IEnumerable<DateTime> ParseAsDatesList(this string datesString)
        {
            if (string.IsNullOrEmpty(datesString))
                return Array.Empty<DateTime>();

            var parsedDates = new List<DateTime>();
            foreach (var dateString in datesString.Split(';', ','))
            {
                DateTime parsedDate;
                if (DateTime.TryParse(dateString,
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.AllowWhiteSpaces,
                            out parsedDate))
                    parsedDates.Add(parsedDate);
            }
            return parsedDates;
        }

        public static float ParseAsFloat(this string s)
        {
            if (!float.TryParse((s ?? "").Replace(",", "."), NumberStyles.Number, CultureInfo.InvariantCulture, out var res))
                return 0f;

            return res;
        }

        public static bool CanParseAsFloat(this string s) =>
            float.TryParse((s ?? "").Replace(",", "."), NumberStyles.Number, CultureInfo.InvariantCulture, out _);

        public static string AsFloatInvariant(this float f) =>
            f.ToString(CultureInfo.InvariantCulture);

        public static string ToHoursMinutes(this float f)
        {
            var hoursPart = Math.Truncate(f);
            var minutesPart = (f - Math.Truncate(f)) * 60;
            return minutesPart > 0 ?  $"{hoursPart:0}h{minutesPart:0}" : $"{hoursPart}h";
        }
    }
}

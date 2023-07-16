using System;
using System.Globalization;

namespace OpenCloseDays.Models
{
    public class MonthWorkHours
    {
        public MonthWorkHours(int month, int year, float workedHours, DateWorkHours[] datesWorkHours)
        {
            Month = month;
            Year = year;
            WorkedHours = workedHours;
            DatesWorkHours = datesWorkHours;
        }

        public int Month { get; }
        public int Year { get; }
        public string MonthName => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month));
        public float WorkedHours { get; }
        public DateWorkHours[] DatesWorkHours { get; }
    }
}

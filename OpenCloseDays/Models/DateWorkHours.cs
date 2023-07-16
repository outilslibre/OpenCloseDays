using System;

namespace OpenCloseDays.Models
{
    public class DateWorkHours
    {
        public DateWorkHours(DateTime date, float workedHours)
        {
            Date = date;
            WorkedHours = workedHours;
        }

        public DateTime Date { get; }
        public float WorkedHours { get; }
    }
}

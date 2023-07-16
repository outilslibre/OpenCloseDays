using System;
using System.Linq;
using System.Collections.Generic;

namespace OpenCloseDays.Models
{

    public class WorkDaysComputationResult
    {
        public IReadOnlyCollection<(DateTime Date, string Name)> PublicHolidayDays { get; internal set; }
        public int TotalOpenDays { get; internal set; }
        public float TotalOpenHours { get; internal set; }
        public int TotalCloseDays { get; internal set; }
        public int TotalSchoolHolidayDays { get; internal set; }

        public DateTime StartDate { get; internal set; }
        public DateTime EndDate { get; internal set; }
        public int MonthsInPeriod => (EndDate.Year - StartDate.Year) * 12 + EndDate.Month - StartDate.Month + 1;

        public IEnumerable<MonthWorkHours> MonthsWorkHours
        {
            get
            {
                return (from dateWorkHours in DatesWorkHours
                        group dateWorkHours by new { dateWorkHours.Date.Month, dateWorkHours.Date.Year } into monthGroup
                        select new MonthWorkHours(monthGroup.Key.Month, monthGroup.Key.Year, 
                            monthGroup.Select(dateWorkHours => dateWorkHours.WorkedHours).DefaultIfEmpty(0).Sum(),
                            monthGroup.ToArray()));
            }
        }

        public DaysWorkHours WorkHoursPerWeekDays { get; internal set; }
        public IReadOnlyCollection<DateWorkHours> DatesWorkHours { get; internal set; }
        public DateTime[] ExcludedDateTimes { get; internal set; }
    }
}

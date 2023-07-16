using OpenCloseDays.Models;
using PublicHoliday;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OpenCloseDays
{
    public class OpenCloseDaysService
    {
        private readonly PublicHolidaysService publicHolidaysService;

        public OpenCloseDaysService(
            PublicHolidaysService publicHolidaysService
            )
        {
            this.publicHolidaysService = publicHolidaysService;
        }

        public IEnumerable<(string code, string name)> GetHandledCountries()
            => publicHolidaysService.GetHandledCountries();

        public IEnumerable<(DateTime Date, string Name)> GetLongWeekEnds(
            string country,
            DateTime startDate, DateTime endDate,
            params DayOfWeek[] longWeekEndsDays)
        {
            if (endDate < startDate)
                throw new ArgumentException("endDate must be after startDate", nameof(endDate));

            if (null == longWeekEndsDays
                || longWeekEndsDays.Length == 0)
                longWeekEndsDays = new[] {
                    DayOfWeek.Monday, DayOfWeek.Tuesday,
                    DayOfWeek.Thursday, DayOfWeek.Friday
                };
            return publicHolidaysService.GetPublicHolidaysForPeriod(country, startDate, endDate)
                .Where(h => longWeekEndsDays.Contains(h.Date.DayOfWeek));
        }

        public WorkDaysComputationResult ComputeWorkDays(
            string country,
            DateTime startDate, DateTime endDate,
            DaysWorkHours workHoursPerWeekDays,
            string[] workedPublicHolidays,
            Func<DateTime, bool> inSchoolHoliday,
            params DateTime[] excludedDates
            )
        {
            if (endDate < startDate)
                throw new ArgumentException("endDate must be after startDate", nameof(endDate));

            var publicHolidayDays = publicHolidaysService.GetPublicHolidaysForPeriod(country, startDate, endDate).ToList();

            workedPublicHolidays ??= Array.Empty<string>();
            excludedDates ??= Array.Empty<DateTime>();

            var notWorkedHolidays = publicHolidayDays.Where(h => !workedPublicHolidays.Any(wh => string.Equals(wh, h.Name, StringComparison.InvariantCultureIgnoreCase))).ToArray();

            var datesWorkHours = new List<DateWorkHours>();
            var openDays = 0;
            var openHours = 0.0f;
            var closeDays = 0;
            var schoolHolidayDaysCount = 0;
            var currentDay = startDate;
            while (currentDay <= endDate)
            {
                var weekDay = currentDay.DayOfWeek;
                var hoursForCurrentDay = (int)(typeof(DaysWorkHours)
                    .GetProperty($"{weekDay}WorkHours")
                    ?.GetValue(workHoursPerWeekDays) ?? 0);
                var minutesForCurrentDay = (int)(typeof(DaysWorkHours)
                    .GetProperty($"{weekDay}WorkMinutes")
                    ?.GetValue(workHoursPerWeekDays) ?? 0);
                var timeForCurrentDay = hoursForCurrentDay + (minutesForCurrentDay / 60.0f);
                var inSchoolHolidays = inSchoolHoliday?.Invoke(currentDay.Date) ?? false;
                if ((hoursForCurrentDay > 0 || minutesForCurrentDay > 0)
                    && !notWorkedHolidays.Any(h => h.Date == currentDay.Date)
                    && !excludedDates.Any(d => d.Date == currentDay.Date)
                    && !inSchoolHolidays)
                {
                    openDays += 1;
                    openHours += timeForCurrentDay;
                    datesWorkHours.Add(new DateWorkHours(currentDay, timeForCurrentDay));
                }
                else
                {
                    closeDays += 1;
                    datesWorkHours.Add(new DateWorkHours(currentDay, 0));
                }
                if (inSchoolHolidays)
                    schoolHolidayDaysCount += 1;

                currentDay = currentDay.AddDays(1);
            }

            return new WorkDaysComputationResult()
            {
                StartDate = startDate,
                EndDate = endDate,
                TotalCloseDays = closeDays,
                PublicHolidayDays = publicHolidayDays,
                TotalSchoolHolidayDays = schoolHolidayDaysCount,
                TotalOpenDays = openDays,
                TotalOpenHours = openHours,
                WorkHoursPerWeekDays = workHoursPerWeekDays,
                DatesWorkHours = datesWorkHours,
                ExcludedDateTimes = excludedDates.Where(d => startDate.Date <= d.Date && d.Date <= endDate.Date).ToArray()
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OpenCloseDays.Tests
{
    public class OpenCloseDaysServiceTests
    {
        [Theory]
        [MemberData(nameof(ComputationData))]
        public void ComputeWorkDays_ShouldCompute(
            string country, DateTime startDate, DateTime endDate, Models.DaysWorkHours workHoursPerWeekDays, string[] workedPublicHolidays, Func<DateTime, bool> isSchoolHoliday, DateTime[] excludedDates,
            int exptected_TotalCloseDays, int expected_TotalOpenDays, float expected_TotalOpenHours, int expected_TotalSchoolHolidayDays, int expected_MonthsInPeriod
            )
        {
            var publicHolidaysService = new PublicHolidaysService();
            var openCloseDaysService = new OpenCloseDaysService(publicHolidaysService);

            var result = openCloseDaysService.ComputeWorkDays(country, startDate, endDate, workHoursPerWeekDays, workedPublicHolidays, isSchoolHoliday, excludedDates);

            Assert.Equal(startDate, result.StartDate);
            Assert.Equal(endDate, result.EndDate);
            Assert.Equal(workHoursPerWeekDays, result.WorkHoursPerWeekDays);

            Assert.Equal(exptected_TotalCloseDays, result.TotalCloseDays);
            Assert.Equal(expected_TotalOpenDays, result.TotalOpenDays);
            Assert.Equal(expected_TotalOpenHours, result.TotalOpenHours);
            Assert.Equal(expected_TotalSchoolHolidayDays, result.TotalSchoolHolidayDays);
            Assert.Equal(expected_MonthsInPeriod, result.MonthsInPeriod);

            Assert.Equal(excludedDates ?? Array.Empty<DateTime>(), result.ExcludedDateTimes);

            Assert.Equal((endDate - startDate).TotalDays + 1, result.DatesWorkHours.Count());
            Assert.Equal(result.TotalOpenHours, result.DatesWorkHours.Select(d => d.WorkedHours).Sum());
            Assert.Equal(publicHolidaysService.GetPublicHolidaysForPeriod(country, startDate, endDate).Count(), result.PublicHolidayDays.Count);
        }

        public static IEnumerable<object[]> ComputationData => new List<object[]>
        {
            new object[] {"fr", new DateTime(2022,11,1), new DateTime(2022, 11, 30), new Models.DaysWorkHours(), null, null, null,
                            10, 20, 140, 0, 1},
            new object[] {"fr", new DateTime(2022,11,1), new DateTime(2022, 11, 30), new Models.DaysWorkHours(), new[]{"Jour de l'armistice"}, null, null,
                            9, 21, 147, 0, 1},
            new object[] {"fr", new DateTime(2022,11,1), new DateTime(2022, 11, 30), new Models.DaysWorkHours(), null, (Func<DateTime, bool>)((d) => d.Date == new DateTime(2022,11,4)), null,
                            11, 19, 133, 1, 1},
            new object[] {"fr", new DateTime(2022,11,1), new DateTime(2022, 11, 30), new Models.DaysWorkHours(), null, null, new[] { new DateTime(2022, 11, 24), new DateTime(2022, 11, 25) },
                            12, 18, 126, 0, 1},
            new object[] {"fr", new DateTime(2022,11,1), new DateTime(2022, 11, 30), new Models.DaysWorkHours(7,30,7,0,7,0,7,0,6,0), null, null, null,
                            10, 20, 139, 0, 1},
        };

        [Fact]
        public void ComputeWorkDays_ShouldThrowIfRevertedDates()
        {
            var publicHolidaysService = new PublicHolidaysService();
            var openCloseDaysService = new OpenCloseDaysService(publicHolidaysService);

            Assert.Throws<ArgumentException>(() => openCloseDaysService.ComputeWorkDays("fr", new DateTime(2022, 11, 25), new DateTime(2022, 11, 10), null, null, null));
        }

    }
}

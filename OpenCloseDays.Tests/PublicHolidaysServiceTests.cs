using System;
using System.Linq;
using Xunit;

namespace OpenCloseDays.Tests
{
    public class PublicHolidaysServiceTests
    {
        [Fact]
        public void GetHandledCountries_ContainsFrance()
        {
            var publicHolidaysService = new PublicHolidaysService();

            var result = publicHolidaysService.GetHandledCountries();
            Assert.Contains(("fr", "France"), result);
        }

        [Fact]
        public void GetCountryName_ShouldReturnCountryNameForKnown()
        {
            var publicHolidaysService = new PublicHolidaysService();

            var result = publicHolidaysService.GetCountryName("fr");
            Assert.Equal("France", result);
        }

        [Fact]
        public void GetCountryName_ShouldReturnCountryCodeForUnknown()
        {
            var publicHolidaysService = new PublicHolidaysService();

            var result = publicHolidaysService.GetCountryName("zz");
            Assert.Equal("zz", result);
        }

        [Fact]
        public void GetPublicHolidaysForYear_ShouldThrowIfUnknowCountry()
        {
            var publicHolidaysService = new PublicHolidaysService();
            Assert.Throws<NotImplementedException>(() => publicHolidaysService.GetPublicHolidaysForYear("zz", 2022));
        }

        [Fact]
        public void GetPublicHolidaysForYear_ShouldReturnFrancePublicHolidays()
        {
            var publicHolidaysService = new PublicHolidaysService();
            Assert.Equal(11, publicHolidaysService.GetPublicHolidaysForYear("fr", 2022).Count);
        }

        [Fact]
        public void GetPublicHolidaysForPeriod_ShouldThrowIfUnknowCountry()
        {
            var publicHolidaysService = new PublicHolidaysService();
            Assert.Throws<NotImplementedException>(() => publicHolidaysService.GetPublicHolidaysForPeriod("zz", new DateTime(2022, 11, 1), new DateTime(2022, 11, 30)));
        }

        [Fact]
        public void GetPublicHolidaysForPeriod_ShouldThrowIfRevertedDates()
        {
            var publicHolidaysService = new PublicHolidaysService();
            Assert.Throws<ArgumentException>(() => publicHolidaysService.GetPublicHolidaysForPeriod("fr", new DateTime(2022, 11, 30), new DateTime(2022, 11, 1)));
        }

        [Fact]
        public void GetPublicHolidaysForPeriod_ShouldReturnFrancePublicHolidays()
        {
            var publicHolidaysService = new PublicHolidaysService();
            var publicHolidaysDates = publicHolidaysService.GetPublicHolidaysForPeriod("fr", new DateTime(2022, 11, 1), new DateTime(2022, 11, 30)).Select(p => p.Date).ToArray();
            Assert.Equal(new[] { new DateTime(2022, 11, 1), new DateTime(2022, 11, 11) }, publicHolidaysDates);
        }
    }
}

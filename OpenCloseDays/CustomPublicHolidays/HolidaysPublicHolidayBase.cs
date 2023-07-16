using PublicHoliday;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenCloseDays.CustomPublicHolidays
{
    internal abstract class HolidaysPublicHolidayBase : PublicHolidayBase
    {
        protected abstract Holidays.NationalHolidays NationalHolidays { get; }
        public override bool IsPublicHoliday(DateTime dt)
        {
            return NationalHolidays.OfYear(dt.Year).Any(holidayData => holidayData.Value.Date.Equals(dt.Date));
        }

        public override IDictionary<DateTime, string> PublicHolidayNames(int year)
        {
            return NationalHolidays.OfYear(year).ToDictionary(holidayData => holidayData.Value, holidayData => holidayData.Key);
        }

        public override IList<DateTime> PublicHolidays(int year)
        {
            return NationalHolidays.OfYear(year).Select(holidayData => holidayData.Value.Date).ToList();
        }
    }
}

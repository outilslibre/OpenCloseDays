using Holidays;
using PublicHoliday;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenCloseDays.CustomPublicHolidays
{
    internal class PortuguesePublicHoliday : HolidaysPublicHolidayBase
    {
        protected override NationalHolidays NationalHolidays => Holidays.NationalHolidays.FromPortugal;
    }
}

using Holidays;
using PublicHoliday;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenCloseDays.CustomPublicHolidays
{
    internal class BrazilPublicHoliday : HolidaysPublicHolidayBase
    {
        protected override NationalHolidays NationalHolidays => Holidays.NationalHolidays.FromBrazil;
    }
}

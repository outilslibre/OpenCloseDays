using PublicHoliday;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCloseDays.CustomPublicHolidays
{
    internal class NoPublicHoliday : PublicHolidayBase
    {
        public NoPublicHoliday() { }

        public override bool IsPublicHoliday(DateTime dt) => false;

        public override IDictionary<DateTime, string> PublicHolidayNames(int year) => new Dictionary<DateTime, string>();

        public override IList<DateTime> PublicHolidays(int year) => new List<DateTime>();
    }

}

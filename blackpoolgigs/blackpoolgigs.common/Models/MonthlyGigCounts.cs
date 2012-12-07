using System.Collections.Generic;
using System.Globalization;

namespace blackpoolgigs.common.Models
{
    public class MonthlyGigCounts
    {
        public MonthCount[] Counts;

        public MonthlyGigCounts()
        {
            Counts = new MonthCount[] { };
        }
        public MonthCount Get(int month, int year)
        {
            foreach (var count in Counts)
            {
                if (count.Month == new DateTimeFormatInfo().GetMonthName(month) &&
                    count.Year == year)
                    return count;
            }
            return new MonthCount
                       {
                           Month = new DateTimeFormatInfo().GetMonthName(month),
                           Year = year,
                           Count = 0
                       };
        }
        public MonthCount Get(int month, string year)
        {
            return Get(month, int.Parse(year));
        }
        public MonthlyGigCounts ForYear(int year)
        {
            var list = new List<MonthCount>();
            for (var i = 1; i <= 12; i++)
            {
                list.Add(Get(i, year));
            }
            return new MonthlyGigCounts()
                       {
                           Counts = list.ToArray()
                       };
        }
    }
}
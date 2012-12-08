using System;
using System.Globalization;

namespace coolbunny.common.Extensions
{
    public static class dateExtensions
    {
        public static DateTime GetMondayOfWeek(this DateTime input)
        {
            while (input.DayOfWeek != DayOfWeek.Monday)
                input = input.AddDays(-1);
            return input;
        }

        public static DateTime GetSundayOfWeek(this DateTime input)
        {
            while (input.DayOfWeek != DayOfWeek.Sunday)
                input = input.AddDays(+1);
            return input;
        }
        public static DateTime FirstDateOfWeek(int year, int weekNum, CalendarWeekRule rule)
        {
            var jan1 = new DateTime(year, 1, 1);

            var daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;
            var firstMonday = jan1.AddDays(daysOffset);

            var cal = CultureInfo.CurrentCulture.Calendar;
            var firstWeek = cal.GetWeekOfYear(jan1, rule, DayOfWeek.Monday);

            if (firstWeek <= 1)
                weekNum -= 1;

            return firstMonday.AddDays(weekNum * 7);
        }

        public static int GetWeekNumber(this DateTime date)
        {
            const int JAN = 1;
            const int DEC = 12;
            const int LASTDAYOFDEC = 31;
            const int FIRSTDAYOFJAN = 1;
            const int THURSDAY = 4;
            var ThursdayFlag = false;

            var DayOfYear = date.DayOfYear;

            var StartWeekDayOfYear = (int)(new DateTime(date.Year, JAN, FIRSTDAYOFJAN)).DayOfWeek;
            var EndWeekDayOfYear = (int)(new DateTime(date.Year, DEC, LASTDAYOFDEC)).DayOfWeek;

            if (StartWeekDayOfYear == 0)
                StartWeekDayOfYear = 7;
            if (EndWeekDayOfYear == 0)
                EndWeekDayOfYear = 7;

            var DaysInFirstWeek = 8 - (StartWeekDayOfYear);

            if (StartWeekDayOfYear == THURSDAY || EndWeekDayOfYear == THURSDAY)
                ThursdayFlag = true;

            var FullWeeks = (int)Math.Ceiling((DayOfYear - (DaysInFirstWeek)) / 7.0);

            var WeekNumber = FullWeeks;

            if (DaysInFirstWeek >= THURSDAY)
                WeekNumber = WeekNumber + 1;

            if (WeekNumber > 52 && !ThursdayFlag)
                WeekNumber = 1;

            if (WeekNumber == 0)
                WeekNumber = new DateTime(date.Year - 1, DEC, LASTDAYOFDEC).GetWeekNumber();
            return WeekNumber;
        }

        public static string SafeDate(this DateTime date)
        {
            if (date == DateTime.MinValue)
                return string.Empty;
            if (date == new DateTime(1, 1, 1, 1, 1, 1))
                return string.Empty;
            return date.ToString("dd-MMM-yyyy");
        }
        public static string SafeDate(this DateTime? date)
        {
            if (date.HasValue)
                return date.Value.SafeDate();
            return string.Empty;
        }
    }
}
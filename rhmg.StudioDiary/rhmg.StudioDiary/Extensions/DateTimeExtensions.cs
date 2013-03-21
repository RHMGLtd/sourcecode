using System;

namespace rhmg.StudioDiary.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime MondayDate(this DateTime inputDate)
        {
            switch (inputDate.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return inputDate;
                case DayOfWeek.Tuesday:
                    return inputDate.AddDays(-1);
                case DayOfWeek.Wednesday:
                    return inputDate.AddDays(-2);
                case DayOfWeek.Thursday:
                    return inputDate.AddDays(-3);
                case DayOfWeek.Friday:
                    return inputDate.AddDays(-4);
                case DayOfWeek.Saturday:
                    return inputDate.AddDays(-5);
                case DayOfWeek.Sunday:
                    return inputDate.AddDays(-6);
                default:
                    throw new ArgumentOutOfRangeException("inputDate");
            }
        }

        public static DateTime SundayDate(this DateTime inputDate)
        {
            switch (inputDate.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return inputDate.AddDays(6);
                case DayOfWeek.Tuesday:
                    return inputDate.AddDays(5);
                case DayOfWeek.Wednesday:
                    return inputDate.AddDays(4);
                case DayOfWeek.Thursday:
                    return inputDate.AddDays(3);
                case DayOfWeek.Friday:
                    return inputDate.AddDays(2);
                case DayOfWeek.Saturday:
                    return inputDate.AddDays(1);
                case DayOfWeek.Sunday:
                    return inputDate;
                default:
                    throw new ArgumentOutOfRangeException("inputDate");
            }
        }
        public static DateTime FirstDayOfMonth(this DateTime inputDate)
        {
            return new DateTime(inputDate.Year, inputDate.Month, 1);
        }
        public static DateTime LastDayOfMonth(this DateTime inputDate)
        {
            return new DateTime(inputDate.Year, inputDate.Month, DateTime.DaysInMonth(inputDate.Year, inputDate.Month));
        }
    }
}
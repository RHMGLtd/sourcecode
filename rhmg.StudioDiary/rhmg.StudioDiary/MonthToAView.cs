using System;
using System.Collections.Generic;
using System.Linq;

namespace rhmg.StudioDiary
{
    public class MonthToAView
    {
        public List<DaySummary> WithPeakAvailability { get; set; }

        public MonthToAView(List<Booking> bookings, DateTime date)
        {
            WithPeakAvailability = new List<DaySummary>();
            Init(bookings, date);
        }

        void Init(List<Booking> bookings, DateTime date)
        {
            //for each day of the month requested
            var dateInMonth = new DateTime(date.Year, date.Month, 1);
            while (dateInMonth.Month == date.Month)
            {
                // if today is a weekday
                if (dateInMonth.DayOfWeek != DayOfWeek.Saturday && dateInMonth.DayOfWeek != DayOfWeek.Sunday)
                {
                    // get any bookings for this date which are peak time
                    var thisDaysBookings = bookings.Where(x => x.Date == dateInMonth && x.IsInWeekdayPeakTime());
                    // if we have four or more bookings then we are full
                    if (thisDaysBookings.Count() < 4)
                        WithPeakAvailability.Add(new DaySummary(dateInMonth));
                }
                dateInMonth = dateInMonth.AddDays(1);
            }
        }
    }

    public class DaySummary
    {
        public DaySummary(DateTime dateInMonth)
        {
            Date = dateInMonth;
        }

        public DateTime Date { get; set; }
    }
}
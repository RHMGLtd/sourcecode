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
                // get any bookings for this date which are peak time
                var thisDaysBookings = bookings.Where(x => x.Date == dateInMonth);
                WithPeakAvailability.Add(new DaySummary(dateInMonth, thisDaysBookings));
                dateInMonth = dateInMonth.AddDays(1);
            }
        }
    }

    public class DaySummary
    {
        public DaySummary(DateTime dateInMonth, IEnumerable<Booking> thisDaysBookings)
        {
            Date = dateInMonth;
            ThisDaysBookings = thisDaysBookings;
            Init();
        }

        void Init()
        {
            // if today is a saturday
            if (Date.DayOfWeek == DayOfWeek.Saturday)
            {
                // if we have a recording longer than 8 hours we are full
                HasAvailability = !ThisDaysBookings.Any(x => x.Rooms.Any(y => y.Name == "Live Room") && x.Length >= new TimeSpan(0, 8, 0, 0));
                return;
            }
            // if today is a sunday
            if (Date.DayOfWeek == DayOfWeek.Sunday)
            {
                HasAvailability = true;
                return;
            }
            // if today is a weekday
            HasAvailability = ThisDaysBookings.Count(x => x.IsInWeekdayPeakTime()) < 4;
        }

        public DateTime Date { get; set; }
        public IEnumerable<Booking> ThisDaysBookings { get; set; }
        public bool HasAvailability { get; set; }
    }
}
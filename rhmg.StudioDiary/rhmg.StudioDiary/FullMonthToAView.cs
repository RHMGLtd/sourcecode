using System;
using System.Collections.Generic;
using System.Linq;

namespace rhmg.StudioDiary
{
    public class FullMonthToAView
    {
        public List<DaySummary> WithPeakAvailability { get; set; }
        public FullMonthToAView(List<Booking> bookings, DateTime startDate, DateTime endDate)
        {
            WithPeakAvailability = new List<DaySummary>();
            Init(bookings, startDate, endDate);
        }

        void Init(List<Booking> bookings, DateTime startDate, DateTime endDate)
        {
            var currentDate = startDate;
            while (currentDate <= endDate)
            {
                WithPeakAvailability.Add(new DaySummary(currentDate, bookings.Where(x => x.Date == currentDate)));
                currentDate = currentDate.AddDays(1);
            }
        }
    }

    public class DaySummary
    {
        public DateTime Date { get; set; }
        public IEnumerable<Booking> ThisDaysBookings { get; set; }
        public bool HasAvailability { get; set; }
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
                HasAvailability = !ThisDaysBookings.Any(x => x.Rooms.Any(y => y.Name == "Live Room") && x.Length >= new TimeSpan(0, 8, 0, 0))
                    // if we have a sum of more than eight hours in the day we are full
                    || ThisDaysBookings.Where(x => x.Rooms.Any(y => y.Name == "Live Room")).Sum(y => y.Length.TotalHours) < 8 ;
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
    }
}
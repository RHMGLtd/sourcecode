using System;
using System.Collections.Generic;

namespace rhmg.StudioDiary
{
    public class WeekToAView
    {
        public WeekToAView(IEnumerable<Booking> bookings, IEnumerable<BackFill> backFill, DateTime mondayDate, DateTime sundayDate)
        {
            MondayDate = mondayDate;
            SundayDate = sundayDate;
            Monday = new DayViewBookingLists { Date = mondayDate };
            Tuesday = new DayViewBookingLists { Date = mondayDate.AddDays(1) };
            Wednesday = new DayViewBookingLists { Date = mondayDate.AddDays(2) };
            Thursday = new DayViewBookingLists { Date = mondayDate.AddDays(3) };
            Friday = new DayViewBookingLists { Date = mondayDate.AddDays(4) };
            Saturday = new DayViewBookingLists { Date = mondayDate.AddDays(5) };
            Sunday = new DayViewBookingLists { Date = sundayDate };
            Init(bookings);
            Init(backFill);
        }

        void Init(IEnumerable<Booking> bookings)
        {
            foreach (var booking in bookings)
            {
                if (booking.Date.DayOfWeek == DayOfWeek.Monday)
                    Monday.Bookings.Add(booking);
                if (booking.Date.DayOfWeek == DayOfWeek.Tuesday)
                    Tuesday.Bookings.Add(booking);
                if (booking.Date.DayOfWeek == DayOfWeek.Wednesday)
                    Wednesday.Bookings.Add(booking);
                if (booking.Date.DayOfWeek == DayOfWeek.Thursday)
                    Thursday.Bookings.Add(booking);
                if (booking.Date.DayOfWeek == DayOfWeek.Friday)
                    Friday.Bookings.Add(booking);
                if (booking.Date.DayOfWeek == DayOfWeek.Saturday)
                    Saturday.Bookings.Add(booking);
                if (booking.Date.DayOfWeek == DayOfWeek.Sunday)
                    Sunday.Bookings.Add(booking);
            }
        }
        void Init(IEnumerable<BackFill> backFills)
        {
            foreach (var backFill in backFills)
            {
                if (backFill.Date.DayOfWeek == DayOfWeek.Monday)
                    Monday.BackFill.Add(backFill);
                if (backFill.Date.DayOfWeek == DayOfWeek.Tuesday)
                    Tuesday.BackFill.Add(backFill);
                if (backFill.Date.DayOfWeek == DayOfWeek.Wednesday)
                    Wednesday.BackFill.Add(backFill);
                if (backFill.Date.DayOfWeek == DayOfWeek.Thursday)
                    Thursday.BackFill.Add(backFill);
                if (backFill.Date.DayOfWeek == DayOfWeek.Friday)
                    Friday.BackFill.Add(backFill);
                if (backFill.Date.DayOfWeek == DayOfWeek.Saturday)
                    Saturday.BackFill.Add(backFill);
                if (backFill.Date.DayOfWeek == DayOfWeek.Sunday)
                    Sunday.BackFill.Add(backFill);
            }
        }

        public DayViewBookingLists Monday { get; set; }
        public DayViewBookingLists Tuesday { get; set; }
        public DayViewBookingLists Wednesday { get; set; }
        public DayViewBookingLists Thursday { get; set; }
        public DayViewBookingLists Friday { get; set; }
        public DayViewBookingLists Saturday { get; set; }
        public DayViewBookingLists Sunday { get; set; }
        public DateTime MondayDate { get; set; }
        public DateTime SundayDate { get; set; }
    }
}
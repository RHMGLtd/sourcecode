using System;
using System.Collections.Generic;

namespace rhmg.StudioDiary
{
    public class WeekToAView
    {
        public WeekToAView(IEnumerable<Booking> bookings)
        {
            Monday = new DayViewBookingLists();
            Tuesday = new DayViewBookingLists();
            Wednesday = new DayViewBookingLists();
            Thursday = new DayViewBookingLists();
            Friday = new DayViewBookingLists();
            Saturday = new DayViewBookingLists();
            Sunday = new DayViewBookingLists();
            Init(bookings);
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

        public DayViewBookingLists Monday { get; set; }
        public DayViewBookingLists Tuesday { get; set; }
        public DayViewBookingLists Wednesday { get; set; }
        public DayViewBookingLists Thursday { get; set; }
        public DayViewBookingLists Friday { get; set; }
        public DayViewBookingLists Saturday { get; set; }
        public DayViewBookingLists Sunday { get; set; }
    }
}
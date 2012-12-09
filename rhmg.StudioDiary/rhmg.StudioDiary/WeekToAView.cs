using System;
using System.Collections.Generic;

namespace rhmg.StudioDiary
{
    public class WeekToAView
    {
        public WeekToAView(IEnumerable<Booking> bookings)
        {
            Monday = new DayView();
            Tuesday = new DayView();
            Wednesday = new DayView();
            Thursday = new DayView();
            Friday = new DayView();
            Saturday = new DayView();
            Sunday = new DayView();
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

        public DayView Monday { get; set; }
        public DayView Tuesday { get; set; }
        public DayView Wednesday { get; set; }
        public DayView Thursday { get; set; }
        public DayView Friday { get; set; }
        public DayView Saturday { get; set; }
        public DayView Sunday { get; set; }
    }
}
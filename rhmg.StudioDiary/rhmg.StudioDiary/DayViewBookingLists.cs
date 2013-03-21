using System;
using System.Collections.Generic;

namespace rhmg.StudioDiary
{
    public class DayViewBookingLists
    {
        public BookingList Bookings { get; set; }
        public List<BackFill> BackFill { get; set; } 
        public DateTime Date { get; set; }

        public DayViewBookingLists()
        {
            Bookings = new BookingList();
            BackFill = new List<BackFill>();
        }
        public BookingList ForRoom(Room toCheck)
        {
            return Bookings.ForRoom(toCheck, Date.DayOfWeek);
        }
    }
}

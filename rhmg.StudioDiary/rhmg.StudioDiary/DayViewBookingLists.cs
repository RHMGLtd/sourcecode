using System;

namespace rhmg.StudioDiary
{
    public class DayViewBookingLists
    {
        public BookingList Bookings { get; set; }
        public DateTime Date { get; set; }

        public DayViewBookingLists()
        {
            Bookings = new BookingList();
        }
        public BookingList ForRoom(Room toCheck)
        {
            return Bookings.ForRoom(toCheck, Date.DayOfWeek);
        }
    }
}
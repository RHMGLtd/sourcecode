using System;
using System.Collections.Generic;
using System.Linq;

namespace rhmg.StudioDiary
{
    public class DayViewBookingLists
    {
        public List<Booking> Bookings { get; set; }
        public DateTime Date { get; set; }

        public List<Booking> ForRoom(Room toCheck)
        {
            var result = Bookings.Where(x => x.Room.Id == toCheck.Id).ToList();
            return !result.Any() ? new List<Booking>() : result;
        }

        public DayViewBookingLists()
        {
            Bookings = new List<Booking>();
        }
    }
}
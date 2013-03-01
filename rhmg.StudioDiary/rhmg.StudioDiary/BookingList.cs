using System;
using System.Collections.Generic;
using System.Linq;

namespace rhmg.StudioDiary
{
    public class BookingList
    {
        public BookingList()
        {
            Bookings = new List<Booking>();
        }
        public BookingList(List<Booking> bookings)
        {
            Bookings = bookings;
        }

        public List<Booking> Bookings { get; private set; }
        public bool IsAWeekend { get; private set; }
        public BookingList ForRoom(Room toCheck, DayOfWeek day)
        {
            var result = Bookings.Where(x => x.Room.Id == toCheck.Id).ToList();
            return new BookingList { Bookings = result, IsAWeekend = (day == DayOfWeek.Saturday || day == DayOfWeek.Sunday) };
        }
        public Booking ForHour(int hour)
        {
            return Bookings.FirstOrDefault(x => x.IsValidFor(hour)) ?? Booking.GetNull();
        }

        public void Add(Booking booking)
        {
            Bookings.Add(booking);
        }
    }
}
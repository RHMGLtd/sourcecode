using System.Collections.Generic;
using System.Linq;

namespace rhmg.StudioDiary
{
    public class DayViewBookingLists
    {
        public List<Booking> Bookings { get; set; }

        public List<Booking> Room2
        {
            get
            {
                return Bookings.Where(x => x.Room.Name == "Room 2").ToList();
            }
        }
        public List<Booking> Room3
        {
            get
            {
                return Bookings.Where(x => x.Room.Name == "Room 3").ToList();
            }
        }
        public List<Booking> Room4
        {
            get
            {
                return Bookings.Where(x => x.Room.Name == "Room 4").ToList();
            }
        }
        public List<Booking> LiveRoom
        {
            get
            {
                return Bookings.Where(x => x.Room.Name == "Live Room").ToList();
            }
        }
        public List<Booking> ControlRoom
        {
            get
            {
                return Bookings.Where(x => x.Room.Name == "Control Room").ToList();
            }
        }

        public DayViewBookingLists()
        {
            Bookings = new List<Booking>();
        }
    }
}
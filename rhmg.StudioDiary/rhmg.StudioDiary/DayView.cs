using System.Collections.Generic;

namespace rhmg.StudioDiary
{
    public class DayView
    {
        public List<Booking> Bookings { get; set; }

        public DayView()
        {
            Bookings = new List<Booking>();
        }
    }
}
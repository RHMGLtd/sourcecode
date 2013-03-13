using System;
using System.Collections.Generic;

namespace rhmg.StudioDiary.InternalWeb.ViewModels
{
    public class DayOccupancyModel
    {
        public DayViewBookingLists CurrentBookings { get; set; }
        public List<Room> AllRooms { get; set; }
        public DateTime Date { get; set; }
    }
}
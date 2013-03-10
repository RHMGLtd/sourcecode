using System;
using System.Collections.Generic;

namespace rhmg.StudioDiary.InternalWeb.ViewModels
{
    public class BaseFormBookingModel
    {
        public string ProductFriendlyName { get; set; }
        public string BookingHint { get; set; }

        public DateTime Date { get; set; }
        public DayViewBookingLists CurrentBookings { get; set; }
        public List<Room> Rooms { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Notes { get; set; }
    }
}
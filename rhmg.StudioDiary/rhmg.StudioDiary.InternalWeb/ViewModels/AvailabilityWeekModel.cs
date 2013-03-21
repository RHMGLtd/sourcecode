using System;
using System.Collections.Generic;

namespace rhmg.StudioDiary.InternalWeb.ViewModels
{
    public class AvailabilityWeekModel
    {
        public DateTime PreviousWeekMonday { get; set; }
        public DateTime NextWeekMonday { get; set; }
        public WeekToAView ThisWeek { get; set; }
        public List<Room> Rooms { get; set; }
        public List<Product> Products { get; set; }
    }
}
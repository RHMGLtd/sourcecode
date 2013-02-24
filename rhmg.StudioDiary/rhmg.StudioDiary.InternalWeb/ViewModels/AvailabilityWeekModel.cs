using System.Collections.Generic;

namespace rhmg.StudioDiary.InternalWeb.ViewModels
{
    public class AvailabilityWeekModel
    {
        public WeekToAView ThisWeek { get; set; }
        public List<Room> Rooms { get; set; }
    }
}
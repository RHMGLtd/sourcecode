using System.Collections.Generic;

namespace rhmg.StudioDiary.InternalWeb.ViewModels
{
    public class AvailabilityDayModel
    {
        public DayViewBookingLists ThisDay { get; set; }
        public List<Room> AllRooms { get; set; }
    }
}
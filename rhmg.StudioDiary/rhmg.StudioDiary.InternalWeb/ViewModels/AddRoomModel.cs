using System.Collections.Generic;

namespace rhmg.StudioDiary.InternalWeb.ViewModels
{
    public class AddRoomModel
    {
        public string Name { get; set; }
        public List<string> RateIds { get; set; }

        public List<Room> CurrentRooms { get; set; } 
        public List<Rate> AvailableRates { get; set; } 
    }

    public class EditRoomModel : AddRoomModel
    {
        public string RoomId { get; set; }
    }
}
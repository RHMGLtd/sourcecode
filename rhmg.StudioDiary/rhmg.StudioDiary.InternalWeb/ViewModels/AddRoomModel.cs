using System.Collections.Generic;

namespace rhmg.StudioDiary.InternalWeb.ViewModels
{
    public class AddRoomModel
    {
        public AddRoomModel()
        {
            RateIds = new List<string>();
        }
        public string Name { get; set; }
        public List<string> RateIds { get; set; }
        public int DisplayOrder { get; set; }

        public List<Room> CurrentRooms { get; set; } 
        public List<Rate> AvailableRates { get; set; } 
    }
}
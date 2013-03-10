using System.Collections.Generic;

namespace rhmg.StudioDiary.InternalWeb.ViewModels
{
    public class AdminIndexModel
    {
        public List<Room> Rooms { get; set; }
        public List<Rate> Rates { get; set; }
        public List<AdditionalEquipment> AdditionalEquipment { get; set; }
        public List<Product> Products { get; set; } 
    }
}
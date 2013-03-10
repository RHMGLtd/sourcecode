using System.Collections.Generic;

namespace rhmg.StudioDiary.InternalWeb.ViewModels
{
    public class AddProductModel
    {
        public AddProductModel()
        {
            RoomIds = new List<string>();
        }
        public string Name { get; set; }
        public int Type { get; set; }
        public int SelectedForm { get; set; }
        public List<string> RoomIds { get; set; }
        public int DisplayOrder { get; set; }
        public string BookingHint { get; set; }

        public List<Product> CurrentProducts { get; set; }
        public List<Room> AvailableRooms { get; set; } 
    }
    public class EditProductModel : AddProductModel
    {
        public string ProductId { get; set; }
    }
}
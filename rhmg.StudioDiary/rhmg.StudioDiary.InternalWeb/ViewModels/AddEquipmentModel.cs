using System.Collections.Generic;

namespace rhmg.StudioDiary.InternalWeb.ViewModels
{
    public class AddEquipmentModel
    {
        public double UnitCost { get; set; }
        public string Description { get; set; }

        public List<AdditionalEquipment> CurrentEquipment { get; set; } 
    }

    public class EditEquipmentModel : AddEquipmentModel
    {
        public string EquipmentId { get; set; }
    }
}
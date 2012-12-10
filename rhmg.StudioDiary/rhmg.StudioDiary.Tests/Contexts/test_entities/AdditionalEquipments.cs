namespace rhmg.StudioDiary.Tests.Contexts.test_entities
{
    public class AdditionalEquipments
    {
        public static AdditionalEquipment amplifier
        {
            get
            {
                return new AdditionalEquipment
                {
                    Description = "Guitar Amplifier",
                    UnitCost = 5.00
                };
            }
        }
        public static AdditionalEquipment cymbalSet
        {
            get
            {
                return new AdditionalEquipment
                {
                    Description = "Cymbal Set",
                    UnitCost = 15.00
                };
            }
        } 
    }
}
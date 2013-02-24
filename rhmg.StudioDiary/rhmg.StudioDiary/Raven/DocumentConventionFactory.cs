using Raven.Client.Document;

namespace rhmg.StudioDiary.Raven
{
    public class DocumentConventionFactory
    {
        public static DocumentConvention getConvention()
        {
            return new DocumentConvention()
                       {
                           FindTypeTagName = type =>
                           {
                               if (typeof(Booking).IsAssignableFrom(type))
                                   return "booking";
                               if (typeof(BackFill).IsAssignableFrom(type))
                                   return "backfill";
                               if (typeof(Contact).IsAssignableFrom(type))
                                   return "contact";
                               if (typeof(AdditionalEquipment).IsAssignableFrom(type))
                                   return "additionalequipment";
                               return null;
                           }
                       };
        }
    }
}
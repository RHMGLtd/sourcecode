using Raven.Client.Document;

namespace rhmg.StudioDiary
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
                               if (typeof(Contact).IsAssignableFrom(type))
                                   return "contact";
                               return null;
                           }
                       };
        }
    }
}
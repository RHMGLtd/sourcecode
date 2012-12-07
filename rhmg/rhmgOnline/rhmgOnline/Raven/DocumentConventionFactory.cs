using Raven.Client.Document;
using rhmgOnline.Models;

namespace rhmgOnline.Raven
{
    public class DocumentConventionFactory
    {
        public static DocumentConvention getConvention()
        {
            return new DocumentConvention
                       {
                           FindTypeTagName = type =>
                               {
                                   if (typeof (Brand).IsAssignableFrom(type))
                                       return "brand";
                                   return null;

                               }
                       };
        }
    }
}
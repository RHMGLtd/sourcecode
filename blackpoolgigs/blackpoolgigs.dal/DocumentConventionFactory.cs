using blackpoolgigs.common.Models;
using Raven.Client.Document;

namespace blackpoolgigs.ravendal
{
    public class DocumentConventionFactory
    {
        public static DocumentConvention getConvention()
        {
            return new DocumentConvention()
            {
                FindTypeTagName = type =>
                {
                    if (typeof(Gig).IsAssignableFrom(type))
                        return "gig";
                    if (typeof(RepeatingGig).IsAssignableFrom(type))
                        return "repeatinggig";
                    if (typeof(VenueMetadata).IsAssignableFrom(type))
                        return "metadata";
                    if (typeof(StolenGear).IsAssignableFrom(type))
                        return "stolengear";
                    if (typeof(BandMetadata).IsAssignableFrom(type))
                        return "band";
                    return null;
                }
            };
        }
    }
}
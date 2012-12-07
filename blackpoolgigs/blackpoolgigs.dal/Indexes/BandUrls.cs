using System.Linq;
using blackpoolgigs.common.Models;
using Raven.Client.Indexes;

namespace blackpoolgigs.ravendal.Indexes
{
    public class BandUrls : AbstractIndexCreationTask<BandMetadata, common.Models.BandUrls>
    {
        public BandUrls()
        {
            Map = docs => from doc in docs
                          select new
                                     {
                                         doc.Id,
                                         doc.MySpaceLink,
                                         doc.FacebookLink,
                                         doc.OtherLinks
                                     };
            Reduce = results => from result in results
                                group result by new
                                                    {
                                                        result.Id,
                                                        result.MySpaceLink,
                                                        result.FacebookLink,
                                                        result.OtherLinks
                                                    }
                                    into g
                                    select new
                                               {
                                                   g.Key.Id,
                                                   g.Key.MySpaceLink,
                                                   g.Key.FacebookLink,
                                                   g.Key.OtherLinks
                                               };
        }
    }
}
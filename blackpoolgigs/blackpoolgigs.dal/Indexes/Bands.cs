using System.Linq;
using blackpoolgigs.common.Models;
using Raven.Client.Indexes;

namespace blackpoolgigs.ravendal.Indexes
{
    public class Bands : AbstractIndexCreationTask<Gig, Bands.ReduceResult>
    {
        public class ReduceResult
        {
            public BandName[] BandNames { get; set; }
        }
        public Bands()
        {
            Map = docs => from doc in docs
                          select new
                                     {
                                         doc.BandNames
                                     };
            Reduce = results => from result in results
                                group result by new
                                                    {
                                                        result.BandNames
                                                    }
                                into g
                                select new
                                           {
                                               g.Key.BandNames
                                           };
        }
    }
}
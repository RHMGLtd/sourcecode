using System.Linq;
using blackpoolgigs.common.Models;
using Raven.Client.Indexes;

namespace blackpoolgigs.ravendal.Indexes
{
    public class Venues : AbstractIndexCreationTask<Gig, Venues.ReduceResult>
    {
        public class ReduceResult
        {
            public string Venue { get; set; }
        }
        public Venues()
        {
            Map = docs => from doc in docs
                          select new
                                     {
                                         doc.Venue
                                     };
            Reduce = results => from result in results
                                group result by new
                                                    {
                                                        result.Venue
                                                    }
                                into g
                                select new
                                           {
                                               g.Key.Venue
                                           };
        }
    }
}
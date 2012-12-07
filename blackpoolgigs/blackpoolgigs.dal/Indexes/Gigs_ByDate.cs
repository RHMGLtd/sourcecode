using System.Linq;
using blackpoolgigs.common.Models;
using Raven.Client.Indexes;

namespace blackpoolgigs.ravendal.Indexes
{
    public class Gigs_ByDate : AbstractIndexCreationTask<Gig, Gigs_ByDate.ReduceResult>
    {
        public class ReduceResult
        {
            public string[] BandNames { get; set; }
            public string Venue { get; set; }
            public Time StartTime { get; set; }
        }
        public Gigs_ByDate()
        {
            Map = docs => from doc in docs
                          select new
                                     {
                                         doc.Id,
                                         doc.BandNames,
                                         doc.Venue,
                                         doc.StartTime
                                     };
        }
    }
}
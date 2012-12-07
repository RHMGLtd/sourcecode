using System;
using System.Linq;
using blackpoolgigs.common.Models;
using Raven.Client.Indexes;

namespace blackpoolgigs.ravendal.Indexes
{
    public class GigCounts_EachDay : AbstractIndexCreationTask<Gig, GigCounts_EachDay.ReduceResult>
    {
        public class ReduceResult
        {
            public DateTime AsOf { get; set; }
            public int FutureGigs { get; set; }
        }
        public GigCounts_EachDay()
        {
            Map = docs => from doc in docs
                          select new
                                     {
                                         AsOf = doc.Date,
                                         FutureGigs = 1
                                     };
            Reduce = results => from result in results
                                group result by new
                                                    {
                                                        result.AsOf
                                                    }
                                into g
                                select new
                                           {
                                               g.Key.AsOf,
                                               FutureGigs = g.Sum(x => x.FutureGigs)
                                           };
        }
    }
}
using System.Linq;
using blackpoolgigs.common.Models;
using Raven.Client.Indexes;

namespace blackpoolgigs.ravendal.Indexes
{
    public class Months : AbstractIndexCreationTask<Gig, Months.ReduceResult>
    {
        public class ReduceResult
        {
            public int Month { get; set; }
            public int Year { get; set; }
        }
        public Months()
        {
                           Map = docs => from doc in docs
                                         select new
                                                    {
                                                        doc.Date.Month,
                                                        doc.Date.Year
                                                    };
            Reduce = results => from result in results
                                group result by new
                                                    {
                                                        result.Month,
                                                        result.Year
                                                    }
                                into g
                                select new
                                           {
                                               g.Key.Month,
                                               g.Key.Year
                                           };
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace blackpoolgigs.common.Models
{
    public class CountCollection
    {
        public Counts[] Counts { get; set; }
        int total { get; set; }
        public CountCollection()
        {
            Counts = new Counts[]{};
        }
        public Counts ForDate(DateTime date)
        {
            if (date < Counts.First().AsOf)
                return new Counts
                           {
                               AsOf = date,
                               FutureGigs = total,
                               TotalGigs = total
                           };
            if (date > Counts.Last().AsOf)
                return new Counts
                           {
                               AsOf = date,
                               FutureGigs = 0,
                               TotalGigs = total
                           };
            return Counts.Where(x => x.AsOf >= date).FirstOrDefault();
        }
        public CountCollection Do(IEnumerable<Counts> input, int totalCount, int numberOfBands, int numberOfVenues)
        {
            total = totalCount;
            input = input.OrderBy(x => x.AsOf);
            var internals = new List<Counts>();
            for (var date = input.First().AsOf; date <= input.Last().AsOf; date = date.AddDays(1))
            {
                internals.Add(new Counts
                                  {
                                      AsOf = date,
                                      FutureGigs = input.Where(x => x.AsOf >= date).Sum(x => x.FutureGigs),
                                      TotalGigs = total,
                                      Bands = numberOfBands,
                                      Venues = numberOfVenues,
                                      AsOfString = date.ToString("dd MMM yyyy")
                                  });
            }
            Counts = internals.ToArray();
            return this;
        }
    }
}
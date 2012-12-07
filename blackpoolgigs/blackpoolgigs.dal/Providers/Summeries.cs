using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using blackpoolgigs.common.Extensions;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using blackpoolgigs.ravendal.Indexes;
using MoreLinq;
using Raven.Client;

namespace blackpoolgigs.ravendal.Providers
{
    public class Summeries : ISummariseVenues, ISummariseGigs, IDoOtherThingsWithGigs, ISummariseBands
    {
        protected readonly IDocumentSession session;
        protected readonly IHandleRepeatingGigs repeaters;

        public Summeries(IDocumentSession session, IHandleRepeatingGigs repeaters)
        {
            this.session = session;
            this.repeaters = repeaters;
        }
        public IEnumerable<Gig> AllGigs()
        {
            var gigs = new List<Gig>();
            var pageNumber = 1;
            while (true)
            {
                var bob = session.Advanced.LuceneQuery<Gig>()
                    .Skip(((pageNumber - 1) * 1000))
                    .Take(1000);
                gigs.AddRange(bob.ToList());
                if (bob.QueryResult.TotalResults <= pageNumber * 1000)
                    break;
                pageNumber++;
            }
            return repeaters.AddGigsTo(gigs);
        }

        public IEnumerable<BandDiary> BandDiaries()
        {
            var gigs = AllGigs();
            var bandNames = BandNames();
            return bandNames.Select(name => new BandDiary
            {
                BandName = name,
                Gigs = gigs.Where(x => x.BandNames.Select(y => y.Value).Contains(name.Value)).OrderBy(b => b.Date)
            }).ToList(); ;
        }

        public MonthlyGigCounts MonthlyCounts()
        {
            var gigs = AllGigs();
            return new MonthlyGigCounts
                       {
                           Counts = gigs
                                .GroupBy(g => new { g.Date.Month, g.Date.Year })
                                .Select(x => new MonthCount
                                                 {
                                                     Month = new DateTimeFormatInfo().GetMonthName(x.Key.Month),
                                                     Year = x.Key.Year,
                                                     Count = gigs.Where(q => q.Date.Month == x.Key.Month && q.Date.Year == x.Key.Year).Count()
                                                 }).ToArray()
                       };
        }

        public CountCollection GetCountCollection()
        {
            var gigs = AllGigs();
            var counts = gigs
                .GroupBy(g => new
                                  {
                                      AsOf = g.Date
                                  })
                .Select(x => new Counts
                                 {
                                     AsOf = x.Key.AsOf,
                                     FutureGigs = gigs.Where(q => q.Date.Date == x.Key.AsOf.Date).Count()
                                 });
            return new CountCollection().Do(counts, TotalCount(), NumberOfBands(), NumberOfVenues());
        }

        public IEnumerable<Diary> GigDiaries()
        {
            var dates = new List<Months.ReduceResult>();
            var pageNumber = 1;
            while (true)
            {
                var bob = session.Advanced.LuceneQuery<Months.ReduceResult>("Months")
                    .Skip(((pageNumber - 1) * 1000))
                    .Take(1000);
                dates.AddRange(bob.ToList());
                if (bob.QueryResult.TotalResults <= pageNumber * 1000)
                    break;
                pageNumber++;
            }
            var gigs = AllGigs();
            var result = new List<Diary>();
            foreach (var diary in dates.Select(date => new Diary(new DateTime(date.Year, date.Month, 1))))
            {
                diary.AddGigs(gigs.Where(x => x.Date.Month == diary.FirstDayOfMonth.Month && x.Date.Year == diary.FirstDayOfMonth.Year).ToList(), false);
                result.Add(diary);
            }
            return result;
        }

        public IEnumerable<VenueDiary> VenueDiaries()
        {
            var venues = new List<Venues.ReduceResult>();
            var pageNumber = 1;
            while (true)
            {
                var bob = session.Advanced.LuceneQuery<Venues.ReduceResult>("Venues")
                    .Skip(((pageNumber - 1) * 1000))
                    .Take(1000);
                venues.AddRange(bob.ToList());
                if (bob.QueryResult.TotalResults <= pageNumber * 1000)
                    break;
                pageNumber++;
            }
            var gigs = AllGigs();
            return venues.Select(venue => new VenueDiary
            {
                Venue = venue.Venue,
                Gigs = gigs.Where(x => x.Venue == venue.Venue).ToList()
            }).ToList().SortGigsInVenues();
        }

        public IEnumerable<VenueMetadata> Metadata()
        {
            var metadata = new List<VenueMetadata>();
            var pageNumber = 1;
            while (true)
            {
                var bob = session.Advanced.LuceneQuery<VenueMetadata>("Venues/Metadata")
                    .Skip(((pageNumber - 1) * 1000))
                    .Take(1000);
                metadata.AddRange(bob.ToList());
                if (bob.QueryResult.TotalResults <= pageNumber * 1000)
                    break;
                pageNumber++;
            }
            return metadata;
        }

        private int TotalCount()
        {
            return AllGigs().Count();
        }

        public IEnumerable<Gig> RecentlyUpdated()
        {
            return session.Advanced.LuceneQuery<Gig>()
                .AddOrder("Edited", true)
                .Take(5)
                .ToList();
        }
        IEnumerable<BandName> BandNames()
        {
            var b = new List<Indexes.Bands.ReduceResult>();
            var pageNumber = 1;
            while (true)
            {
                var bob = session.Advanced.LuceneQuery<Indexes.Bands.ReduceResult>("Bands")
                    .Skip(((pageNumber - 1) * 1000))
                    .Take(1000);
                b.AddRange(bob.ToList());
                if (bob.QueryResult.TotalResults <= pageNumber * 1000)
                    break;
                pageNumber++;
            }

            var bandNames = b.Select(x => x.BandNames).ToList();
            var result = new List<BandName>();
            foreach (var bandName in bandNames)
            {
                if (bandName == null)
                    continue;
                foreach (var name in bandName)
                {
                    if (!result.Where(x => x.Value == name.Value).Any())
                        result.Add(name);
                }
            }
            return result;
        }
        public int NumberOfBands()
        {
            return BandNames().Count();
        }

        public IEnumerable<BandMetadata> BandMetadata()
        {
            var metadata = new List<BandMetadata>();
            var pageNumber = 1;
            while (true)
            {
                var bob = session.Advanced.LuceneQuery<BandMetadata>()
                    .Skip(((pageNumber - 1) * 1000))
                    .Take(1000);
                metadata.AddRange(bob.ToList());
                if (bob.QueryResult.TotalResults <= pageNumber * 1000)
                    break;
                pageNumber++;
            }
            return metadata;
        }

        int NumberOfVenues()
        {
            return AllGigs()
                .Select(x => x.Venue).DistinctBy(x => x).Count();
        }
    }
}
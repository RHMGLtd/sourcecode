using System;
using System.Collections.Generic;
using System.Linq;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using coolbunny.common.Extensions;
using coolbunny.common.Helpers;
using Raven.Client;

namespace blackpoolgigs.ravendal.Providers
{
    public class GigProvider : IStoreGigs, IGiveYouGigs, IOneOffAdminStuff
    {
        protected readonly IDocumentSession session;
        protected readonly IHandleRepeatingGigs repeaters;
        protected readonly ISummariseGigs gigSummary;

        public GigProvider(IDocumentSession session, IHandleRepeatingGigs repeaters, ISummariseGigs gigSummary)
        {
            this.session = session;
            this.gigSummary = gigSummary;
            this.repeaters = repeaters;
        }

        public Gig Save(Gig gig)
        {
            session.Store(gig);
            session.SaveChanges();
            return gig;
        }

        public Gig Delete(string id)
        {
            var gig = session.Load<Gig>(id);
            if (gig != null)
                session.Delete(gig);
            session.SaveChanges();
            return gig;
        }
        public Diary Get(DateTime date)
        {
            var gigs = new List<Gig>();
            var pageNumber = 1;
            while (true)
            {
                var bob = session.Advanced.LuceneQuery<Gig>()
                    .WhereEquals("Date.Month", date.Month).AndAlso()
                    .WhereEquals("Date.Year", date.Year)
                    .Skip(((pageNumber - 1) * 128))
                    .Take(128);
                gigs.AddRange(bob.ToList());
                if (bob.QueryResult.TotalResults <= pageNumber * 128)
                    break;
                pageNumber++;
            }

            return new Diary(date).AddGigs(gigs, false);
        }

        public PageOfResults<Gig> Get(PagingParams paging)
        {
            var result = session.Advanced.LuceneQuery<Gig>()
                .AddOrder("Date", paging.SortOrder == "desc")
                .Skip(((paging.PageNumber - 1) * paging.PageSize))
                .Take(paging.PageSize);
            return new PageOfResults<Gig>
                       {
                           CurrentPageNumber = paging.PageNumber,
                           NumberOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(result.QueryResult.TotalResults) / Convert.ToDouble(paging.PageSize))),
                           TotalNumberOfRecords = result.QueryResult.TotalResults,
                           Results = result.ToList()
                       };
        }

        public Counts Counts(DateTime date)
        {
            return gigSummary.GetCountCollection().ForDate(date);
        }

        public Gig Get(string recordId)
        {
            return session.Load<Gig>(recordId);
        }

        public PageOfResults<Gig> Get(BandName band, PagingParams paging)
        {
            var result = gigSummary.BandDiaries().Where(x => x.BandName.Value.Tidy() == band.Value.Tidy()).FirstOrDefault().Gigs;
            return new PageOfResults<Gig>
            {
                CurrentPageNumber = paging.PageNumber,
                NumberOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(result.Count()) / Convert.ToDouble(paging.PageSize))),
                TotalNumberOfRecords = result.Count(),
                Results = result.Skip(((paging.PageNumber - 1) * paging.PageSize)).Take(paging.PageSize).ToList()
            };
        }

        public DiaryEntry GetEntry(DateTime date)
        {
            return Get(date).GetEntry(date);
        }

        public MonthlyGigCounts MonthlyCounts(int year)
        {
            return gigSummary.MonthlyCounts().ForYear(year);
        }

        public DiaryLine Week(DateTime date)
        {
            throw new NotImplementedException();
        }

        public int AddCreatedDates()
        {
            var gigs = session.Advanced.LuceneQuery<Gig>().Take(2000).ToList();
            foreach (var gig in gigs)
            {
                gig.Created = new DateTime(2011, 04, 23).AddDays(0.RandomFromTo(10));
                gig.Edited = gig.Created;
                session.Store(gig);
            }
            session.SaveChanges();
            return gigs.Count();
        }

        public void DataCleanseSpaces()
        {
            var gigs = session.Advanced.LuceneQuery<Gig>().WaitForNonStaleResults().Take(2000).ToList();
            foreach (var gig in gigs)
            {
                foreach (var t in gig.BandNames)
                {
                    t.Value = t.Value.SafeTrim();
                }
                gig.Venue = gig.Venue.SafeTrim();
                gig.Price = gig.Price.SafeTrim();
                gig.StartTime = gig.StartTime.SafeTrim();
                session.Store(gig);
            }
            session.SaveChanges();
        }
    }
}
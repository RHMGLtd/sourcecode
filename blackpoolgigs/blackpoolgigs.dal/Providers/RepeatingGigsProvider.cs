using System;
using System.Collections.Generic;
using System.Linq;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using coolbunny.common.Helpers;
using Raven.Client;

namespace blackpoolgigs.ravendal.Providers
{
    public class RepeatingGigsProvider : IHandleRepeatingGigs
    {
        protected readonly IDocumentSession session;

        public RepeatingGigsProvider(IDocumentSession session)
        {
            this.session = session;
        }

        public RepeatingGig SaveRepeating(RepeatingGig gig)
        {
            session.Store(gig);
            session.SaveChanges();
            return gig;
        }

        public RepeatingGig GetRepeating(string id)
        {
            return session.Load<RepeatingGig>(id);
        }

        public PageOfResults<RepeatingGig> GetRepeating(PagingParams paging)
        {
            var result = session.Advanced.LuceneQuery<RepeatingGig>()
                .AddOrder("SequenceStartFrom", paging.SortOrder == "desc")
                .Skip(((paging.PageNumber - 1) * paging.PageSize))
                .Take(paging.PageSize);
            return new PageOfResults<RepeatingGig>
            {
                CurrentPageNumber = paging.PageNumber,
                NumberOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(result.QueryResult.TotalResults) / Convert.ToDouble(paging.PageSize))),
                TotalNumberOfRecords = result.QueryResult.TotalResults,
                Results = result.ToList()
            };
        }

        RepeatingGig IHandleRepeatingGigs.Delete(string id)
        {
            var gig = session.Load<RepeatingGig>(id);
            if (gig != null)
                session.Delete(gig);
            session.SaveChanges();
            return gig;
        }

        public List<Gig> AddGigsTo(List<Gig> addTo)
        {
            var repeatingGigs = Gigs();
            foreach (var gig in
                repeatingGigs.Where(gig => !addTo.Where(x => x.Date.Date == gig.Date.Date && x.Venue == gig.Venue).Any()))
            {
                addTo.Add(gig);
            }
            return addTo;
        }

        IEnumerable<Gig> Gigs()
        {
            // now extract repeating gigs
            var repeaters = new List<RepeatingGig>();
            var pageNumber = 1;
            while (true)
            {
                var bob = session.Advanced.LuceneQuery<RepeatingGig>()
                    .Skip(((pageNumber - 1) * 2000))
                    .Take(128);
                repeaters.AddRange(bob.ToList());
                if (bob.QueryResult.TotalResults <= pageNumber * 2000)
                    break;
                pageNumber++;
            }
            var gigs = new List<Gig>();
            foreach (var repeatingGig in repeaters)
                gigs.AddRange(repeatingGig.AsGigs());
            return gigs;
        }
    }
}
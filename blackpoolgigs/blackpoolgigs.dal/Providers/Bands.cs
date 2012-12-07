using System;
using System.Collections.Generic;
using System.Linq;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using coolbunny.common.Helpers;
using MoreLinq;
using Raven.Client;

namespace blackpoolgigs.ravendal.Providers
{
    public class Bands : IWillProvideBands, ISaveBands
    {
        protected readonly IDocumentSession session;
        protected readonly ISummariseGigs gigs;
        public Bands(IDocumentSession session, ISummariseGigs gigs)
        {
            this.session = session;
            this.gigs = gigs;
        }

        public IEnumerable<BandName> BandNames()
        {
            var bandNames = gigs.AllGigs()
                .Select(x => x.BandNames).ToList();
            return (from bandName in bandNames from name in bandName select name).DistinctBy(x => x.Value).OrderBy(x => x.Value).ToList();
        }

        public PageOfResults<BandName> GetBands(PagingParams paging, params string[] alphaPick)
        {
            var result = BandNames();
            var pageOf = result.Skip(((paging.PageNumber - 1) * paging.PageSize))
                .Take(paging.PageSize);
            return new PageOfResults<BandName>
            {
                CurrentPageNumber = paging.PageNumber,
                NumberOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(result.Count()) / Convert.ToDouble(paging.PageSize))),
                TotalNumberOfRecords = result.Count(),
                Results = pageOf.ToList()
            };
        }

        public BandDiary GetDiary(BandName name)
        {
            throw new NotImplementedException();
        }

        public BandMetadata GetMetadata(BandName name)
        {
            return session.Advanced.LuceneQuery<BandMetadata>()
                       .WhereEquals("BandName", name.Value)
                       .FirstOrDefault() ?? new BandMetadata {BandName = name.Value};
        }

        public PageOfResults<BandUrls> GetBandUrls(PagingParams paging)
        {
            var pageOf = session.Advanced.LuceneQuery<BandUrls>("BandUrls").Skip(((paging.PageNumber - 1) * paging.PageSize))
                .Take(paging.PageSize);
            return new PageOfResults<BandUrls>
            {
                CurrentPageNumber = paging.PageNumber,
                NumberOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(pageOf.QueryResult.TotalResults) / Convert.ToDouble(paging.PageSize))),
                TotalNumberOfRecords = pageOf.QueryResult.TotalResults,
                Results = pageOf.ToList()
            };
        }

        public BandMetadata Save(BandMetadata band)
        {
            session.Store(band);
            session.SaveChanges();
            return band;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using blackpoolgigs.common.Contracts;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using blackpoolgigs.ravendal.Indexes;
using coolbunny.common.Extensions;
using coolbunny.common.Helpers;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Json.Linq;

namespace blackpoolgigs.ravendal.Providers
{
    public class VenueProvider : IGiveYouVenues,  IStoreMetadata, IDealWithRavenAttachments
    {
        protected readonly IDocumentSession session;
        private readonly IDocumentStore documentStore;
        protected readonly IHandleRepeatingGigs repeaters;
        protected readonly ISummariseVenues venueSummary;
        protected readonly ISummariseGigs gigSummary;

        public VenueProvider(IDocumentSession session, IDocumentStore documentStore, IHandleRepeatingGigs repeaters, ISummariseVenues venueSummary, ISummariseGigs gigSummary)
        {
            this.session = session;
            this.gigSummary = gigSummary;
            this.venueSummary = venueSummary;
            this.repeaters = repeaters;
            this.documentStore = documentStore;
        }

        public VenueDiary Get(VenueName venueName)
        {
            var gigs = gigSummary.AllGigs()
                .Where(x => x.Venue == venueName.Value)
                .OrderBy(x => x.Date)
                .ToList();
            return new VenueDiary
            {
                Gigs = gigs,
                Venue = venueName.Value
            };
        }

        public PageOfResults<VenueDiaryPlusMetaData> DiaryPlusMetadata(PagingParams paging)
        {
            var diaries = Get(paging);
            var metadata = venueSummary.Metadata();
            var result = diaries.Results.Select(diary => new VenueDiaryPlusMetaData
                                                             {
                                                                 Diary = diary,
                                                                 Metadata = metadata
                                                                        .Where(x => x.VenueName == diary.Venue)
                                                                        .FirstOrDefault() ?? new VenueMetadata { VenueName = diary.Venue }
                                                             }).ToList();
            return new PageOfResults<VenueDiaryPlusMetaData>
                       {
                           CurrentPageNumber = paging.PageNumber,
                           NumberOfPages = diaries.NumberOfPages,
                           TotalNumberOfRecords = diaries.TotalNumberOfRecords,
                           Results = result
                       };
        }
        public PageOfResults<VenueDiary> Get(PagingParams paging)
        {
            var gigs = gigSummary.AllGigs();
            var result = session.Advanced.LuceneQuery<Venues.ReduceResult>("Venues")
                .OrderBy("Venue")
                .Skip(((paging.PageNumber - 1) * paging.PageSize))
                .Take(paging.PageSize);
            return new PageOfResults<VenueDiary>
            {
                CurrentPageNumber = paging.PageNumber,
                NumberOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(result.QueryResult.TotalResults) / Convert.ToDouble(paging.PageSize))),
                TotalNumberOfRecords = result.QueryResult.TotalResults,
                Results = result.Select(x => new VenueDiary { Venue = x.Venue, Gigs = gigs.Where(y => y.Venue == x.Venue).ToList() }).ToList()
            };
        }

        public VenueMetadata GetMetadata(VenueName venueName)
        {
            return session.Advanced.LuceneQuery<VenueMetadata>("Venues/Metadata")
                .WhereEquals("VenueName", venueName.Value)
                .FirstOrDefault() ?? new VenueMetadata
                                         {
                                             VenueName = venueName.Value
                                         };
        }

        public IEnumerable<VenueMetadata> Venues()
        {
            throw new NotImplementedException();
        }

        public VenueMetadata Store(VenueMetadata metadata, HttpPostedFileBase mainImage)
        {
            session.Store(metadata);
            if (mainImage != null)
            {
                documentStore.DatabaseCommands.PutAttachment(metadata.Id + "/mainImage", null, mainImage.InputStream.ReadFully(),
                    RavenJObject.FromObject(
                    new
                    {
                        Filename = metadata.MainImageName
                    }
                    ));
            }
            session.SaveChanges();
            return metadata;
        }

        public Attachment MainImage(string id)
        {
            return documentStore.DatabaseCommands.GetAttachment(id.EnsureStartsWith("metadata/") + "/mainImage");
        }

        public bool Delete(string id)
        {
            var metadata = session.Load<VenueMetadata>(id);
            if (metadata != null)
                session.Delete(metadata);
            session.SaveChanges();
            return true;
        }
    }
}
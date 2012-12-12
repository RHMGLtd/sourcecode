using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using blackpoolgigs.common.Contracts;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using coolbunny.common.Helpers;
using OpenFileSystem.IO;

namespace blackpoolgigs.filedal.Providers
{
    public class VenueProvider : IGiveYouVenues
    {
        protected readonly IAccessTheFileSystem cache;
        protected readonly IFileSystem files;

        public VenueProvider(IAccessTheFileSystem cache, IFileSystem files)
        {
            this.cache = cache;
            this.files = files;
        }

        public PageOfResults<VenueDiary> Get(PagingParams paging)
        {
            var venues = files.GetDirectory(HostingEnvironment.MapPath("~/Content/Export/Venues/")).Files();
            var filtered = venues.OrderBy(x => x.NameWithoutExtension).Select(x => x.NameWithoutExtension)
                .Skip(((paging.PageNumber - 1) * paging.PageSize))
                .Take(paging.PageSize);
            return new PageOfResults<VenueDiary>
            {
                CurrentPageNumber = paging.PageNumber,
                NumberOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(venues.Count()) / Convert.ToDouble(paging.PageSize))),
                TotalNumberOfRecords = venues.Count(),
                Results = filtered.Select(x => Get(new VenueName { Value = x })).ToList()
            };
        }

        public PageOfResults<VenueDiaryPlusMetaData> DiaryPlusMetadata(PagingParams paging)
        {
            throw new NotImplementedException();
        }

        public VenueDiary Get(VenueName venueName)
        {
            var dir = files.GetDirectory(HostingEnvironment.MapPath("~/Content/Export/Venues/"));
            var result = new SmartMatch<VenueDiary>().Do(dir, venueName);
            return result.ExactMatchFound ? result.Results.First() : new EmptyVenueDiary { Venue = venueName.Value };
        }

        public VenueMetadata GetMetadata(VenueName venueName)
        {
            var dir = files.GetDirectory(HostingEnvironment.MapPath("~/Content/Export/Venues/Metadata"));
            var result = new SmartMatch<VenueMetadata>().Do(dir, venueName);
            return result.ExactMatchFound ? result.Results.First() : new VenueMetadata { VenueName = venueName.Value };
        }

        public IEnumerable<VenueMetadata> Venues()
        {
            var metadata = files.GetDirectory(HostingEnvironment.MapPath("~/Content/Export/Venues/Metadata")).Files();
            return metadata.Select(result => cache.ReadOut<VenueMetadata>("Venues/Metadata/" + result.Name));
        }
    }
}
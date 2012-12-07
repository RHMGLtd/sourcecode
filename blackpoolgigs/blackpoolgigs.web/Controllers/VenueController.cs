using System.Linq;
using blackpoolgigs.common.Contracts;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.web.Models;
using blackpoolgigs.web.Models.ViewModels;
using blackpoolgigs.web.Services.Interfaces;
using blackpoolgigs.web.Urls;
using coolbunny.common.Extensions;
using coolbunny.common.Helpers;
using Snooze;

namespace blackpoolgigs.web.Controllers
{
    public class VenueController : ResourceController
    {
        protected readonly IGiveYouVenues venues;
        protected readonly IProvideBlobs blobs;

        public VenueController(IGiveYouVenues venues, IProvideBlobs blobs)
        {
            this.venues = venues;
            this.blobs = blobs;
        }
        public ResourceResult Get(VenueMapUrl url)
        {
            var coords = venues.GetMetadata(new VenueName { Value = url.Venue.Tidy() }).MapCoords;
            if (coords.IsEmpty())
                return NotFound(new VenueMapNotFoundViewModel(blobs.GiveMeOne(), FourOhFourReason.NoMapForThisVenue) { VenueName = url.Venue }).AsHtml();
            return OK(new VenueMapViewModel
            {
                VenueName = url.Venue,
                MapCoords = coords
            }).AsHtml();
        }
        public ResourceResult Get(VenueUrl url)
        {
            var diary = venues.Get(new VenueName { Value = url.Venue.Tidy() });
            if (diary.IsNull)
                return NotFound(new VenueNotFoundViewModel(blobs.GiveMeOne()) { VenueName = url.Venue }).AsHtml();
            diary.Gigs = diary.Gigs.OrderBy(x => x.Date).ToList();
            return OK(new VenueViewModel
                          {
                              Diary = diary,
                              Metadata = venues.GetMetadata(new VenueName { Value = url.Venue.Tidy() })
                          }).AsHtml();
        }
        public ResourceResult Get(VenuesUrl url)
        {
            return OK(new VenuesViewModel
                          {
                              Params = new PagingParams(url),
                              Venues = venues.Get(new PagingParams(url))
                          }).AsHtml();
        }
        public ResourceResult Get(VenuesMapUrl url)
        {
            return OK(new VenuesMapViewModel
                          {
                              Venues = venues.Venues()
                          }).AsHtml();
        }
        public ResourceResult Get(GigUrl url)
        {
            var diary = venues.Get(new VenueName { Value = url.Venue });
            if (diary.IsNull)
                return NotFound(new VenueNotFoundViewModel(blobs.GiveMeOne()) { VenueName = url.Venue.Tidy() }).AsHtml();
            var gig = diary.ForDate(url.AsDate());
            if (gig == null)
                return NotFound(new DiaryNotFoundViewModel(FourOhFourReason.NoGigOnThisDate, blobs.GiveMeOne())).AsHtml();
            return OK(new GigViewModel
                          {
                              Gig = gig,
                              Metadata = venues.GetMetadata(new VenueName { Value = url.Venue })
                          }).AsHtml();
        }
    }
}
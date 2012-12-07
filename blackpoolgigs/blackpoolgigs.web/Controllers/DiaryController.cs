using System;
using System.Collections.Generic;
using System.Linq;
using blackpoolgigs.common.Contracts;
using blackpoolgigs.common.Extensions;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.web.Models;
using blackpoolgigs.web.Models.ViewModels;
using blackpoolgigs.web.Services.Interfaces;
using blackpoolgigs.web.Urls;
using Snooze;

namespace blackpoolgigs.web.Controllers
{
    public class DiaryController : ResourceController
    {
        protected readonly IGiveYouGigs gigs;
        protected readonly IProvideBlobs blobs;
        protected readonly IGiveYouVenues venues;

        public DiaryController(IGiveYouGigs gigs, IProvideBlobs blobs, IGiveYouVenues venues)
        {
            this.gigs = gigs;
            this.venues = venues;
            this.blobs = blobs;
        }

        public ResourceResult Get(DiaryUrl url)
        {
            return Get(new MonthUrl(DateTime.Now)).AsHtml();
        }
        public ResourceResult Get(TodayUrl url)
        {
            return Get(new DayUrl(DateTime.Now)).AsHtml();
        }
        public ResourceResult Get(YearUrl url)
        {
            if (!url.IsValid())
                return NotFound(new DiaryNotFoundViewModel(FourOhFourReason.DateNotValid, blobs.GiveMeOne())).AsHtml();
            if (Int32.Parse(url.Year).YearTooFarInFuture())
                return NotFound(new DiaryNotFoundViewModel(FourOhFourReason.TooFarInTheFuture, blobs.GiveMeOne())).AsHtml();
            var counts = gigs.MonthlyCounts(Int32.Parse(url.Year));
            if (Int32.Parse(url.Year).YearIsArchived() &&
                !counts.Counts.Select(b => b.Count > 0).Any())
                return NotFound(new DiaryNotFoundViewModel(FourOhFourReason.HistoricalNoGigs, blobs.GiveMeOne())).AsHtml();
            return OK(new YearViewModel
                          {
                              Year = url.Year,
                              MonthlyCounts = counts
                          }).AsHtml();
        }
        public ResourceResult Get(MonthUrl url)
        {
            if (!url.IsValid())
                return NotFound(new DiaryNotFoundViewModel(FourOhFourReason.DateNotValid, blobs.GiveMeOne())).AsHtml();
            if (Int32.Parse(url.Year).YearTooFarInFuture())
                return NotFound(new DiaryNotFoundViewModel(FourOhFourReason.TooFarInTheFuture, blobs.GiveMeOne())).AsHtml();
            var gigList = gigs.Get(url.AsDate());
            if (Int32.Parse(url.Year).YearIsArchived() &&
                !gigList.HasGigs())
                return NotFound(new DiaryNotFoundViewModel(FourOhFourReason.HistoricalNoGigs, blobs.GiveMeOne())).AsHtml();
            return OK(new MonthViewModel
            {
                Month = url.Month,
                Year = url.Year,
                Diary = gigList
            }).AsHtml();
        }
        public ResourceResult Get(DayMapUrl url)
        {
            if (!url.IsValid())
                return NotFound(new DiaryNotFoundViewModel(FourOhFourReason.DateNotValid, blobs.GiveMeOne())).AsHtml();
            if (Int32.Parse(url.Year).YearTooFarInFuture())
                return NotFound(new DiaryNotFoundViewModel(FourOhFourReason.TooFarInTheFuture, blobs.GiveMeOne())).AsHtml();
            var gigList = gigs.GetEntry(url.AsDate());
            if (Int32.Parse(url.Year).YearIsArchived() &&
                !gigList.HasGigs())
                return NotFound(new DiaryNotFoundViewModel(FourOhFourReason.HistoricalNoGigs, blobs.GiveMeOne())).AsHtml();
            var result = gigList.Gigs.Select(gig => new GigSummaryForMap
                                                        {
                                                            Bands = gig.Summary, Venue = gig.Venue, MapCoords = venues.GetMetadata(new VenueName {Value = gig.Venue}).MapCoords
                                                        }).ToList();
            return OK(new DayMapViewModel
                          {
                              Year = url.Year,
                              Date = url.Date,
                              Month = url.Month,
                              Gigs = result,
                              ForDate = url.AsDate()
                          }).AsHtml();
        }
        public ResourceResult Get(DayUrl url)
        {
            if (!url.IsValid())
                return NotFound(new DiaryNotFoundViewModel(FourOhFourReason.DateNotValid, blobs.GiveMeOne())).AsHtml();
            if (Int32.Parse(url.Year).YearTooFarInFuture())
                return NotFound(new DiaryNotFoundViewModel(FourOhFourReason.TooFarInTheFuture, blobs.GiveMeOne())).AsHtml();
            var gigList = gigs.GetEntry(url.AsDate());
            if (Int32.Parse(url.Year).YearIsArchived() &&
                !gigList.HasGigs())
                return NotFound(new DiaryNotFoundViewModel(FourOhFourReason.HistoricalNoGigs, blobs.GiveMeOne())).AsHtml();
            return OK(new DayViewModel
            {
                Year = url.Year,
                Date = url.Date,
                Month = url.Month,
                Entry = gigList
            }).AsHtml();
        }
    }
}
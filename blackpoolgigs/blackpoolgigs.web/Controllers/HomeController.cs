using System;
using System.Globalization;
using System.Linq;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using blackpoolgigs.web.Models;
using blackpoolgigs.web.Models.ViewModels;
using blackpoolgigs.web.Urls;
using Snooze;

namespace blackpoolgigs.web.Controllers
{
    public class HomeController : ResourceController
    {
        protected readonly IGiveYouGigs gigs;
        protected readonly IDoOtherThingsWithGigs otherGigs;
        

        public HomeController(IGiveYouGigs gigs, IDoOtherThingsWithGigs otherGigs)
        {
            this.gigs = gigs;
            this.otherGigs = otherGigs;
        }

        public ResourceResult Get(HomeUrl url)
        {
            var counts = gigs.Counts(DateTime.Now.Date);
            return OK(new HomeViewModel
                          {
                              Month = new DateTimeFormatInfo().GetMonthName(DateTime.Now.Month),
                              TotalGigs = counts.TotalGigs,
                              FutureGigs = counts.FutureGigs,
                              BandCount = counts.Bands,
                              VenueCount = counts.Venues,
                              ThisWeek = gigs.Week(DateTime.Now.Date),
                              RecentlyUpdated = otherGigs.RecentlyUpdated().Select(x => new RecentlyChangedInfo(x)).ToArray()
                          }).AsHtml();
        }
    }
}
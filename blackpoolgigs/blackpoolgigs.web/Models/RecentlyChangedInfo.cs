using System;
using blackpoolgigs.common.Models;

namespace blackpoolgigs.web.Models
{
    public class RecentlyChangedInfo
    {
        public string Summary { get; set; }
        public string DaysSinceChange { get; set; }
        public string Venue { get; set; }
        public DateTime Date { get; set; }

        public RecentlyChangedInfo(Gig gig)
        {
            Summary = gig.Summary;
            DaysSinceChange = formatDate(gig.Edited);
            Venue = gig.Venue;
            Date = gig.Date;
        }

        string formatDate(DateTime changeDate)
        {
            if (changeDate.Date == DateTime.Now.Date)
                return "updated today";
            return string.Format("updated {0} days ago", DateTime.Now.Date.Subtract(changeDate.Date).Days);
        }
    }
}
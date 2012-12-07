using blackpoolgigs.common.Models;

namespace blackpoolgigs.web.Models.ViewModels
{
    public class HomeViewModel
    {
        public int TotalGigs { get; set; }
        public int FutureGigs { get; set; }
        public int VenueCount { get; set; }
        public int BandCount { get; set; }
        public string Month { get; set; }
        public DiaryLine ThisWeek { get; set; }
        public RecentlyChangedInfo[] RecentlyUpdated { get; set; }
    }
}
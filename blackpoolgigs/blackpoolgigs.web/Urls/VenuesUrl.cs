using coolbunny.common.Interfaces;
using Snooze;

namespace blackpoolgigs.web.Urls
{
    public class VenuesUrl : Url, IPage
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
    }
}
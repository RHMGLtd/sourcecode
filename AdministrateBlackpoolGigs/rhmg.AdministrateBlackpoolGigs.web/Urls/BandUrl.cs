using coolbunny.common.Extensions;
using coolbunny.common.Interfaces;
using Snooze;

namespace rhmg.AdministrateBlackpoolGigs.web.Urls
{
    public class BandUrl : Url, IPage
    {
        public BandUrl()
        {
            
        }

        public BandUrl(string name)
        {
            BandName = name.Tidy();
        }
        public string BandName { get; set; }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
    }
}
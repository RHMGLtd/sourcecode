using coolbunny.common.Interfaces;
using Snooze;

namespace blackpoolgigs.web.Urls
{
    public class BandsUrl : Url, IPage
    {
        public BandsUrl()
        {
            AlphaPick = "a";
        }

        public BandsUrl(string alphaPick)
        {
            AlphaPick = alphaPick;
        }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }

        public string AlphaPick { get; set; }
    }
}
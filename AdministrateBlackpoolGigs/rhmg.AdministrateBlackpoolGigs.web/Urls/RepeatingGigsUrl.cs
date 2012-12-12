using coolbunny.common.Interfaces;
using Snooze;

namespace rhmg.AdministrateBlackpoolGigs.web.Urls
{
    public class RepeatingGigsUrl : Url, IPage
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }   
    }
}
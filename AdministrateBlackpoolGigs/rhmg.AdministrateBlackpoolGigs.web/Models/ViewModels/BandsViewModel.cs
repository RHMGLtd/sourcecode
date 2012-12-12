using blackpoolgigs.common.Models;
using coolbunny.common.Helpers;

namespace rhmg.AdministrateBlackpoolGigs.web.Models.ViewModels
{
    public class BandsViewModel
    {
        public PageOfResults<BandName> Bands { get; set; }
        public PagingParams Params { get; set; }
    }
}
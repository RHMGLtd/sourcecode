using blackpoolgigs.common.Models;
using coolbunny.common.Helpers;

namespace rhmg.AdministrateBlackpoolGigs.web.Models.ViewModels
{
    public class VenuesViewModel
    {
        public PageOfResults<VenueDiaryPlusMetaData> Venues { get; set; }
        public PagingParams Params { get; set; }
    }
}
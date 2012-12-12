using System.Linq;
using blackpoolgigs.common.Models;
using coolbunny.common.Helpers;

namespace rhmg.AdministrateBlackpoolGigs.web.Models.ViewModels
{
    public class RepeatingGigsViewModel
    {
        public PageOfResults<RepeatingGig> Gigs { get; set; }
        public PagingParams Params { get; set; }
        public int count
        {
            get { return Gigs.Results.Count(); }
        }
    }
}
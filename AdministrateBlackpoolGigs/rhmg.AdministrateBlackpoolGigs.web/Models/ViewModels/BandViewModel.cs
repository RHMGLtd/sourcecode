using blackpoolgigs.common.Models;
using coolbunny.common.Helpers;

namespace rhmg.AdministrateBlackpoolGigs.web.Models.ViewModels
{
    public class BandViewModel
    {
        public string Id { get; set; }

        public string BandName { get; set; }
        public PageOfResults<Gig> Gigs { get; set; }
        public PagingParams Params { get; set; }

        public string MainContactName { get; set; }
        public string[] OtherContacts { get; set; }
        public string ContactEmail { get; set; }
        public string AverageAge { get; set; }
        public string Bio { get; set; }
        public string Style { get; set; }
        public int CoverPercentage { get; set; }
        public int OriginalPercentage { get; set; }
        public string MySpaceLink { get; set; }
        public string FacebookLink { get; set; }
        public string[] OtherLinks { get; set; }
        public string NumberOfBandMembers { get; set; }
        public string HomeTown { get; set; }

        public BandViewModel()
        {
            OtherContacts = new string[] { };
            OtherLinks = new string[] { };
        }
    }
}
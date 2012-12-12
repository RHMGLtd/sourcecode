using System.Collections.Generic;
using System.Web;
using blackpoolgigs.common.Contracts;
using blackpoolgigs.common.Models;

namespace rhmg.AdministrateBlackpoolGigs.web.Models.ViewModels
{
    public class VenueViewModel
    {
        public string Id { get; set; }
        public VenueName Name { get; set; }
        public string MainImageName { get; set; }
        public HttpPostedFileBase MainImage { get; set; }
        public string BarAndClubReviewsUrl { get; set; }

        public string ContactName { get; set; }
        public string MainPhoneNumber { get; set; }
        public string[] SecondaryPhoneNumbers { get; set; }
        public string[] AddressLines { get; set; }
        public string PostCode { get; set; }
        public string Website { get; set; }
        public string EmailAddress { get; set; }

        public string Lat { get; set; }
        public string Long { get; set; }

        public List<Gig> Gigs { get; set; }
    }
}
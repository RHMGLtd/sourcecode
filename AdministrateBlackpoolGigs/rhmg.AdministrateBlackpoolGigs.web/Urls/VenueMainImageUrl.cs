using Snooze;

namespace rhmg.AdministrateBlackpoolGigs.web.Urls
{
    public class VenueMainImageUrl : Url
    {
        public VenueMainImageUrl()
        {
            
        }

        public VenueMainImageUrl(string venueName)
        {
            VenueName = venueName;
        }
        public string VenueName { get; set; }
    }
}
using Snooze;

namespace blackpoolgigs.web.Urls
{
    public class VenueUrl : Url
    {
        public VenueUrl() { }

        public VenueUrl(string venue)
        {
            Venue = venue;
        }
        public string Venue { get; set; }
    }
}
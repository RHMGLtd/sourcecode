using Snooze;

namespace blackpoolgigs.web.Urls
{
    public class VenueMapUrl : Url
    {
        public VenueMapUrl() { }

        public VenueMapUrl(string venue)
        {
            Venue = venue;
        }
        public string Venue { get; set; }
    }
}
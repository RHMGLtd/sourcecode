using blackpoolgigs.common.Models;

namespace blackpoolgigs.web.Models
{
    public class GigSummaryForMap
    {
        public string Bands { get; set; }
        public string Venue { get; set; }
        public GoogleMapCoords MapCoords { get; set; }
    }
}
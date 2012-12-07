using System.Globalization;
using blackpoolgigs.common.Models;

namespace blackpoolgigs.web.Models.ViewModels
{
    public class GigViewModel
    {
        public Gig Gig { get; set; }
        public VenueMetadata Metadata { get; set; }

        public int DateAsInt()
        {
            return Gig.Date.Day;
        }
        public string Month
        {
            get { return new DateTimeFormatInfo().GetMonthName(this.Gig.Date.Month); }
        }
        public string Year
        {
            get { return Gig.Date.Year.ToString(); }
        }
    }
}
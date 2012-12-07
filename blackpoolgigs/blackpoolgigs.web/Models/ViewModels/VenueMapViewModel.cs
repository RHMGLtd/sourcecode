using System;
using blackpoolgigs.common.Models;

namespace blackpoolgigs.web.Models.ViewModels
{
    public class VenueMapViewModel
    {
        public VenueMapViewModel()
        {
            Month = string.Empty;
            Year = DateTime.Now.Year.ToString();
        }
        public string VenueName { get; set; }
        public GoogleMapCoords MapCoords { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }
}
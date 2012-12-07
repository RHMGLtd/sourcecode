using System;
using System.Collections.Generic;
using blackpoolgigs.common.Models;

namespace blackpoolgigs.web.Models.ViewModels
{
    public class VenuesMapViewModel
    {
        public VenuesMapViewModel()
        {
            Month = string.Empty;
            Year = DateTime.Now.Year.ToString();
        }

        public IEnumerable<VenueMetadata> Venues { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }
}
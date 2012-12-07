using System;
using blackpoolgigs.common.Models;

namespace blackpoolgigs.web.Models.ViewModels
{
    public class VenueViewModel
    {
        public VenueViewModel()
        {
            Month = string.Empty;
            Year = DateTime.Now.Year.ToString();
        }

        public VenueDiary Diary { get; set; }
        public VenueMetadata Metadata { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }
}
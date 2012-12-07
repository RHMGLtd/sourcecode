using System;
using System.Collections.Generic;

namespace blackpoolgigs.web.Models.ViewModels
{
    public class DayMapViewModel
    {
        public string Date { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public List<GigSummaryForMap> Gigs { get; set; }
        public DateTime ForDate { get; set; }

        public int DateAsInt()
        {
            return int.Parse(Date);
        }
    }
}
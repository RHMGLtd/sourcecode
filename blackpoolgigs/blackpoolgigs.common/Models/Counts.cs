using System;

namespace blackpoolgigs.common.Models
{
    public class Counts
    {
        public int TotalGigs { get; set; }
        public int FutureGigs { get; set; }
        public int Bands { get; set; }
        public int Venues { get; set; }
        public DateTime AsOf { get; set; }
        public string AsOfString { get; set; }
    }
}
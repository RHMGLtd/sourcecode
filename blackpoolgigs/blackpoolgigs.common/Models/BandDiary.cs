using System.Collections.Generic;

namespace blackpoolgigs.common.Models
{
    public class BandDiary
    {
        public BandDiary()
        {
            Gigs = new List<Gig>();
        }

        public BandDiary(BandName name)
        {
            BandName = name;
            Gigs = new List<Gig>();
        }
        public BandName BandName { get; set; }
        public IEnumerable<Gig> Gigs { get; set; }
    }
}
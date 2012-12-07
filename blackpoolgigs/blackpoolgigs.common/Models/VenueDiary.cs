using System;
using System.Collections.Generic;
using System.Linq;

namespace blackpoolgigs.common.Models
{
    public class EmptyVenueDiary : VenueDiary
    {
        public EmptyVenueDiary()
        {
            IsNull = true;
            Gigs = new List<Gig>();
        }
    }

    public class VenueDiary
    {
        public bool IsNull { get; internal set; }
        public string Venue { get; set; }
        public List<Gig> Gigs { get; set; }

        public VenueDiary()
        {
            Gigs = new List<Gig>();
        }

        public List<Gig> Future
        {
            get { return Gigs.Where(x => x.Date >= DateTime.Now.Date).ToList(); }
        }

        public Gig ForDate(DateTime input)
        {
            return Gigs.Where(x => x.Date == input.Date).FirstOrDefault();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using blackpoolgigs.common.Models;

namespace blackpoolgigs.web.Models.ViewModels
{
    public class BandViewModel
    {
        public BandMetadata Metadata { get; set; }
        public BandDiary Diary { get; set; }
        public string AlphaPick { get; set; }

        public List<Gig> FutureGigs
        {
            get { return Diary.Gigs.Where(x => x.Date >= DateTime.Now.Date).OrderBy(x => x.Date).Take(5).ToList(); }
        }
        public List<Gig> HistoricGigs
        {
            get { return Diary.Gigs.Where(x => x.Date < DateTime.Now.Date).OrderByDescending(x => x.Date).Take(5).ToList(); }
        }
    }
}
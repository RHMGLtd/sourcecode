using System;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using blackpoolgigs.web.Services.Interfaces;

namespace blackpoolgigs.web.Services
{
    public class Diariser : IDiarise
    {
        protected readonly  IGiveYouGigs gigs;

        public Diariser(IGiveYouGigs gigs)
        {
            this.gigs = gigs;
        }

        public Diary Get(DateTime date)
        {
            return gigs.Get(date);
        }
    }
}
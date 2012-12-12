using System;
using System.Linq;
using blackpoolgigs.common.Models;
using coolbunny.common.Extensions;

namespace rhmg.AdministrateBlackpoolGigs.web.Models.ViewModels
{
    public class RepeatingGigViewModel
    {
        public RepeatingGigViewModel()
        {
            Created = DateTime.Now;
            BandNames = new string[] { };
        }

        public RepeatingGigViewModel(RepeatingGig gig)
        {
            Id = gig.Id;
            BandNames = gig.BandNames.Select(x => x.Value.SafeTrim()).ToArray();
            Venue = gig.Venue;
            StartTime = gig.StartTime;
            Price = gig.Price;
            MoreInfo = gig.MoreInfo;
            GigTitle = gig.GigTitle;
            Source = gig.Source;

            SequenceStartFrom = gig.SequenceStartFrom;
            SequenceEndOn = gig.SequenceEndOn;

            Mondays = gig.Mondays;
            Tuesdays = gig.Tuesdays;
            Wednesdays = gig.Wednesdays;
            Thursdays = gig.Thursdays;
            Fridays = gig.Fridays;
            Saturdays = gig.Saturdays;
            Sundays = gig.Sundays;

            Created = gig.Created;
            Edited = gig.Edited;
        }

        public RepeatingGig AsRepeatingGig()
        {
            return new RepeatingGig
            {
                Id = Id,
                BandNames = BandNames.Select(name => new BandName { Value = name.SafeTrim() }).ToArray(),
                Created = Created,
                Edited = Edited,
                Fridays = Fridays,
                GigTitle = GigTitle,
                Mondays = Mondays,
                MoreInfo = MoreInfo,
                Price = Price,
                Saturdays = Saturdays,
                SequenceEndOn = SequenceEndOn,
                SequenceStartFrom = SequenceStartFrom,
                Source = Source,
                StartTime = StartTime,
                Sundays = Sundays,
                Thursdays = Thursdays,
                Tuesdays = Tuesdays,
                Venue = Venue,
                Wednesdays = Wednesdays
            };
        }

        public string Id { get; set; }
        public string[] BandNames { get; set; }
        public string Venue { get; set; }
        public string StartTime { get; set; }
        public string Price { get; set; }
        public string MoreInfo { get; set; }
        public string GigTitle { get; set; }
        public string Source { get; set; }

        public DateTime SequenceStartFrom { get; set; }
        public DateTime? SequenceEndOn { get; set; }

        public bool Mondays { get; set; }
        public bool Tuesdays { get; set; }
        public bool Wednesdays { get; set; }
        public bool Thursdays { get; set; }
        public bool Fridays { get; set; }
        public bool Saturdays { get; set; }
        public bool Sundays { get; set; }

        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace blackpoolgigs.common.Models
{
    public class RepeatingGig
    {
        public RepeatingGig()
        {
            BandNames = new BandName[] { };
            Created = DateTime.Now;
        }

        /// <summary>
        /// Raven Id
        /// </summary>
        public string Id { get; set; }
        public BandName[] BandNames { get; set; }
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

        public IEnumerable<Gig> AsGigs()
        {
            var result = new List<Gig>();
            var endDate = SequenceEndOn.HasValue ? SequenceEndOn.Value : DateTime.Now.AddMonths(6);
            for (var date = SequenceStartFrom.Date; date <= endDate.Date; date = date.AddDays(1))
            {
                if (Mondays && date.DayOfWeek == DayOfWeek.Monday)
                    result.Add(AsGig(date));
                if (Tuesdays && date.DayOfWeek == DayOfWeek.Tuesday)
                    result.Add(AsGig(date));
                if (Wednesdays && date.DayOfWeek == DayOfWeek.Wednesday)
                    result.Add(AsGig(date));
                if (Thursdays && date.DayOfWeek == DayOfWeek.Thursday)
                    result.Add(AsGig(date));
                if (Fridays && date.DayOfWeek == DayOfWeek.Friday)
                    result.Add(AsGig(date));
                if (Saturdays && date.DayOfWeek == DayOfWeek.Saturday)
                    result.Add(AsGig(date));
                if (Sundays && date.DayOfWeek == DayOfWeek.Sunday)
                    result.Add(AsGig(date));
            }
            return result;
        }

        Gig AsGig(DateTime date)
        {
            return new Gig
                       {
                           Date = date,
                           Venue = Venue,
                           BandNames = BandNames,
                           StartTime = StartTime,
                           Price = Price,
                           MoreInfo = MoreInfo,
                           GigTitle = GigTitle,
                           Source = Source,
                           Created = Created,
                           Edited = Edited
                       };
        }
    }
}
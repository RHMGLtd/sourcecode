using System;
using System.Linq;
using blackpoolgigs.common.Models;
using coolbunny.common.Extensions;

namespace rhmg.AdministrateBlackpoolGigs.web.Models.ViewModels
{
    public class ManageViewModel
    {
        public ManageViewModel()
        {
            Date = DateTime.MinValue;
            BandNames = new string[] { };
            Created = DateTime.Now;
        }

        public ManageViewModel(Gig gig, string altId)
        {
            Id = string.IsNullOrWhiteSpace(gig.Id) ? altId : gig.Id;
            BandNames = gig.BandNames.Select(x => x.Value.SafeTrim()).ToArray();
            Venue = gig.Venue.SafeTrim();
            Date = gig.Date;
            StartTime = gig.StartTime.SafeTrim();
            Price = gig.Price.SafeTrim();
            MoreInfo = gig.MoreInfo.SafeTrim();
            GigTitle = gig.GigTitle.SafeTrim();
            Source = gig.Source;
            Created = gig.Created;
            Edited = gig.Edited;
            Cancelled = gig.Cancelled;
        }


        /// <summary>
        /// Raven Id
        /// </summary>
        public string Id { get; set; }
        public string[] BandNames { get; set; }
        public string Venue { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public string Price { get; set; }
        public string MoreInfo { get; set; }
        public string GigTitle { get; set; }
        public bool Cancelled { get; set; }
        /// <summary>
        /// Where did we get this from?
        /// </summary>
        public string Source { get; set; }
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }

        public Gig AsGig()
        {
            return new Gig
                       {
                           Id = Id,
                           BandNames = BandNames.Select(name => new BandName { Value = name.SafeTrim() }).ToArray(),
                           Venue = Venue.SafeTrim(),
                           Date = Date,
                           StartTime = StartTime.SafeTrim(),
                           Price = Price.SafeTrim(),
                           MoreInfo = MoreInfo.SafeTrim(),
                           GigTitle = GigTitle.SafeTrim(),
                           Source = Source,
                           Created = Created,
                           Edited = Edited,
                           Cancelled = Cancelled
                       };
        }

        public string SafeDateString()
        {
            return Date.SafeDate();
        }
    }
}
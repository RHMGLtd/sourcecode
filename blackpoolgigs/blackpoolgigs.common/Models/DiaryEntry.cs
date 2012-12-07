using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace blackpoolgigs.common.Models
{
    public class DiaryEntry
    {
        public bool HasGigs()
        {
            return Gigs.Any();
        }
        public DiaryEntry()
        {
            Gigs = new List<Gig>();
        }
        public DateTime Date { get; set; }
        public List<Gig> Gigs { get; set; }
        [JsonIgnore]
        public string Summary
        {
            get
            {
                var result = string.Empty;
                if (!Gigs.Any())
                    return result;
                foreach (var gig in Gigs)
                    result += gig.Summary + " | &#10;";
                result = result.Substring(0, result.Length - 8);
                return result;
            }
        }
        [JsonIgnore]
        public string CountSummary
        {
            get
            {
                if (Gigs.Any())
                {
                    if (Gigs.Count() == 1)
                        return "1 gig today";
                    return string.Format("{0} gigs today", Gigs.Count());
                }
                return "0 gigs today";
            }
        }
        public DiaryEntry Sort()
        {
            AddOrder(x => x.StartTime);
            return this;
        }

        public DiaryEntry AddOrder(Func<Gig, string > func)
        {
            Gigs = Gigs.OrderBy(func).ToList();
            return this;
        }
    }
}
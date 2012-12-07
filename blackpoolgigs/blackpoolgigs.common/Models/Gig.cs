using System;
using System.Linq;
using coolbunny.common.Extensions;
using Newtonsoft.Json;

namespace blackpoolgigs.common.Models
{
    public class Gig
    {
        /// <summary>
        /// Raven Id
        /// </summary>
        public string Id { get; set; }
        public BandName[] BandNames { get; set; }
        public string Venue { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public string Price { get; set; }
        public string MoreInfo { get; set; }
        public string GigTitle { get; set; }
        /// <summary>
        /// Where did we get this from?
        /// </summary>
        public string Source { get; set; }
        public bool Cancelled { get; set; }

        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }

        public Gig()
        {
            BandNames = new BandName[] { };
            Created = DateTime.Now;
        }
        [JsonIgnore]
        public string Summary
        {
            get
            {
                var result = string.Empty;
                if (BandNames.Any())
                {
                    foreach (var band in BandNames)
                    {
                        result = string.Format("{0}, {1}", result, band.Value);
                    }
                    result = result.Substring(2);
                }
                else
                {
                    result = GigTitle;
                }
                return string.Format("{0} at {1}", result, Venue);
            }
        }
    }

    public class BandName
    {
        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }

    public class Time
    {
        public Time(int hour, int minute)
        {
            if (!(hour.Between(0, 23) && minute.Between(0, 59)))
                throw new ArgumentOutOfRangeException();
            Hour = hour;
            Minute = minute;
        }

        public int Hour { get; set; }
        public int Minute { get; set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Hour, Minute);
        }
    }
}
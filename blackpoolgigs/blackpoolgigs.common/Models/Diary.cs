using System;
using System.Collections.Generic;
using System.Linq;

namespace blackpoolgigs.common.Models
{
    public class Diary
    {
        public DateTime FirstDayOfMonth { get; set; }
        public List<DiaryLine> Lines { get; set; }

        public bool HasGigs()
        {
            return Lines.Any(diaryLine => diaryLine.HasGigs());
        }

        public int GigCount()
        {
            var i = 0;
            foreach (var line in Lines)
            {
                foreach (var entry in line.Entries)
                {
                    i += entry.Gigs.Count();
                }
            }
            return i;
        }
        public Diary()
        {
            Lines = new List<DiaryLine>();
        }
        public Diary(DateTime target)
        {
            Lines = new List<DiaryLine>();
            BuildDiary(target);
        }
        private void BuildDiary(DateTime target)
        {
            FirstDayOfMonth = new DateTime(target.Year, target.Month, 1);
            var line = new DiaryLine();
            Lines.Add(line);
            var date = FirstDayOfMonth;
            line.Entries.Add(new DiaryEntry
                                 {
                                     Date = date
                                 });
            date = date.AddDays(1);
            while (date.Month == FirstDayOfMonth.Month)
            {
                if (date.DayOfWeek == DayOfWeek.Monday)
                {
                    line = new DiaryLine();
                    Lines.Add(line);
                }
                line.Entries.Add(new DiaryEntry
                {
                    Date = date
                });
                date = date.AddDays(1);
            }
        }

        /// <summary>
        /// Adds gigs to the diary - this is a "first in" process keyed on date and venue - if the diary already contains a gig for that venue on that date it will be ignored.
        /// </summary>
        /// <param name="gigs"></param>
        /// <returns></returns>
        public Diary AddGigs(List<Gig> gigs, bool checkForDuplicates)
        {
            gigs = gigs.OrderBy(g => g.Date).ToList();
            foreach (var gig in gigs)
            {
                foreach (var line in Lines)
                {
                    var found = line.ForDate(gig.Date);
                    if (found == null) continue;
                    if (checkForDuplicates && found.Gigs.Where(x => x.Date.Date == gig.Date.Date && x.Venue == gig.Venue).Any())
                        continue;
                    found.Gigs.Add(gig);
                    break;
                }
            }
            return this;
        }

        public DiaryEntry GetEntry(DateTime date)
        {
            foreach (var line in Lines)
            {
                var found = line.ForDate(date);
                if (found == null) continue;
                return found.Sort();
            }
            return new DiaryEntry
                       {
                           Date = date
                       };
        }

        public DiaryLine Week(DateTime date)
        {
            foreach (var line in Lines)
            {
                if (line.ForDate(date) == null)
                    continue;
                return line;
            }
            return new DiaryLine();
        }
    }
}
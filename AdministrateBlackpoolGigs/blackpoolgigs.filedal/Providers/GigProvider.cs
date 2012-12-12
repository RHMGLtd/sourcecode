using System;
using System.Collections.Generic;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using coolbunny.common.Extensions;
using coolbunny.common.Helpers;

namespace blackpoolgigs.filedal.Providers
{
    public class GigProvider : IGiveYouGigs, IDoOtherThingsWithGigs
    {
        protected readonly IAccessTheFileSystem cache;

        public GigProvider(IAccessTheFileSystem cache)
        {
            this.cache = cache;
        }

        public Counts Counts(DateTime date)
        {
            return cache.ReadOut<CountCollection>("counts.json").ForDate(date);
        }

        public MonthlyGigCounts MonthlyCounts(int year)
        {
            return cache.ReadOut<MonthlyGigCounts>("monthlyCounts.json").ForYear(year);
        }

        public DiaryLine Week(DateTime date)
        {
            var result = new DiaryLine();
            for (var d = date.GetMondayOfWeek(); d <= date.GetSundayOfWeek(); d = d.AddDays(1))
            {
                result.Entries.Add(GetEntry(d));
            }
            return result;
        }

        public IEnumerable<Gig> RecentlyUpdated()
        {
            return cache.ReadOutList<Gig>("recentlyUpdated.json");
        }

        public Diary Get(DateTime date)
        {
            return cache.ReadOut(date.ToString("MMM yyyy") + ".json", new Diary(date));
        }

        public DiaryEntry GetEntry(DateTime date)
        {
            return Get(date).GetEntry(date);
        }

        public PageOfResults<Gig> Get(PagingParams paging)
        {
            throw new NotImplementedException();
        }

        public Gig Get(string recordId)
        {
            throw new NotImplementedException();
        }

        public PageOfResults<Gig> Get(BandName band, PagingParams paging)
        {
            throw new NotImplementedException();
        }
    }
}
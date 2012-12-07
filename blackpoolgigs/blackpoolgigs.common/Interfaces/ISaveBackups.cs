using System.Collections.Generic;
using blackpoolgigs.common.Models;

namespace blackpoolgigs.common.Interfaces
{
    public interface ISaveBackups
    {
        void Save(IEnumerable<Diary> diaries, 
            CountCollection counts, 
            MonthlyGigCounts monthlyCounts,
            IEnumerable<VenueDiary> venueDiaries,
            IEnumerable<BandDiary> bandDiaries,
            IEnumerable<BandMetadata> bandMetadata,
            IEnumerable<VenueMetadata> venueMetadata,
            IEnumerable<StolenGear> stolenGear,
            IEnumerable<Gig> recentlyUpdated);
    }
}
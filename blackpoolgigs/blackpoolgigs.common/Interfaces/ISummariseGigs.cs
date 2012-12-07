using System.Collections.Generic;
using blackpoolgigs.common.Models;

namespace blackpoolgigs.common.Interfaces
{
    public interface ISummariseGigs
    {
        CountCollection GetCountCollection();
        MonthlyGigCounts MonthlyCounts();
        IEnumerable<Diary> GigDiaries();
        IEnumerable<Gig> AllGigs();
        IEnumerable<BandDiary> BandDiaries();
    }
}
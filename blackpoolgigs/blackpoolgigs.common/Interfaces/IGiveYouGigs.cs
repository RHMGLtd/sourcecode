using System;
using blackpoolgigs.common.Models;
using coolbunny.common.Helpers;

namespace blackpoolgigs.common.Interfaces
{
    public interface IGiveYouGigs
    {
        Counts Counts(DateTime date);
        Diary Get(DateTime date);
        PageOfResults<Gig> Get(PagingParams paging);
        Gig Get(string recordId);
        PageOfResults<Gig> Get(BandName band, PagingParams paging);
        DiaryEntry GetEntry(DateTime date);
        MonthlyGigCounts MonthlyCounts(int year);
        DiaryLine Week(DateTime date);
    }
}
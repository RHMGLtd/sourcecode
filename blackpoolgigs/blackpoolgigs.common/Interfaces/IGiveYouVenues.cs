using System.Collections.Generic;
using blackpoolgigs.common.Contracts;
using blackpoolgigs.common.Models;
using coolbunny.common.Helpers;

namespace blackpoolgigs.common.Interfaces
{
    public interface IGiveYouVenues
    {
        PageOfResults<VenueDiary> Get(PagingParams paging);
        PageOfResults<VenueDiaryPlusMetaData> DiaryPlusMetadata(PagingParams paging);
        VenueDiary Get(VenueName venueName);
        VenueMetadata GetMetadata(VenueName venueName);
        IEnumerable<VenueMetadata> Venues();
    }
}
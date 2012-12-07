using System.Collections.Generic;
using blackpoolgigs.common.Models;
using coolbunny.common.Helpers;

namespace blackpoolgigs.common.Interfaces
{
    public interface IHandleRepeatingGigs
    {
        RepeatingGig SaveRepeating(RepeatingGig gig);
        RepeatingGig GetRepeating(string id);
        PageOfResults<RepeatingGig> GetRepeating(PagingParams paging);
        RepeatingGig Delete(string id);
        List<Gig> AddGigsTo(List<Gig> addTo);
    }
}
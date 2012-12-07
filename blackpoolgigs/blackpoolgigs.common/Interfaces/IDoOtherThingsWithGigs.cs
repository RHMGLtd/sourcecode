using System.Collections.Generic;
using blackpoolgigs.common.Models;

namespace blackpoolgigs.common.Interfaces
{
    public interface IDoOtherThingsWithGigs
    {
        IEnumerable<Gig> RecentlyUpdated();
    }
}
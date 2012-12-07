using System.Collections.Generic;
using blackpoolgigs.common.Models;

namespace blackpoolgigs.common.Interfaces
{
    public interface  ISummariseVenues
    {
        IEnumerable<VenueDiary> VenueDiaries();
        IEnumerable<VenueMetadata> Metadata();
    }
}
using System.Collections.Generic;
using blackpoolgigs.common.Models;

namespace blackpoolgigs.common.Interfaces
{
    public interface ISummariseBands
    {
        int NumberOfBands();
        IEnumerable<BandMetadata> BandMetadata();
    }
}
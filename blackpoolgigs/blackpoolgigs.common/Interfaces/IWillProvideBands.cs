using System.Collections.Generic;
using blackpoolgigs.common.Models;
using coolbunny.common.Helpers;

namespace blackpoolgigs.common.Interfaces
{
    public interface IWillProvideBands
    {
        IEnumerable<BandName> BandNames();
        PageOfResults<BandName> GetBands(PagingParams paging, params string[] alphaPick);
        BandDiary GetDiary(BandName name);
        BandMetadata GetMetadata(BandName name);
        PageOfResults<BandUrls> GetBandUrls(PagingParams paging);
    }
}
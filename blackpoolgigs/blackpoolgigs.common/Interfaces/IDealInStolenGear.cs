using blackpoolgigs.common.Models;
using coolbunny.common.Helpers;

namespace blackpoolgigs.common.Interfaces
{
    public interface IDealInStolenGear
    {
        PageOfResults<StolenGear> Get(PagingParams paging);
        StolenGear Get(string id);
    }
}
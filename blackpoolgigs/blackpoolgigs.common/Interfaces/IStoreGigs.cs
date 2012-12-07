using blackpoolgigs.common.Models;

namespace blackpoolgigs.common.Interfaces
{
    public interface IStoreGigs
    {
        Gig Save(Gig gig);
        Gig Delete(string id);
    }
}
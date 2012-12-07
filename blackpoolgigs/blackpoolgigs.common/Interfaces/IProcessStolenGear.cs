using System.Web;
using blackpoolgigs.common.Models;

namespace blackpoolgigs.common.Interfaces
{
    public interface IProcessStolenGear
    {
        StolenGear Save(StolenGear input, params HttpPostedFileBase[] images);
        StolenGear Delete(string id);
    }
}
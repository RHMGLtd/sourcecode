using System.Web;
using blackpoolgigs.common.Models;

namespace blackpoolgigs.common.Interfaces
{
    public interface IStoreMetadata
    {
        VenueMetadata Store(VenueMetadata metadata, HttpPostedFileBase mainImage);
        bool Delete(string id);
    }
}
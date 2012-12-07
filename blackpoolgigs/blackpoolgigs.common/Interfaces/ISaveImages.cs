using System.Web;

namespace blackpoolgigs.common.Interfaces
{
    public interface ISaveImages
    {
        void Save(HttpPostedFileBase[] images, string[] names, string subFolderName);
    }
}
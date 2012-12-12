using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using blackpoolgigs.common.Interfaces;
using coolbunny.common.Extensions;
using OpenFileSystem.IO;

namespace blackpoolgigs.filedal.Providers
{
    public class Imagerer : ISaveImages
    {
        protected readonly IFileSystem files;

        public Imagerer(IFileSystem files)
        {
            this.files = files;
        }

        public void Save(HttpPostedFileBase[] images, string[] names, string subFolderName)
        {
            var path = subFolderName == null ? HostingEnvironment.MapPath("~/Content/StolenImages/") : HostingEnvironment.MapPath("~/Content/StolenImages/" + subFolderName + "/");
            if (!files.GetDirectory(path).Exists)
                files.CreateDirectory(path);

            var keepNames = new List<string>();
            for (var i = 0; i < images.Length; i++)
            {
                keepNames.Add(names[i]);
                if (images[i] == null)
                    continue;
                var image = Image.FromStream(images[i].InputStream).Resize(180);
                image.Save(path + names[i]);
            }
            foreach (var file in files.Files(path).Where(file => !keepNames.Where(f => f == file.Name).Any()))
            {
                file.Delete();
            }
        }
    }
}
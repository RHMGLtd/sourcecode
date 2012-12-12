using System;
using System.Linq;
using System.Web.Hosting;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using coolbunny.common.Extensions;
using coolbunny.common.Helpers;
using OpenFileSystem.IO;

namespace blackpoolgigs.filedal.Providers
{
    public class StolenGearProvider : IDealInStolenGear
    {
        protected readonly IAccessTheFileSystem cache;
        protected readonly IFileSystem files;

        public StolenGearProvider(IAccessTheFileSystem cache, IFileSystem files)
        {
            this.cache = cache;
            this.files = files;
        }

        public PageOfResults<StolenGear> Get(PagingParams paging)
        {
            var stolenGearFiles = files.GetDirectory(HostingEnvironment.MapPath("~/Content/Export/StolenGear/")).Files();
            var filtered = stolenGearFiles
                .Select(file => cache.ReadOut<StolenGear>("StolenGear/" + file.Name))
                .Where(s => !s.Archived)
                .OrderBy(s => s.Edited).ToList()
                .Skip(((paging.PageNumber - 1) * paging.PageSize))
                .Take(paging.PageSize);

            return new PageOfResults<StolenGear>
            {
                CurrentPageNumber = paging.PageNumber,
                NumberOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(stolenGearFiles.Count()) / Convert.ToDouble(paging.PageSize))),
                TotalNumberOfRecords = stolenGearFiles.Count(),
                Results = filtered.ToList()
            };
        }

        public StolenGear Get(string id)
        {
            var stolenGearFiles = files.GetDirectory(HostingEnvironment.MapPath("~/Content/Export/StolenGear/")).Files();
            return
                stolenGearFiles.Select(file => cache.ReadOut<StolenGear>("StolenGear/" + file.Name)).Where(
                    s => s.Id == id.EnsureStartsWith("stolengear/")).FirstOrDefault();
        }
    }
}
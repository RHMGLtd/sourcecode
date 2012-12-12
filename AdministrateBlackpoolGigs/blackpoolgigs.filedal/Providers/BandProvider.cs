using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Hosting;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using coolbunny.common.Helpers;
using OpenFileSystem.IO;

namespace blackpoolgigs.filedal.Providers
{
    public class BandProvider : IWillProvideBands, ISummariseBands
    {
        protected readonly IAccessTheFileSystem fs;
        protected readonly IFileSystem files;

        public BandProvider(IAccessTheFileSystem fs, IFileSystem files)
        {
            this.fs = fs;
            this.files = files;
        }

        public IEnumerable<BandName> BandNames()
        {
            return files.GetDirectory(HostingEnvironment.MapPath("~/Content/Export/Bands/")).Files().Select(
                    x => new BandName { Value = x.NameWithoutExtension });
        }

        public PageOfResults<BandName> GetBands(PagingParams paging, params string[] alphaPick)
        {
            var bands = BandNames();
            var filter = alphaPick.Any() ? bands.Where(x => x.Value.StartsWith(alphaPick[0], true, CultureInfo.CurrentCulture)) : bands;
            
            var result = filter.OrderBy(x => x.Value)
                .Skip(((paging.PageNumber - 1) * paging.PageSize))
                .Take(paging.PageSize);
            return new PageOfResults<BandName>
            {
                CurrentPageNumber = paging.PageNumber,
                NumberOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(filter.Count()) / Convert.ToDouble(paging.PageSize))),
                TotalNumberOfRecords = filter.Count(),
                Results = result
            };
        }

        public BandDiary GetDiary(BandName name)
        {
            return fs.ReadOut("Bands/" + name.Value + ".json", new BandDiary(name));
        }

        public BandMetadata GetMetadata(BandName name)
        {
            return fs.ReadOut("Bands/Metadata/" + name.Value + ".json", new BandMetadata(name.Value));
        }

        public PageOfResults<BandUrls> GetBandUrls(PagingParams paging)
        {
            throw new NotImplementedException();
        }

        public int NumberOfBands()
        {
            return BandNames().Count();
        }

        public IEnumerable<BandMetadata> BandMetadata()
        {
            throw new NotImplementedException();
        }
    }
}
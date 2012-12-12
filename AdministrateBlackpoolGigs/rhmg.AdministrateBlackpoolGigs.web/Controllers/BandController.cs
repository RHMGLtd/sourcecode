using AutoMapper;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using coolbunny.common.Helpers;
using rhmg.AdministrateBlackpoolGigs.web.Models.ViewModels;
using rhmg.AdministrateBlackpoolGigs.web.Urls;
using Snooze;

namespace rhmg.AdministrateBlackpoolGigs.web.Controllers
{
    public class BandController : ResourceController
    {
        protected readonly IWillProvideBands bands;
        protected readonly IGiveYouGigs gigs;
        protected readonly ISaveBands bandSave;

        public BandController(IWillProvideBands bands, IGiveYouGigs gigs, ISaveBands bandSave)
        {
            this.bands = bands;
            this.bandSave = bandSave;
            this.gigs = gigs;
        }

        public ResourceResult Get(BandsUrl url)
        {
            return OK(new BandsViewModel
                          {
                              Bands = bands.GetBands(new PagingParams(url)),
                              Params = new PagingParams(url)
                          }).AsHtml();
        }

        public ResourceResult Get(BandUrl url)
        {
            var vm = Mapper.Map<BandMetadata, BandViewModel>(bands.GetMetadata(new BandName {Value = url.BandName}));
            vm.Params = new PagingParams(url);
            vm.Gigs = gigs.Get(new BandName {Value = url.BandName}, new PagingParams(url));
            return OK(vm).AsHtml();
        }

        public ResourceResult Post(BandUrl url, BandViewModel input)
        {
            bandSave.Save(Mapper.Map<BandViewModel, BandMetadata>(input));
            return Get(new BandUrl(input.BandName));
        }
    }
}
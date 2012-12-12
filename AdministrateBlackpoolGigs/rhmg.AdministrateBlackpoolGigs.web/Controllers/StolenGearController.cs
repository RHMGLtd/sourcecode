using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.ViewModels;
using coolbunny.common.Extensions;
using coolbunny.common.Helpers;
using rhmg.AdministrateBlackpoolGigs.web.Urls;
using Snooze;

namespace rhmg.AdministrateBlackpoolGigs.web.Controllers
{
    public class StolenGearController : ResourceController
    {
        protected readonly IDealInStolenGear gear;
        protected readonly IProcessStolenGear storeGear;

        public StolenGearController(IDealInStolenGear gear, IProcessStolenGear storeGear)
        {
            this.gear = gear;
            this.storeGear = storeGear;
        }

        public ResourceResult Get(StolenGearListUrl url)
        {
            return OK(new StolenGearListViewModel
                          {
                              Params = new PagingParams(url),
                              Items = gear.Get(new PagingParams(url))
                          }).AsHtml();
        }
        public ResourceResult Get(AddStolenGearUrl url)
        {
            return OK(new StolenGearViewModel()).AsHtml();
        }
        public ResourceResult Get(StolenGearUrl url)
        {
            return OK(new StolenGearViewModel(gear.Get(url.Id.EnsureStartsWith("stolengear/")))).AsHtml();
        }

        public ResourceResult Post(AddStolenGearUrl url, StolenGearViewModel input)
        {
            var id = storeGear.Save(input.AsGear(), input.Image1, input.Image2, input.Image3, input.Image4, input.Image5).Id;
            return OK(new StolenGearUrl(id)).AsHtml();
        }
        public ResourceResult Post(StolenGearUrl url, StolenGearViewModel input)
        {
            return OK(new StolenGearViewModel(storeGear.Save(input.AsGear(), input.Image1, input.Image2, input.Image3, input.Image4, input.Image5))).AsHtml();
        }

        public ResourceResult Delete(StolenGearUrl url)
        {
            storeGear.Delete(url.Id.EnsureStartsWith("stolengear/"));
            return OK(true).AsJson();
        }
    }
}
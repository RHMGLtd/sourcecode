using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.ViewModels;
using blackpoolgigs.web.Models.ViewModels;
using blackpoolgigs.web.Urls;
using coolbunny.common.Extensions;
using coolbunny.common.Helpers;
using Snooze;

namespace blackpoolgigs.web.Controllers
{
    public class ContentController : ResourceController
    {
        protected readonly IDealInStolenGear stolen;

        public ContentController(IDealInStolenGear stolen)
        {
            this.stolen = stolen;
        }

        public ResourceResult Get(AboutUrl url)
        {
            return OK(new AboutViewModel()).AsHtml();
        }

        public ResourceResult Get(BandLookingForAGigUrl url)
        {
            return OK(new BandLookingForAGigViewModel()).AsHtml();
        }

        public ResourceResult Get(StolenGearListUrl url)
        {
            return OK(new StolenGearListViewModel
                          {
                              Params = new PagingParams(url),
                              Items = stolen.Get(new PagingParams(url))
                          }).AsHtml();
        }
        public ResourceResult Get(StolenGearUrl url)
        {
            return OK(new StolenGearViewModel(stolen.Get(url.Id.EnsureStartsWith("stolengear/")))).AsHtml();
        }
    }
}
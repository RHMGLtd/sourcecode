using blackpoolgigs.common.Interfaces;
using coolbunny.common.Extensions;
using rhmg.AdministrateBlackpoolGigs.web.Urls;
using Snooze;

namespace rhmg.AdministrateBlackpoolGigs.web.Controllers
{
    public class LookupController : ResourceController
    {
        protected readonly ISuggestThings suggestMe;

        public LookupController(ISuggestThings suggestMe)
        {
            this.suggestMe = suggestMe;
        }

        public ResourceResult Get(LookupUrl url)
        {
            var result = new string[] {};
            if (url.Which == "Venues")
                result = suggestMe.Venues(url.Term.SafeTrim());
            if (url.Which == "BandNames")
                result = suggestMe.Bands(url.Term.SafeTrim());
            return OK(result).AsJson();
        }
    }
}
using coolbunny.common.Extensions;
using Snooze;

namespace rhmg.AdministrateBlackpoolGigs.web.Urls
{
    public class StolenGearUrl : Url
    {
        public StolenGearUrl()
        {

        }

        public StolenGearUrl(string id)
        {
            Id = id.RemoveStartsWith("stolengear");
        }
        public string Id { get; set; }
    }
}
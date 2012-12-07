using coolbunny.common.Extensions;
using Snooze;

namespace blackpoolgigs.web.Urls
{
    public class StolenGearUrl : Url
    {
        public StolenGearUrl()
        {
            
        }

        public StolenGearUrl(string id)
        {
            Id = id.RemoveStartsWith("stolengear/");
        }
        public string Id { get; set; }
    }
}
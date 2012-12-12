using coolbunny.common.Extensions;
using Snooze;

namespace rhmg.AdministrateBlackpoolGigs.web.Urls
{
    public class RepeatingGigUrl : Url
    {
        public RepeatingGigUrl()
        {
            
        }

        public RepeatingGigUrl(string id)
        {
            Id = id.RemoveStartsWith("repeatinggig/");
        }
        public string Id { get; set; }
    }
}
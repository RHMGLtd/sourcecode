using coolbunny.common.Extensions;
using Snooze;

namespace blackpoolgigs.web.Urls
{
    public class BandUrl : Url
    {
        public BandUrl()
        {
            
        }

        public BandUrl(string bandName)
        {
            BandName = bandName.Tidy();
        }
        public string BandName { get; set; }
    }
}
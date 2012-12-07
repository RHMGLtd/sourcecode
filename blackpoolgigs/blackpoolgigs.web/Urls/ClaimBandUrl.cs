using Snooze;

namespace blackpoolgigs.web.Urls
{
    public class ClaimBandUrl : Url
    {
        public ClaimBandUrl()
        {
            
        }

        public ClaimBandUrl(string bandName)
        {
            BandName = bandName;
        }
        public string BandName { get; set; }
    }
}
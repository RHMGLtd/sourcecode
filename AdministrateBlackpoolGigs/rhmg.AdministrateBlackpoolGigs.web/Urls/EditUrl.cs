using coolbunny.common.Extensions;
using Snooze;

namespace rhmg.AdministrateBlackpoolGigs.web.Urls
{
    public class EditUrl : Url
    {
        public EditUrl()
        {
            
        }

        public EditUrl(string id)
        {
            RecordId = id.RemoveStartsWith("gig/");
        }
        public string RecordId { get; set; }   
    }
}
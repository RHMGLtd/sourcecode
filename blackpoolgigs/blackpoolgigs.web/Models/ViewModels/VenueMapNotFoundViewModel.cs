using System;

namespace blackpoolgigs.web.Models.ViewModels
{
    public class VenueMapNotFoundViewModel : NotFoundViewModel
    {
        public VenueMapNotFoundViewModel(Blob blob, FourOhFourReason reason)
        {
            Reason = reason;
            Blob = blob;
            Month = string.Empty;
            Year = DateTime.Now.Year.ToString();
        }
    }
}
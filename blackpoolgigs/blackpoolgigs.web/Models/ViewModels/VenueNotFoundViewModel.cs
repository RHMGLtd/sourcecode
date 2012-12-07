using System;

namespace blackpoolgigs.web.Models.ViewModels
{
    public class VenueNotFoundViewModel : NotFoundViewModel
    {
        public VenueNotFoundViewModel(Blob blob)
        {
            Blob = blob;
            Month = string.Empty;
            Year = DateTime.Now.Year.ToString();   
        }
    }
}
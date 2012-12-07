using System;

namespace blackpoolgigs.web.Models.ViewModels
{
    public class FourOhFourViewModel : NotFoundViewModel
    {
        public FourOhFourViewModel(FourOhFourReason reason, Blob blob)
        {
            Reason = reason;
            Blob = blob;
            Month = string.Empty;
            Year = DateTime.Now.Year.ToString(); 
        }
    }
}
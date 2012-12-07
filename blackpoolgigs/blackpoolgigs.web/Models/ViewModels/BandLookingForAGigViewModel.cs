using System;

namespace blackpoolgigs.web.Models.ViewModels
{
    public class BandLookingForAGigViewModel
    {
        public BandLookingForAGigViewModel()
        {
            Month = string.Empty;
            Year = DateTime.Now.Year.ToString();
        }
        public string Month { get; set; }
        public string Year { get; set; }   
    }
}
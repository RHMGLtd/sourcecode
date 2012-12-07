using System;

namespace blackpoolgigs.web.Models.ViewModels
{
    public class AboutViewModel
    {
        public AboutViewModel()
        {
            Month = string.Empty;
            Year = DateTime.Now.Year.ToString();
        }
        public string Month { get; set; }
        public string Year { get; set; }
    }
}
using System;
using blackpoolgigs.common.Models;
using coolbunny.common.Helpers;

namespace blackpoolgigs.web.Models.ViewModels
{
    public class VenuesViewModel
    {
        public VenuesViewModel()
        {
            Month = string.Empty;
            Year = DateTime.Now.Year.ToString();
        }

        public PagingParams Params { get; set; }
        public PageOfResults<VenueDiary> Venues { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
    }
}
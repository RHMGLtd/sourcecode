using System;
using blackpoolgigs.common.Models;
using coolbunny.common.Helpers;

namespace blackpoolgigs.web.Models.ViewModels
{
    public class BandsViewModel
    {
        public BandsViewModel()
        {
            Month = string.Empty;
            Year = DateTime.Now.Year.ToString();
        }

        public int TotalNumberOfBands { get; set; }
        public PagingParams Params { get; set; }
        public PageOfResults<BandName> Bands { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string AlphaPick { get; set; }
    }
}
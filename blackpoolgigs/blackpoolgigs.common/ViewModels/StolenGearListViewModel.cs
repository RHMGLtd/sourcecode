using System;
using blackpoolgigs.common.Models;
using coolbunny.common.Helpers;

namespace blackpoolgigs.common.ViewModels
{
    public class StolenGearListViewModel
    {
        public StolenGearListViewModel()
        {
            Month = string.Empty;
            Year = DateTime.Now.Year.ToString();
        }
        public string Month { get; set; }
        public string Year { get; set; }

        public PagingParams Params { get; set; }
        public PageOfResults<StolenGear> Items { get; set; }
    }
}
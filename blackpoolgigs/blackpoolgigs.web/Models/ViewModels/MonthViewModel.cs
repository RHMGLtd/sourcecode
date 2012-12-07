using blackpoolgigs.common.Extensions;
using blackpoolgigs.common.Models;

namespace blackpoolgigs.web.Models.ViewModels
{
    public class MonthViewModel
    {
        public string Month { get; set; }
        public string Year { get; set; }
        public Diary Diary { get; set; }

        public bool YearBeforeArchived()
        {
            return YearBefore().YearIsArchived();
        }
        public int YearBefore()
        {
            return int.Parse(Year) - 1;
        }

        public int NextYear()
        {
            return int.Parse(Year) + 1;
        }
        public bool NextYearTooFarInFuture()
        {
            return NextYear().YearTooFarInFuture();
        }
    }
}
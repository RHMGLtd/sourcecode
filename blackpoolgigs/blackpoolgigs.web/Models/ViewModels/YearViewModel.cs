using blackpoolgigs.common.Models;

namespace blackpoolgigs.web.Models.ViewModels
{
    public class YearViewModel
    {
        public string Year { get; set; }
        public MonthlyGigCounts MonthlyCounts { get; set; }
    }
}
using blackpoolgigs.common.Models;

namespace blackpoolgigs.web.Models.ViewModels
{
    public class DayViewModel
    {
        public string Date { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public DiaryEntry Entry { get; set; }

        public int DateAsInt()
        {
            return int.Parse(Date);
        }
    }
}
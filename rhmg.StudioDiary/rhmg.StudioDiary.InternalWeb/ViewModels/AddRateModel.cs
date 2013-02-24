using System.Collections.Generic;

namespace rhmg.StudioDiary.InternalWeb.ViewModels
{
    public class AddRateModel
    {
        public int DaysPer { get; set; }
        public int HoursPer { get; set; }
        public int MinutesPer { get; set; }

        public double PoundsAmount { get; set; }
        public string Description { get; set; }

        public List<Rate> CurrentRates { get; set; } 
    }

    public class EditRateModel : AddRateModel
    {
        public string RateId { get; set; }
    }
}
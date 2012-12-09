using System;

namespace rhmg.StudioDiary
{
    public class Rate
    {
        public TimeSpan Per { get; set; }
        public double PoundsAmount { get; set; }
        public string Description { get; set; }

        public double For(TimeSpan length)
        {
            var factor = length.TotalHours / Per.TotalHours;
            return PoundsAmount*factor;
        }
    }
}
using System;

namespace rhmg.StudioDiary
{
    public struct TimePart
    {
        public short Hour { get; set; }
        public short Minute { get; set; }

        public static TimePart FromString(string input)
        {
            var split = input.Split(':');
            if (split.Length != 2)
                throw new ArgumentException("input string not in the correct format");
            short hour;
            short minute;
            short.TryParse(split[0], out hour);
            short.TryParse(split[1], out minute);
            if (hour < 1 || hour > 23)
                throw new ArgumentException("Hour is not an hour, mofo!");
            if (minute < 0 || minute > 59)
                throw new ArgumentException("Minute is not a minute, mofo!");
            return new TimePart
                       {
                           Hour = hour,
                           Minute = minute
                       };
        }

        public static TimeSpan Duration(string startInput, string endInput)
        {
            var start = FromString(startInput);
            var end = FromString(endInput);

            var startTime = new DateTime(2010, 1, 1, start.Hour, start.Minute, 0);
            var endTime = new DateTime(2010, 1, 1, end.Hour, end.Minute, 0);

            return endTime - startTime;
        }
        public override string ToString()
        {
            return string.Format("{0}:{1}", Hour.ToString("00"), Minute.ToString("00"));
        }

        public static TimePart EndTime(TimePart start, TimeSpan length)
        {
            var startTime = new DateTime(2010, 1, 1, start.Hour, start.Minute, 0);
            var end = startTime + length;
            return new TimePart
                       {
                           Hour = (short)end.Hour,
                           Minute = (short)end.Minute
                       };
        }
    }
}
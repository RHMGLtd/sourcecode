using System;

namespace rhmg.StudioDiary.Tests.Contexts.test_entities
{
    public class Rates
    {
        public static Rate standardEveningRate
        {
            get
            {
                return new Rate
                {
                    Description = "Standard Evening Four Hours",
                    Per = new TimeSpan(4, 0, 0),
                    PoundsAmount = 25.00
                };
            }
        }
        public static Rate liveRoomEveningRate
        {
            get
            {
                return new Rate
                {
                    Description = "Live Room Evening Four Hours",
                    Per = new TimeSpan(4, 0, 0),
                    PoundsAmount = 35.00
                };
            }
        }
        public static Rate daytimeHourly
        {
            get
            {
                return new Rate
                {
                    Description = "Daytime Hourly",
                    Per = new TimeSpan(1, 0, 0),
                    PoundsAmount = 7.50
                };
            }
        }
        public static Rate liveRoomDayTimeHourly
        {
            get
            {
                return new Rate
                {
                    Description = "Live Room Hourly",
                    Per = new TimeSpan(1, 0, 0),
                    PoundsAmount = 10.00
                };
            }
        } 
    }
}
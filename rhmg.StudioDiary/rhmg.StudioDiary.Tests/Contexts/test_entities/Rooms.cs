namespace rhmg.StudioDiary.Tests.Contexts.test_entities
{
    public class Rooms
    {
        public static Room liveRoom
        {
            get
            {
                return new Room
                {
                    Name = "Live Room",
                    Rates = new[]
                                           {
                                               Rates.liveRoomEveningRate,
                                               Rates.liverRoomDayTimeHourly
                                           }
                };
            }
        }
        public static Room room2
        {
            get
            {
                return new Room
                {
                    Name = "Room 2",
                    Rates = new[]
                                           {
                                               Rates.standardEveningRate,
                                               Rates.daytimeHourly
                                           }
                };
            }
        }
        public static Room room3
        {
            get
            {
                return new Room
                {
                    Name = "Room 3",
                    Rates = new[]
                                           {
                                               Rates.standardEveningRate,
                                               Rates.daytimeHourly
                                           }
                };
            }
        }
        public static Room room4
        {
            get
            {
                return new Room
                {
                    Name = "Room 4",
                    Rates = new[]
                                           {
                                               Rates.standardEveningRate,
                                               Rates.daytimeHourly
                                           }
                };
            }
        } 
    }
}
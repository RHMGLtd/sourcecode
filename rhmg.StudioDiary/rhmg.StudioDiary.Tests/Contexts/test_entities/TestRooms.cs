namespace rhmg.StudioDiary.Tests.Contexts.test_entities
{
    public class TestRooms
    {
        public static Room controlRoom
        {
            get
            {
                return new Room
                {
                    Id= "rooms/1",
                    Name = "Live Room",
                    Rates = new[]
                                           {
                                               Rates.liveRoomEveningRate,
                                               Rates.liverRoomDayTimeHourly
                                           }
                };
            }
        }
        public static Room liveRoom
        {
            get
            {
                return new Room
                {
                    Id = "rooms/2",
                    Name = "Live Room",
                    Rates = new[]
                                           {
                                               Rates.liveRoomEveningRate,
                                               Rates.liverRoomDayTimeHourly
                                           }
                };
            }
        }
        public static Room room2NoRates
        {
            get
            {
                return new Room
                {
                    Id = "rooms/3",
                    Name = "Room 2"
                };
            }
        }
        public static Room room2
        {
            get
            {
                return new Room
                {
                    Id = "rooms/3",
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
                    Id = "rooms/4",
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
                    Id = "rooms/5",
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
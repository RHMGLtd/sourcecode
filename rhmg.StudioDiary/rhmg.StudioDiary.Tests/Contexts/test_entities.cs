using System;
using System.Collections.Generic;

namespace rhmg.StudioDiary.Tests.Contexts
{
    public class test_entities
    {
        public class Dates
        {
            public static DateTime monday = new DateTime(2012, 12, 3);
            public static DateTime tuesday = new DateTime(2012, 12, 4);
            public static DateTime wednesday = new DateTime(2012, 12, 5);
            public static DateTime thursday = new DateTime(2012, 12, 6);
            public static DateTime friday = new DateTime(2012, 12, 7);
            public static DateTime saturday = new DateTime(2012, 12, 8);
            public static DateTime sunday = new DateTime(2012, 12, 9);
        }
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
                                               liveRoomEveningRate,
                                               liverRoomDayTimeHourly
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
                                               standardEveningRate,
                                               daytimeHourly
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
                                               standardEveningRate,
                                               daytimeHourly
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
                                               standardEveningRate,
                                               daytimeHourly
                                           }
                    };
                }
            }
        }

        public static Contact TheBeatles
        {
            get
            {
                return new Contact
                           {
                               Name = "The Beatles",
                               PhoneNumber = "ScouseLandSomewhere"
                           };
            }
        }
        public static Booking standard_4_hour_evening_rehearsal_booking
        {
            get
            {
                var dp = new DateTime(2000,1,1);
                var st = new TimePart
                             {
                                 Hour = 12
                             };
                return Booking.Create(new List<Contact> { TheBeatles }, dp, st, new TimeSpan(4, 0, 0), Rooms.room4, standardEveningRate);
            }
        }
        
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
        public static Rate liverRoomDayTimeHourly
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
        public static AdditionalEquipment amplifier
        {
            get
            {
                return new AdditionalEquipment
                           {
                               Description = "Guitar Amplifier",
                               UnitCost = 5.00
                           };
            }
        }
        public static AdditionalEquipment cymbalSet
        {
            get
            {
                return new AdditionalEquipment
                           {
                               Description = "Cymbal Set",
                               UnitCost = 15.00
                           };
            }
        }
    }
}
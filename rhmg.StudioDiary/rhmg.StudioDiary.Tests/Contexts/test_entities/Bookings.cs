using System;

namespace rhmg.StudioDiary.Tests.Contexts.test_entities
{
    public class Bookings
    {
        public class rehearsals
        {
            public static Booking standard_4_hour_evening_rehearsal_booking
            {
                get
                {
                    var dp = new DateTime(2000, 1, 1);
                    var st = new TimePart
                                 {
                                     Hour = 12
                                 };
                    return Booking.Create(Contacts.TheBeatles.Id, dp, st, new TimeSpan(4, 0, 0), TestRooms.room4,
                                          Rates.standardEveningRate, Products.rehearsal);
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;

namespace rhmg.StudioDiary.Tests.Contexts.test_entities
{
    public class Bookings
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
                return Booking.Create(new List<Contact> { Contacts.TheBeatles }, dp, st, new TimeSpan(4, 0, 0), Rooms.room4, Rates.standardEveningRate);
            }
        } 
    }
}
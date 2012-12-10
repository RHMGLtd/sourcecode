using rhmg.StudioDiary.Tests.Contexts.test_entities;

namespace rhmg.StudioDiary.Tests.Contexts
{
    public class with_booking
    {
        public static Booking standard_4_hour_evening_rehearsal_booking;
        public with_booking()
        {
            standard_4_hour_evening_rehearsal_booking = Bookings.standard_4_hour_evening_rehearsal_booking;
        }
    }
}
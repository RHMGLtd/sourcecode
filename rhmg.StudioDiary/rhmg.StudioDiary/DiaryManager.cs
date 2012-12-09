using System;
using rhmg.StudioDiary.Extensions;

namespace rhmg.StudioDiary
{
    public class DiaryManager
    {
        public static WeekToAView WeekToAViewFor(DateTime date, IRepository<Booking> repository)
        {
            var monday = date.MondayDate();
            var sunday = date.SundayDate();
            var bookings = repository.Get(x => x.Date >= monday && x.Date <= sunday);
            return new WeekToAView(bookings);
        }
    }
}
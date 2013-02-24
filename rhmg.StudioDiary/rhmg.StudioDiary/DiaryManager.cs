using System;
using System.Linq;
using Raven.Client;
using Raven.Client.Linq;
using rhmg.StudioDiary.Extensions;

namespace rhmg.StudioDiary
{
    public class DiaryManager
    {
        public static WeekToAView WeekToAViewFor(DateTime date, IDocumentSession session)
        {
            var monday = date.MondayDate();
            var sunday = date.SundayDate();
            var bookings = session.Query<Booking>().Where(x => !x.IsCancelled && x.Date >= monday && x.Date <= sunday);
            return new WeekToAView(bookings, date.MondayDate(), date.SundayDate());
        }

        public static MonthToAView MonthToAViewFor(DateTime date, IDocumentSession session)
        {
            return new MonthToAView(session.Query<Booking>().Where(x => !x.IsCancelled && x.Date.Month == date.Month).ToList(), date);
        }

        public static DayViewBookingLists DayCheck(DateTime checkDate, IDocumentSession session)
        {
            return new DayViewBookingLists
                       {
                           Bookings = session.Query<Booking>().Where(x => !x.IsCancelled && x.Date.Date == checkDate.Date).ToList(),
                           Date = checkDate
                       };
        }
    }
}
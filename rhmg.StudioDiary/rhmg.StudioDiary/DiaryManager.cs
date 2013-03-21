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
            var monday = date.MondayDate().Date;
            var sunday = date.SundayDate().Date;
            var bookings = session.Advanced.LuceneQuery<Booking>()
                .Include(x => x.MainContactId)
                .WhereEquals(x => x.IsCancelled, false)
                .AndAlso()
                .WhereBetweenOrEqual(x => x.Date, monday, sunday);
            foreach (var booking in bookings)
                if (!string.IsNullOrEmpty(booking.MainContactId))
                    booking.MainContact = session.Load<Contact>(booking.MainContactId);
            var backFill = session.Advanced.LuceneQuery<BackFill>()
                .Include(x => x.MainContactId)
                .WhereEquals(x => x.Upgraded, false)
                .AndAlso()
                .WhereBetweenOrEqual(x => x.Date, monday, sunday);
            foreach (var bf in backFill)
                if (!string.IsNullOrEmpty(bf.MainContactId))
                    bf.MainContact = session.Load<Contact>(bf.MainContactId);

            return new WeekToAView(bookings, backFill, date.MondayDate(), date.SundayDate());
        }

        public static MonthToAView MonthToAViewFor(DateTime date, IDocumentSession session)
        {
            return new MonthToAView(session.Query<Booking>().Where(x => !x.IsCancelled && x.Date.Month == date.Month).ToList(), date);
        }

        public static DayViewBookingLists DayCheck(DateTime checkDate, IDocumentSession session)
        {
            var newBookings = session.Advanced.LuceneQuery<Booking>()
                .Include(x => x.MainContactId)
                .WhereEquals(x => x.IsCancelled, false)
                .AndAlso()
                .WhereEquals(x => x.Date, checkDate.Date)
                .ToList();
            foreach (var booking in newBookings)
                booking.MainContact = session.Load<Contact>(booking.MainContactId);
            return new DayViewBookingLists
                       {
                           Bookings = new BookingList(newBookings),
                           Date = checkDate
                       };
        }
    }
}
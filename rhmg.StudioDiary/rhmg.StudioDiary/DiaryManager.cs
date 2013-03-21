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

        public static FullMonthToAView FullMonthToAViewFor(DateTime dateTime, IDocumentSession session)
        {
            var firstDayOfMonth = dateTime.FirstDayOfMonth();
            var weekDayOfFirstDayOfMonth = firstDayOfMonth.DayOfWeek;
            var firstDateToFind = firstDayOfMonth;
            if (weekDayOfFirstDayOfMonth == DayOfWeek.Tuesday)
                firstDateToFind = firstDayOfMonth.AddDays(-1);
            if (weekDayOfFirstDayOfMonth == DayOfWeek.Wednesday)
                firstDateToFind = firstDayOfMonth.AddDays(-2);
            if (weekDayOfFirstDayOfMonth == DayOfWeek.Thursday)
                firstDateToFind = firstDayOfMonth.AddDays(-3);
            if (weekDayOfFirstDayOfMonth == DayOfWeek.Friday)
                firstDateToFind = firstDayOfMonth.AddDays(-4);
            if (weekDayOfFirstDayOfMonth == DayOfWeek.Saturday)
                firstDateToFind = firstDayOfMonth.AddDays(-5);
            if (weekDayOfFirstDayOfMonth == DayOfWeek.Sunday)
                firstDateToFind = firstDayOfMonth.AddDays(-6);

            var lastDayOfMonth = dateTime.LastDayOfMonth();
            var weekDayOfLastDayOfMonth = lastDayOfMonth.DayOfWeek;
            var lastDateToFind = lastDayOfMonth;
            if (weekDayOfLastDayOfMonth == DayOfWeek.Monday)
                lastDateToFind = lastDayOfMonth.AddDays(6);
            if (weekDayOfLastDayOfMonth == DayOfWeek.Tuesday)
                lastDateToFind = lastDayOfMonth.AddDays(5);
            if (weekDayOfLastDayOfMonth == DayOfWeek.Wednesday)
                lastDateToFind = lastDayOfMonth.AddDays(4);
            if (weekDayOfLastDayOfMonth == DayOfWeek.Thursday)
                lastDateToFind = lastDayOfMonth.AddDays(3);
            if (weekDayOfLastDayOfMonth == DayOfWeek.Friday)
                lastDateToFind = lastDayOfMonth.AddDays(2);
            if (weekDayOfLastDayOfMonth == DayOfWeek.Saturday)
                lastDateToFind = lastDayOfMonth.AddDays(1);

            var bookings = session.Advanced.LuceneQuery<Booking>()
                .WhereBetweenOrEqual(x => x.Date, firstDateToFind, lastDateToFind)
                .ToList();
            return new FullMonthToAView(bookings, firstDateToFind, lastDateToFind);
        }
    }
}
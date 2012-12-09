using System;
using System.Collections.Generic;
using Machine.Specifications;
using rhmg.StudioDiary.Tests.Contexts;

namespace rhmg.StudioDiary.Tests
{
    public class when_getting_the_week_to_a_view
    {
        public class when_there_is_a_booking_on_every_day : with_raven_integration<Booking, Booking>
        {
            static WeekToAView thisWeek;

            Establish context = () =>
            {
                var st = new TimePart
                {
                    Hour = 12
                };
                var mondayBooking = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.monday, st, new TimeSpan(4, 0, 0), test_entities.room4, test_entities.standardEveningRate);
                var tuesdayBooking = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.tuesday, st, new TimeSpan(4, 0, 0), test_entities.room4, test_entities.standardEveningRate);
                var wednesdayBooking = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.wednesday, st, new TimeSpan(4, 0, 0), test_entities.room4, test_entities.standardEveningRate);
                var thursdayBooking = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.thursday, st, new TimeSpan(4, 0, 0), test_entities.room4, test_entities.standardEveningRate);
                var fridayBooking = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.friday, st, new TimeSpan(4, 0, 0), test_entities.room4, test_entities.standardEveningRate);
                var saturdayBooking = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.saturday, st, new TimeSpan(4, 0, 0), test_entities.room4, test_entities.standardEveningRate);
                var sundayBooking = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.sunday, st, new TimeSpan(4, 0, 0), test_entities.room4, test_entities.standardEveningRate);
                mondayBooking.Save(new Repository<Booking>(session));
                tuesdayBooking.Save(new Repository<Booking>(session));
                wednesdayBooking.Save(new Repository<Booking>(session));
                thursdayBooking.Save(new Repository<Booking>(session));
                fridayBooking.Save(new Repository<Booking>(session));
                saturdayBooking.Save(new Repository<Booking>(session));
                sundayBooking.Save(new Repository<Booking>(session));
            };

            Because and_we_ask_for_the_week_to_a_view = () =>
                                                            {
                                                                thisWeek =
                                                                    DiaryManager.WeekToAViewFor(
                                                                        test_entities.Dates.monday, new Repository<Booking>(session));
                                                            };

            It has_an_entry_for_monday = () => thisWeek.Monday.Bookings.ShouldNotBeEmpty();
            It has_an_entry_for_tuesday = () => thisWeek.Tuesday.Bookings.ShouldNotBeEmpty();
            It has_an_entry_for_wednesday = () => thisWeek.Wednesday.Bookings.ShouldNotBeEmpty();
            It has_an_entry_for_thursday = () => thisWeek.Thursday.Bookings.ShouldNotBeEmpty();
            It has_an_entry_for_friday = () => thisWeek.Friday.Bookings.ShouldNotBeEmpty();
            It has_an_entry_for_saturday = () => thisWeek.Saturday.Bookings.ShouldNotBeEmpty();
            It has_an_entry_for_sunday = () => thisWeek.Sunday.Bookings.ShouldNotBeEmpty();
        }
    }
}
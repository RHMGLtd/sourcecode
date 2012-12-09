using System;
using System.Collections.Generic;
using System.Linq;
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
                var mondayBooking = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.monday, st, new TimeSpan(4, 0, 0), test_entities.Rooms.room4, test_entities.standardEveningRate);
                var tuesdayBooking = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.tuesday, st, new TimeSpan(4, 0, 0), test_entities.Rooms.room4, test_entities.standardEveningRate);
                var wednesdayBooking = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.wednesday, st, new TimeSpan(4, 0, 0), test_entities.Rooms.room4, test_entities.standardEveningRate);
                var thursdayBooking = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.thursday, st, new TimeSpan(4, 0, 0), test_entities.Rooms.room4, test_entities.standardEveningRate);
                var fridayBooking = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.friday, st, new TimeSpan(4, 0, 0), test_entities.Rooms.room4, test_entities.standardEveningRate);
                var saturdayBooking = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.saturday, st, new TimeSpan(4, 0, 0), test_entities.Rooms.room4, test_entities.standardEveningRate);
                var sundayBooking = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.sunday, st, new TimeSpan(4, 0, 0), test_entities.Rooms.room4, test_entities.standardEveningRate);
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

        public class when_we_are_full_on_one_day : with_raven_integration<Booking, Booking>
        {
            static WeekToAView thisWeek;

            Establish context = () =>
            {
                var st = new TimePart
                {
                    Hour = 19
                };
                var room2Booking = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.monday, st, new TimeSpan(4, 0, 0), test_entities.Rooms.room2, test_entities.standardEveningRate);
                var room3Booking = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.monday, st, new TimeSpan(4, 0, 0), test_entities.Rooms.room3, test_entities.standardEveningRate);
                var room4Booking = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.monday, st, new TimeSpan(4, 0, 0), test_entities.Rooms.room4, test_entities.standardEveningRate);
                var liveRoomBooking = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.monday, st, new TimeSpan(4, 0, 0), test_entities.Rooms.liveRoom, test_entities.liveRoomEveningRate);
                room2Booking.Save(new Repository<Booking>(session));
                room3Booking.Save(new Repository<Booking>(session));
                room4Booking.Save(new Repository<Booking>(session));
                liveRoomBooking.Save(new Repository<Booking>(session));
            };

            Because and_we_ask_for_the_week_to_a_view = () =>
            {
                thisWeek =
                    DiaryManager.WeekToAViewFor(
                        test_entities.Dates.monday, new Repository<Booking>(session));
            };

            It has_an_entry_for_monday = () => thisWeek.Monday.Bookings.ShouldNotBeEmpty();
            It has_an_entry_for_room_2_on_the_monday = () => thisWeek.Monday.Room2.ShouldNotBeEmpty();
            It has_an_entry_for_room_3_on_the_monday = () => thisWeek.Monday.Room3.ShouldNotBeEmpty();
            It has_an_entry_for_room_4_on_the_monday = () => thisWeek.Monday.Room4.ShouldNotBeEmpty();
            It has_an_entry_for_live_room_on_the_monday = () => thisWeek.Monday.LiveRoom.ShouldNotBeEmpty();
            It has_an_entry_for_control_room_on_the_monday = () => thisWeek.Monday.ControlRoom.ShouldBeEmpty();
        }
    }

    public class when_getting_month_to_a_view
    {
        public class when_there_is_available_peak_slots_on_one_day_and_not_on_another_on_week_days : with_raven_integration<Booking, Booking>
        {
            static MonthToAView monthToAView;

            Establish context = () =>
            {
                var sixPM = new TimePart
                {
                    Hour = 18
                };
                var sevenPM = new TimePart
                {
                    Hour = 19
                };
                var room2Booking = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.monday, sixPM, new TimeSpan(4, 0, 0), test_entities.Rooms.room2, test_entities.standardEveningRate);
                var room3Booking = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.monday, sevenPM, new TimeSpan(4, 0, 0), test_entities.Rooms.room3, test_entities.standardEveningRate);
                var room4Booking = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.monday, sevenPM, new TimeSpan(4, 0, 0), test_entities.Rooms.room4, test_entities.standardEveningRate);
                var liveRoomBooking = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.monday, sevenPM, new TimeSpan(4, 0, 0), test_entities.Rooms.liveRoom, test_entities.liveRoomEveningRate);

                var tuesdayRoom3Booking = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.tuesday, sevenPM, new TimeSpan(4, 0, 0), test_entities.Rooms.room3, test_entities.standardEveningRate);

                room2Booking.Save(new Repository<Booking>(session));
                room3Booking.Save(new Repository<Booking>(session));
                room4Booking.Save(new Repository<Booking>(session));
                liveRoomBooking.Save(new Repository<Booking>(session));
                tuesdayRoom3Booking.Save(new Repository<Booking>(session));
            };
            Because and_we_ask_for_the_month_to_a_view = () =>
            {
                monthToAView =
                    DiaryManager.MonthToAViewFor(
                        test_entities.Dates.monday, new Repository<Booking>(session));
            };

            It has_no_availability_for_the_monday =
                () => monthToAView.WithPeakAvailability.FirstOrDefault(x => x.Date == test_entities.Dates.monday).HasAvailability.ShouldBeFalse();
            It has_availability_for_the_tuesday =
                () => monthToAView.WithPeakAvailability.FirstOrDefault(x => x.Date == test_entities.Dates.tuesday).HasAvailability.ShouldBeTrue();
        }

        public class when_there_is_recording_on_a_saturday_of_eight_hours_length : with_raven_integration<Booking, Booking>
        {
            static MonthToAView monthToAView;

            Establish context = () =>
            {
                var time = new TimePart
                {
                    Hour = 10
                };
                var liveRoomBooking = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.saturday, time, new TimeSpan(8, 0, 0), test_entities.Rooms.liveRoom, test_entities.liveRoomEveningRate);

                liveRoomBooking.Save(new Repository<Booking>(session));
            };
            Because and_we_ask_for_the_month_to_a_view = () =>
            {
                monthToAView =
                    DiaryManager.MonthToAViewFor(
                        test_entities.Dates.monday, new Repository<Booking>(session));
            };
            It has_no_availability_for_the_saturday =
                () => monthToAView.WithPeakAvailability.FirstOrDefault(x => x.Date == test_entities.Dates.saturday).HasAvailability.ShouldBeFalse();
        }
        public class when_there_is_recording_on_a_saturday_of_four_hours_length : with_raven_integration<Booking, Booking>
        {
            static MonthToAView monthToAView;

            Establish context = () =>
            {
                var time = new TimePart
                {
                    Hour = 10
                };
                var liveRoomBooking = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.saturday, time, new TimeSpan(4, 0, 0), test_entities.Rooms.liveRoom, test_entities.liveRoomEveningRate);

                liveRoomBooking.Save(new Repository<Booking>(session));
            };
            Because and_we_ask_for_the_month_to_a_view = () =>
            {
                monthToAView =
                    DiaryManager.MonthToAViewFor(
                        test_entities.Dates.monday, new Repository<Booking>(session));
            };
            It has_no_availability_for_the_saturday =
                () => monthToAView.WithPeakAvailability.FirstOrDefault(x => x.Date == test_entities.Dates.saturday).HasAvailability.ShouldBeTrue();
        }

        public class when_there_are_multiple_recordings_on_saturday : with_raven_integration<Booking, Booking>
        {
            static MonthToAView monthToAView;

            Establish context = () =>
            {
                var ten = new TimePart
                {
                    Hour = 10
                };
                var four = new TimePart
                {
                    Hour = 16
                };
                var liveRoomBooking1 = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.saturday, ten, new TimeSpan(4, 0, 0), test_entities.Rooms.liveRoom, test_entities.liveRoomEveningRate);

                liveRoomBooking1.Save(new Repository<Booking>(session));
                var liveRoomBooking2 = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.saturday, four, new TimeSpan(4, 0, 0), test_entities.Rooms.liveRoom, test_entities.liveRoomEveningRate);

                liveRoomBooking2.Save(new Repository<Booking>(session));
            };
            Because and_we_ask_for_the_month_to_a_view = () =>
            {
                monthToAView =
                    DiaryManager.MonthToAViewFor(
                        test_entities.Dates.monday, new Repository<Booking>(session));
            };
            It has_no_availability_for_the_saturday =
                () => monthToAView.WithPeakAvailability.FirstOrDefault(x => x.Date == test_entities.Dates.saturday).HasAvailability.ShouldBeTrue();
        }
    }
}
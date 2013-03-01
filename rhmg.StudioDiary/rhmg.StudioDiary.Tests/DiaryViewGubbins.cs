using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using rhmg.StudioDiary.Tests.Contexts;
using rhmg.StudioDiary.Tests.Contexts.test_entities;

namespace rhmg.StudioDiary.Tests
{
    public class when_getting_the_week_to_a_view
    {
        public class when_there_is_a_booking_on_every_day : with_raven_integration<Booking, Booking>
        {
            static WeekToAView thisWeek;

            Establish context = () =>
                                    {
                                        var theBeatles = Contacts.TheBeatles;
                                        theBeatles.Save(session);
                                        var st = new TimePart
                                        {
                                            Hour = 12
                                        };
                                        var mondayBooking = Booking.Create(theBeatles.Id, Dates.monday, st, new TimeSpan(4, 0, 0), TestRooms.room4, Rates.standardEveningRate);
                                        var tuesdayBooking = Booking.Create(theBeatles.Id, Dates.tuesday, st, new TimeSpan(4, 0, 0), TestRooms.room4, Rates.standardEveningRate);
                                        var wednesdayBooking = Booking.Create(theBeatles.Id, Dates.wednesday, st, new TimeSpan(4, 0, 0), TestRooms.room4, Rates.standardEveningRate);
                                        var thursdayBooking = Booking.Create(theBeatles.Id, Dates.thursday, st, new TimeSpan(4, 0, 0), TestRooms.room4, Rates.standardEveningRate);
                                        var fridayBooking = Booking.Create(theBeatles.Id, Dates.friday, st, new TimeSpan(4, 0, 0), TestRooms.room4, Rates.standardEveningRate);
                                        var saturdayBooking = Booking.Create(theBeatles.Id, Dates.saturday, st, new TimeSpan(4, 0, 0), TestRooms.room4, Rates.standardEveningRate);
                                        var sundayBooking = Booking.Create(theBeatles.Id, Dates.sunday, st, new TimeSpan(4, 0, 0), TestRooms.room4, Rates.standardEveningRate);
                                        mondayBooking.Save(session);
                                        tuesdayBooking.Save(session);
                                        wednesdayBooking.Save(session);
                                        thursdayBooking.Save(session);
                                        fridayBooking.Save(session);
                                        saturdayBooking.Save(session);
                                        sundayBooking.Save(session);
                                    };

            Because and_we_ask_for_the_week_to_a_view = () =>
                                                            {
                                                                thisWeek =
                                                                    DiaryManager.WeekToAViewFor(
                                                                        Dates.monday, session);
                                                            };

            It has_the_contact_on_the_booking_on_monday =
                () => thisWeek.Monday.Bookings.Bookings.First().MainContact.ShouldNotBeNull();

            It has_an_entry_for_monday = () => thisWeek.Monday.Bookings.Bookings.ShouldNotBeEmpty();
            It has_the_correct_date_for_monday = () => thisWeek.Monday.Date.ShouldEqual(Dates.monday);
            It has_an_entry_for_tuesday = () => thisWeek.Tuesday.Bookings.Bookings.ShouldNotBeEmpty();
            It has_an_entry_for_wednesday = () => thisWeek.Wednesday.Bookings.Bookings.ShouldNotBeEmpty();
            It has_an_entry_for_thursday = () => thisWeek.Thursday.Bookings.Bookings.ShouldNotBeEmpty();
            It has_the_correct_date_for_thursday = () => thisWeek.Thursday.Date.ShouldEqual(Dates.thursday);
            It has_an_entry_for_friday = () => thisWeek.Friday.Bookings.Bookings.ShouldNotBeEmpty();
            It has_an_entry_for_saturday = () => thisWeek.Saturday.Bookings.Bookings.ShouldNotBeEmpty();
            It has_an_entry_for_sunday = () => thisWeek.Sunday.Bookings.Bookings.ShouldNotBeEmpty();
            It has_the_correct_date_for_sunday = () => thisWeek.Sunday.Date.ShouldEqual(Dates.sunday);
        }

        public class when_we_are_full_on_one_day : with_raven_integration<Booking, Booking>
        {
            static WeekToAView thisWeek;
            static Contact _theBeatles;

            Establish context = () =>
            {
                var st = new TimePart
                {
                    Hour = 19
                };
                _theBeatles = Contacts.TheBeatles.Save(session);
                var room2Booking = Booking.Create(_theBeatles.Id, Dates.monday, st, new TimeSpan(4, 0, 0), TestRooms.room2, Rates.standardEveningRate);
                var room3Booking = Booking.Create(_theBeatles.Id, Dates.monday, st, new TimeSpan(4, 0, 0), TestRooms.room3, Rates.standardEveningRate);
                var room4Booking = Booking.Create(_theBeatles.Id, Dates.monday, st, new TimeSpan(4, 0, 0), TestRooms.room4, Rates.standardEveningRate);
                var liveRoomBooking = Booking.Create(_theBeatles.Id, Dates.monday, st, new TimeSpan(4, 0, 0), TestRooms.liveRoom, Rates.liveRoomEveningRate);
                room2Booking.Save(session);
                room3Booking.Save(session);
                room4Booking.Save(session);
                liveRoomBooking.Save(session);
            };

            Because and_we_ask_for_the_week_to_a_view = () =>
            {
                thisWeek =
                    DiaryManager.WeekToAViewFor(
                        Dates.monday, session);
            };

            It has_an_entry_for_monday = () => thisWeek.Monday.Bookings.Bookings.ShouldNotBeEmpty();
            It has_an_entry_for_room_2_on_the_monday = () => thisWeek.Monday.ForRoom(TestRooms.room2).Bookings.ShouldNotBeEmpty();
            It has_an_entry_for_room_3_on_the_monday = () => thisWeek.Monday.ForRoom(TestRooms.room3).Bookings.ShouldNotBeEmpty();
            It has_an_entry_for_room_4_on_the_monday = () => thisWeek.Monday.ForRoom(TestRooms.room4).Bookings.ShouldNotBeEmpty();
            It has_an_entry_for_live_room_on_the_monday = () => thisWeek.Monday.ForRoom(TestRooms.liveRoom).Bookings.ShouldNotBeEmpty();
            It has_an_entry_for_control_room_on_the_monday = () => thisWeek.Monday.ForRoom(TestRooms.controlRoom).Bookings.ShouldBeEmpty();
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
                var room2Booking = Booking.Create(Contacts.TheBeatles.Id, Dates.monday, sixPM, new TimeSpan(4, 0, 0), TestRooms.room2, Rates.standardEveningRate);
                var room3Booking = Booking.Create(Contacts.TheBeatles.Id, Dates.monday, sevenPM, new TimeSpan(4, 0, 0), TestRooms.room3, Rates.standardEveningRate);
                var room4Booking = Booking.Create(Contacts.TheBeatles.Id, Dates.monday, sevenPM, new TimeSpan(4, 0, 0), TestRooms.room4, Rates.standardEveningRate);
                var liveRoomBooking = Booking.Create(Contacts.TheBeatles.Id, Dates.monday, sevenPM, new TimeSpan(4, 0, 0), TestRooms.liveRoom, Rates.liveRoomEveningRate);

                var tuesdayRoom3Booking = Booking.Create(Contacts.TheBeatles.Id, Dates.tuesday, sevenPM, new TimeSpan(4, 0, 0), TestRooms.room3, Rates.standardEveningRate);

                room2Booking.Save(session);
                room3Booking.Save(session);
                room4Booking.Save(session);
                liveRoomBooking.Save(session);
                tuesdayRoom3Booking.Save(session);
            };
            Because and_we_ask_for_the_month_to_a_view = () =>
            {
                monthToAView =
                    DiaryManager.MonthToAViewFor(
                        Dates.monday, session);
            };

            It has_no_availability_for_the_monday =
                () => monthToAView.WithPeakAvailability.FirstOrDefault(x => x.Date == Dates.monday).HasAvailability.ShouldBeFalse();
            It has_availability_for_the_tuesday =
                () => monthToAView.WithPeakAvailability.FirstOrDefault(x => x.Date == Dates.tuesday).HasAvailability.ShouldBeTrue();
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
                var liveRoomBooking = Booking.Create(Contacts.TheBeatles.Id, Dates.saturday, time, new TimeSpan(8, 0, 0), TestRooms.liveRoom, Rates.liveRoomEveningRate);

                liveRoomBooking.Save(session);
            };
            Because and_we_ask_for_the_month_to_a_view = () =>
            {
                monthToAView =
                    DiaryManager.MonthToAViewFor(
                        Dates.monday, session);
            };
            It has_no_availability_for_the_saturday =
                () => monthToAView.WithPeakAvailability.FirstOrDefault(x => x.Date == Dates.saturday).HasAvailability.ShouldBeFalse();
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
                var liveRoomBooking = Booking.Create(Contacts.TheBeatles.Id, Dates.saturday, time, new TimeSpan(4, 0, 0), TestRooms.liveRoom, Rates.liveRoomEveningRate);

                liveRoomBooking.Save(session);
            };
            Because and_we_ask_for_the_month_to_a_view = () =>
            {
                monthToAView =
                    DiaryManager.MonthToAViewFor(
                        Dates.monday, session);
            };
            It has_no_availability_for_the_saturday =
                () => monthToAView.WithPeakAvailability.FirstOrDefault(x => x.Date == Dates.saturday).HasAvailability.ShouldBeTrue();
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
                var liveRoomBooking1 = Booking.Create(Contacts.TheBeatles.Id, Dates.saturday, ten, new TimeSpan(4, 0, 0), TestRooms.liveRoom, Rates.liveRoomEveningRate);

                liveRoomBooking1.Save(session);
                var liveRoomBooking2 = Booking.Create(Contacts.TheBeatles.Id, Dates.saturday, four, new TimeSpan(4, 0, 0), TestRooms.liveRoom, Rates.liveRoomEveningRate);

                liveRoomBooking2.Save(session);
            };
            Because and_we_ask_for_the_month_to_a_view = () =>
            {
                monthToAView =
                    DiaryManager.MonthToAViewFor(
                        Dates.monday, session);
            };
            It has_no_availability_for_the_saturday =
                () => monthToAView.WithPeakAvailability.FirstOrDefault(x => x.Date == Dates.saturday).HasAvailability.ShouldBeTrue();
        }
    }
}
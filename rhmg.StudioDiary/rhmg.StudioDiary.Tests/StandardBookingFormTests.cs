using System;
using System.Linq;
using Machine.Specifications;
using rhmg.StudioDiary.InternalWeb.ViewModels;
using rhmg.StudioDiary.Tests.Contexts;
using rhmg.StudioDiary.Tests.Contexts.test_entities;

namespace rhmg.StudioDiary.Tests
{
    public class creating_a_booking_for_a_new_band_from_standard_form_booking_model : with_raven_integration<Booking, Booking>
    {
        static Booking _booking;
        static Product _product;
        static StandardFormBookingModel _model;
        static DateTime _date;
        Establish context = () =>
                                {
                                    _product = Products.rehearsal;
                                    _product.Save(session);
                                    var rate = Rates.liveRoomEveningRate;
                                    rate.Save(session);
                                    var room = TestRooms.liveRoom;
                                    room.Rates = new[] { rate };
                                    room.Save(session);
                                    _date = DateTime.Now.Date;
                                    _model = new StandardFormBookingModel
                                                 {
                                                     ProductFriendlyName = _product.Name,
                                                     Room = _product.RoomIds.First(),
                                                     RateId = rate.Id,
                                                     OneOffCharge = 0.00,
                                                     BandName = "bob",
                                                     Date = _date,
                                                     EmailAddress = "someone@something.net",
                                                     StartTime = "19:00",
                                                     EndTime = "23:00",
                                                     MainContactName = "bruce",
                                                     Notes = "some notes",
                                                     PhoneNumber = "01234 567890"
                                                 };
                                };
        Because of = () => _booking = _model.CreateBooking(_product, session);
        It has_saved_the_booking = () => _booking.Id.ShouldNotBeNull();
        It has_the_room_saved_correctly = () => _booking.Rooms.First().Id.ShouldEqual("rooms/2");
    }
    public class creating_a_booking_for_an_existing_band_from_standard_form_booking_model : with_raven_integration<Booking, Booking>
    {
        static Booking _booking;
        static Product _product;
        static StandardFormBookingModel _model;
        static DateTime _date;
        static Contact _band;
        Establish context = () =>
        {
            _product = Products.rehearsal;
            _product.Save(session);
            var rate = Rates.liveRoomEveningRate;
            rate.Save(session);
            var room = TestRooms.liveRoom;
            room.Rates = new[] { rate };
            room.Save(session);
            _band = Contacts.TheBeatles;
            _band.Save(session);
            _date = DateTime.Now.Date;
            _model = new StandardFormBookingModel
            {
                ProductFriendlyName = _product.Name,
                Room = _product.RoomIds.First(),
                ContactId = _band.Id,
                RateId = rate.Id,
                RatesToPickFromMaybe = room.Rates.ToList(),
                OneOffCharge = 0.00,
                Date = _date,
                StartTime = "19:00",
                EndTime = "23:00",
                Notes = "some notes",
                PhoneNumber = "01234 567890"
            };
        };
        Because of = () => _booking = _model.CreateBooking(_product, session);
        It has_saved_the_booking = () => _booking.Id.ShouldNotBeNull();
        It has_the_correct_contact = () => _booking.MainContactId.ShouldEqual(_band.Id);
        It has_the_room_saved_correctly = () => _booking.Rooms.First().Id.ShouldEqual("rooms/2");
    }
    public class creating_a_booking_for_an_existing_band_from_standard_form_booking_model_when_booking_out_the_place : with_raven_integration<Booking, Booking>
    {
        static Booking _booking;
        static Product _product;
        static StandardFormBookingModel _model;
        static DateTime _date;
        static Contact _band;
        Establish context = () =>
        {
            _product = Products.party;
            _product.Save(session);
            var room = TestRooms.liveRoom;
            room.Save(session);
            _band = Contacts.TheBeatles;
            _band.Save(session);
            _date = DateTime.Now.Date;
            _model = new StandardFormBookingModel
            {
                ProductFriendlyName = _product.Name,
                Room = _product.RoomIds.First(),
                ContactId = _band.Id,
                OneOffCharge = 150.00,
                Date = _date,
                StartTime = "12:00",
                EndTime = "14:00",
                Notes = "some notes",
                PhoneNumber = "01234 567890"
            };
        };
        Because of = () => _booking = _model.CreateBooking(_product, session);
        It has_saved_the_booking = () => _booking.Id.ShouldNotBeNull();
        It has_the_correct_contact = () => _booking.MainContactId.ShouldEqual(_band.Id);
        It has_the_room_saved_correctly = () => _booking.Rooms.Count.ShouldEqual(5);
    }
}
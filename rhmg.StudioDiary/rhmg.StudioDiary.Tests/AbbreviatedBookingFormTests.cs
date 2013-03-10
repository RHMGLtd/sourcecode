using System;
using System.Linq;
using Machine.Specifications;
using rhmg.StudioDiary.InternalWeb.ViewModels;
using rhmg.StudioDiary.Tests.Contexts;
using rhmg.StudioDiary.Tests.Contexts.test_entities;

namespace rhmg.StudioDiary.Tests
{
    public class creating_a_booking_for_a_new_band_from_extended_form_booking_model : with_raven_integration<Booking, Booking>
    {
        static Booking _booking;
        static Product _product;
        static ExtendedFormBookingModel _model;
        static DateTime _date;
        Establish context = () =>
                                {
                                    _product = Products.party;
                                    _product.Save(session);
                                    var room = TestRooms.liveRoom;
                                    room.Save(session);
                                    _date = DateTime.Now.Date;
                                    _model = new ExtendedFormBookingModel
                                                 {
                                                     ProductFriendlyName = _product.Name,
                                                     OneOffCharge = 200.00,
                                                     BandName = "bob",
                                                     Date = _date,
                                                     EmailAddress = "someone@something.net",
                                                     StartTime = "19:00",
                                                     EndTime = "23:00",
                                                     MainContactName = "bruce",
                                                     Notes = "some notes",
                                                     PhoneNumber = "01234 567890",
                                                     MakeUpSession = true,
                                                     NumberInParty = "10",
                                                     PizzaOnTheDay = true,
                                                     Address = "someone, somewhere",
                                                     Age = "8",
                                                     Biscuits = true,
                                                     Postcode = "OL8 3TH",
                                                     SecondaryContactName ="the mum",
                                                     SongChoice = "something shit, probably"
                                                 };
                                };
        Because of = () => _booking = _model.CreateBooking(_date, _product, session);
        It has_saved_the_booking = () => _booking.Id.ShouldNotBeNull();
        It has_the_room_saved_correctly = () => _booking.Rooms.Count().ShouldEqual(5);
    }
}
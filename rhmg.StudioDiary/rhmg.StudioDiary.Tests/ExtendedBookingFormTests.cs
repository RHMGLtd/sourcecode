using System;
using System.Linq;
using Machine.Specifications;
using rhmg.StudioDiary.InternalWeb.ViewModels;
using rhmg.StudioDiary.Tests.Contexts;
using rhmg.StudioDiary.Tests.Contexts.test_entities;

namespace rhmg.StudioDiary.Tests
{
    public class creating_a_booking_from_an_abbreviated_booking_form : with_raven_integration<Booking, Booking>
    {
        static Booking _booking;
        static Product _product;
        static AbbreviatedFormBookingModel _model;
        static DateTime _date;
        Establish context = () =>
                                {
                                    _product = Products.party;
                                    _product.Save(session);
                                    var room = TestRooms.liveRoom;
                                    room.Save(session);
                                    _date = DateTime.Now.Date;
                                    _model = new AbbreviatedFormBookingModel
                                                 {
                                                     ProductFriendlyName = _product.Name,
                                                     Date = _date,
                                                     StartTime = "19:00",
                                                     EndTime = "23:00",
                                                     Notes = "some notes"
                                                 };
                                };
        Because of = () => _booking = _model.CreateBooking(_product, session);
        It has_saved_the_booking = () => _booking.Id.ShouldNotBeNull();
        It has_the_room_saved_correctly = () => _booking.Rooms.Count().ShouldEqual(5);
    }
}
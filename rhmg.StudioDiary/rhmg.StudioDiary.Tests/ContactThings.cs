using System;
using System.Collections.Generic;
using Machine.Specifications;
using rhmg.StudioDiary.Tests.Contexts;
using rhmg.StudioDiary.Tests.Contexts.test_entities;

namespace rhmg.StudioDiary.Tests
{
    public class when_a_contact_is_saved : with_raven_integration<Contact, Contact>
    {
        static Contact contact;

        Establish context = () => contact = Contacts.TheBeatles;

        Because of = () => contact = contact.Save(session);

        It has_provided_an_id = () => contact.Id.ShouldEqual("contact/1");
    }

    public class getting_a_contact_by_phone_number : with_raven_integration<Contact, Contact>
    {
        static Contact contact;

        Establish context = () => Contacts.TheBeatles.Save(session);

        Because of = () => contact = Contact.GetByPhone(Contacts.TheBeatles.PhoneNumber, session);
        It is_the_correct_contact = () => contact.Name.ShouldEqual("The Beatles");

    }

    public class getting_bookings_for_a_contact : with_raven_integration<Contact, Contact>
    {
        static Contact contact;

        Establish context = () =>
                                {
                                    var bob = Contacts.TheBeatles.Save(session);
                                    var booking = Bookings.rehearsals.standard_4_hour_evening_rehearsal_booking;
                                    booking.MainContactId = bob.Id;
                                    booking.Save(session);
                                    wait();
                                };

        Because of = () => contact = Contact.Get("contact/1", session);

        It is_the_correct_contact = () => contact.Name.ShouldEqual("The Beatles");
        It has_the_bookings_loaded = () => contact.Bookings.Count.ShouldEqual(1);
    }

    public class applying_a_refund_to_a_contact_which_is_less_than_the_owings_on_last_cancelled_booking :
        with_raven_integration<Contact, Contact>
    {
        static Booking twoWeeksAgoAndCancelledWithNoNotice;
        static Booking oneWeekAgoAndComplete;
        static Booking thisWeekAndCancelledWithOneDayNotice;
        static Contact _theBeatles;

        Establish context = () =>
                                {
                                    _theBeatles = Contacts.TheBeatles.Save(session);
                                    _theBeatles.Save(session);
                                    twoWeeksAgoAndCancelledWithNoNotice =
                                        Booking.Create(_theBeatles.Id,
                                                       DateTime.Now.AddDays(-14),
                                                       new TimePart { Hour = 18 },
                                                       new TimeSpan(4, 0, 0), TestRooms.room4, Rates.standardEveningRate, Products.rehearsal);
                                    twoWeeksAgoAndCancelledWithNoNotice.Cancel(CancellationType.FullCost, "nobbers",
                                                                               twoWeeksAgoAndCancelledWithNoNotice.Date);
                                    twoWeeksAgoAndCancelledWithNoNotice.Save(session);

                                    oneWeekAgoAndComplete = Booking.Create(_theBeatles.Id,
                                                                           DateTime.Now.AddDays(-7),
                                                                           new TimePart { Hour = 18 },
                                                                           new TimeSpan(4, 0, 0), TestRooms.room4,
                                                                           Rates.standardEveningRate, Products.rehearsal);
                                    oneWeekAgoAndComplete.CheckIn();
                                    oneWeekAgoAndComplete.Save(session);
                                    oneWeekAgoAndComplete.ApplyPayment(new Payment
                                    {
                                        Amount = 25.00,
                                        DateMade = DateTime.Now.AddDays(-7),
                                        Method = PaymentMethod.Cash,
                                        PaymentType = PaymentType.Standard
                                    });
                                    oneWeekAgoAndComplete.Save(session);

                                    thisWeekAndCancelledWithOneDayNotice =
                                        Booking.Create(_theBeatles.Id,
                                                       DateTime.Now,
                                                       new TimePart { Hour = 18 },
                                                       new TimeSpan(4, 0, 0), TestRooms.room4, Rates.standardEveningRate, Products.rehearsal);
                                    thisWeekAndCancelledWithOneDayNotice.Cancel(CancellationType.HalfCost, "nobbers",
                                                                                twoWeeksAgoAndCancelledWithNoNotice.Date
                                                                                    .AddDays(-1));
                                    thisWeekAndCancelledWithOneDayNotice.Save(session);
                                    wait();
                                };

        Because of = () =>
        {
            _theBeatles.ApplyRefund(10.00, session);
            wait();
        };
        It has_reduced_the_owings = () => _theBeatles.CurrentlyOverdue(session).ShouldEqual(27.50);
    }
}

using System.Collections.Generic;
using Machine.Specifications;
using rhmg.StudioDiary.Tests.Contexts;

namespace rhmg.StudioDiary.Tests
{
    public class when_a_contact_is_saved : with_raven_integration<Contact, Contact>
    {
        static Contact contact;

        Establish context = () => contact = test_entities.TheBeatles;

        Because of = () => contact = contact.Save(new Repository<Contact>(session));

        It has_provided_an_id = () => contact.Id.ShouldEqual("contact/1");
    }

    public class getting_bookings_for_a_contact : with_raven_integration<Contact, Contact>
    {
        static Contact contact;

        Establish context = () =>
                                {
                                    var contact = test_entities.TheBeatles.Save(new Repository<Contact>(session));
                                    var booking = test_entities.standard_4_hour_evening_rehearsal_booking;
                                    booking.Contacts = new List<Contact> { contact };
                                    booking.Save(new Repository<Booking>(session));
                                };

        Because of = () => contact = Contact.Get("contact/1", new Repository<Contact>(session), new Repository<Booking>(session));

        It has_one_booking = () => contact.Bookings.Count.ShouldEqual(1);
        It has_a_total_currently_owed_for_all_bookings = () => contact.CurrentlyOwed(new Repository<Booking>(session)).ShouldEqual(25.00);
    }
}
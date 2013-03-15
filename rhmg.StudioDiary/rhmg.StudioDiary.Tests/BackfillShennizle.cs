using System.Collections.Generic;
using Machine.Specifications;
using rhmg.StudioDiary.Tests.Contexts;
using rhmg.StudioDiary.Tests.Contexts.test_entities;

namespace rhmg.StudioDiary.Tests
{
    public class when_a_new_backfill_is_created : with_raven_integration<BackFill, BackFill>
    {
        static BackFill backFill;

        Establish context = () =>
                                {
                                    var contact = Contacts.TheBeatles;
                                    contact.Save(session);
                                    backFill = new BackFill
                                                   {
                                                       MainContactId = contact.Id,
                                                       Date = Dates.monday
                                                   };
                                    backFill.Save(session);
                                };

        Because of = () => backFill = BackFill.Get(backFill.Id, session);

        It has_a_contact_set = () => backFill.MainContact.PhoneNumber.ShouldEqual(Contacts.TheBeatles.PhoneNumber);
        It has_a_date_set = () => backFill.Date.ShouldEqual(Dates.monday);
    }

    public class when_retrieving_backfill_for_a_specific_day : with_raven_integration<BackFill, BackFill>
    {
        static List<BackFill> backfills;
        static BackFill backFill;

        Establish context = () =>
        {
            var contact = Contacts.TheBeatles;
            contact.Save(session);
            backFill = new BackFill
            {
                MainContactId = contact.Id,
                Date = Dates.monday
            };
            backFill.Save(session);
        };
        Because of = () => backfills = BackFill.Get(BackFills.standardBackFill.Date, session);

        It finds_some = () => backfills.ShouldNotBeEmpty();
    }

    public class when_upgrading_backfill_to_a_booking : with_raven_integration<Booking, Booking>
    {
        static BackFill backfill;

        Establish context = () =>
        {
            backfill = BackFills.standardBackFill;
        };

        Because of = () => backfill.Upgrade();

        It has_recorded_the_backfill_as_upgraded = () => backfill.Upgraded.ShouldBeTrue();

    }
}
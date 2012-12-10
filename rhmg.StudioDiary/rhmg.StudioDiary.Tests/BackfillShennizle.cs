using Machine.Specifications;
using rhmg.StudioDiary.Tests.Contexts;
using rhmg.StudioDiary.Tests.Contexts.test_entities;

namespace rhmg.StudioDiary.Tests
{
    public class when_a_new_backfill_is_created
    {
        static BackFill backFill;

        Because of = () => backFill = BackFills.standardBackFill;

        It has_a_contact_set = () => backFill.Contact.PhoneNumber.ShouldEqual(Contacts.TheBeatles.PhoneNumber);
        It has_a_date_set = () => backFill.Date.ShouldEqual(Dates.monday);
    }

    public class when_a_new_backfill_is_saved : with_raven_integration<BackFill, BackFill>
    {
        static BackFill backFill;

        Establish context = () => backFill = BackFills.standardBackFill;

        Because of = () => backFill = backFill.Save(new Repository<BackFill>(session));

        It has_provided_an_id = () => backFill.Id.ShouldEqual("backfill/1");
    }

    public class when_retrieving_a_backfill : with_raven_integration<BackFill, BackFill>
    {
        static BackFill backfill;

        Establish context = () =>
                                {
                                    var bob = BackFills.standardBackFill;
                                    bob.Save(new Repository<BackFill>(session));
                                };

        Because of = () => backfill = BackFill.Get("backfill/1", new Repository<BackFill>(session));

        It has_loaded = () => backfill.ShouldNotBeNull();
    }
}
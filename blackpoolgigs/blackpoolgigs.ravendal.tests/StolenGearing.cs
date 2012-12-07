using System.Web;
using blackpoolgigs.common.Models;
using blackpoolgigs.ravendal.Providers;
using blackpoolgigs.ravendal.tests.contexts;
using coolbunny.common.Helpers;
using coolbunny.ravendal.Extensions;
using Machine.Specifications;

namespace blackpoolgigs.ravendal.tests
{
    public class storing_a_stolen_gear : with_raven_integration<StolenGearHandler, StolenGear>
    {
        static StolenGear result;
        Establish context = () => Service.Save(new StolenGear());
        Because of = () => result = session.Load<StolenGear>("stolengear/1");
        It is_not_null = () => result.ShouldNotBeNull();
    }
    public class getting_a_stolen_gear : with_raven_integration<StolenGearHandler, StolenGear>
    {
        static StolenGear result;
        Establish context = () =>
            {
                session.Store(new StolenGear());
                session.SaveChanges();
            };
        Because of = () => result = Service.Get("stolengear/1");
        It is_not_null = () => result.ShouldNotBeNull();
    }
    public class getting_a_list_of_stolen_gear : with_raven_integration<StolenGearHandler, StolenGear>
    {
        static PageOfResults<StolenGear> result;
        Establish context = () =>
        {
            session.Store(new StolenGear());
            session.SaveChanges();
            session.WaitForQueryToComplete("dynamic/stolengear");
        };
        Because of = () => result = Service.Get(new PagingParams(1, 10, "", ""));
        It is_not_null = () => result.TotalNumberOfRecords.ShouldEqual(1);
    }
    public class deleting_a_stolen_gear : with_raven_integration<StolenGearHandler, StolenGear>
    {
        Establish context = () =>
        {
            session.Store(new StolenGear());
            session.SaveChanges();
        };
        Because of = () => Service.Delete("stolengear/1");
        It is_not_there_any_more = () => session.Load<StolenGear>("stolengear/1").ShouldBeNull();
    }
    public class getting_stolen_gear_count : with_raven_integration<CountProvider, StolenGear>
    {
        static int result;

        Establish context = () =>
        {
            session.Store(new StolenGear());
            session.Store(new StolenGear());
            session.Store(new StolenGear());
            session.SaveChanges();
        };

        Because of = () => result = Service.StolenGear;
        It has_returned_three = () => result.ShouldEqual(3);
    }
}
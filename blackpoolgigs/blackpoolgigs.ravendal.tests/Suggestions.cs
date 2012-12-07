using System.Collections.Generic;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using blackpoolgigs.ravendal.Providers;
using blackpoolgigs.ravendal.tests.contexts;
using coolbunny.ravendal.Extensions;
using coolbunny.tests.common.contexts;
using Machine.Specifications;

namespace blackpoolgigs.ravendal.tests
{
    public class when_suggesting_venues_with_one_match : with_raven_integration<SuggestMe, List<string>>
    {
        static string[] result;
        Establish context = () =>
            {
                session.Store(new Gig
                                  {
                                      Venue = "test"
                                  });
                session.SaveChanges();
                session.WaitForQueryToComplete("Venues");
            };

        Because of = () => result = Service.Venues("test");

        It has_returned_one_match = () => result.Length.ShouldEqual(1);
    }

    public class when_suggesting_venues_where_two_could_match : with_raven_integration<SuggestMe, List<string>>
    {
        static string[] result;
        Establish context = () =>
        {
            session.Store(new Gig
            {
                Venue = "The Blue Room"
            });
            session.Store(new Gig
            {
                Venue = "The Castle Casino"
            });
            session.SaveChanges();
            session.WaitForQueryToComplete("Venues");
        };

        Because of = () => result = Service.Venues("the");

        It has_returned_one_match = () => result.Length.ShouldEqual(2);
    }

    public class when_suggesting_venues_where_two_could_match_with_spaces : with_raven_integration<SuggestMe, List<string>>
    {
        static string[] result;
        Establish context = () =>
        {
            session.Store(new Gig
            {
                Venue = "The Blue Room"
            });
            session.Store(new Gig
            {
                Venue = "The Castle Casino"
            });
            session.SaveChanges();
            session.WaitForQueryToComplete("Venues");
        };

        Because of = () => result = Service.Venues("the ");

        It has_returned_one_match = () => result.Length.ShouldEqual(2);
    }

    public class when_suggesting_venues_where_two_of_three_match : with_raven_integration<SuggestMe, List<string>>
    {
        static string[] result;
        Establish context = () =>
        {
            session.Store(new Gig
            {
                Venue = "The Blue Room"
            });
            session.Store(new Gig
            {
                Venue = "The Castle Casino"
            });
            session.Store(new Gig
            {
                Venue = "Uncle Toms Cabin"
            });
            session.SaveChanges();
            session.WaitForQueryToComplete("Venues");
        };

        Because of = () => result = Service.Venues("the");

        It has_returned_one_match = () => result.Length.ShouldEqual(2);
    }

    public class when_suggesting_venues_where_one_of_three_match : with_raven_integration<SuggestMe, List<string>>
    {
        static string[] result;
        Establish context = () =>
        {
            session.Store(new Gig
            {
                Venue = "The Blue Room"
            });
            session.Store(new Gig
            {
                Venue = "The Castle Casino"
            });
            session.Store(new Gig
            {
                Venue = "Uncle Toms Cabin"
            });
            session.SaveChanges();
            session.WaitForQueryToComplete("Venues");
        };

        Because of = () => result = Service.Venues("tom");

        It has_returned_one_match = () => result.Length.ShouldEqual(1);
    }

    public class when_suggesting_bands_where_there_is_only_one_option : with_service<SuggestMe>
    {
        static string[] result;

        Establish context = () => Stub<IWillProvideBands>()
                                      .Setup(x => x.BandNames())
                                      .Returns(new[]
                                                   {
                                                       new BandName {Value = "band"}
                                                   });

        Because of = () => result = Service.Bands("band");

        It has_returned_one_match = () => result.Length.ShouldEqual(1);
    }
    public class when_suggesting_bands_where_there_is_two_options : with_service<SuggestMe>
    {
        static string[] result;

        Establish context = () => Stub<IWillProvideBands>()
                                      .Setup(x => x.BandNames())
                                      .Returns(new[]
                                                   {
                                                       new BandName {Value = "band"},
                                                       new BandName { Value = "person"} 
                                                   });

        Because of = () => result = Service.Bands("band");

        It has_returned_one_match = () => result.Length.ShouldEqual(1);
    }
    public class when_suggesting_bands_where_there_is_two_options_both_match : with_service<SuggestMe>
    {
        static string[] result;
        
        Establish context = () => Stub<IWillProvideBands>()
                                      .Setup(x => x.BandNames())
                                      .Returns(new[]
                                                   {
                                                       new BandName {Value = "band 1"},
                                                       new BandName { Value = "band 2"} 
                                                   });

        Because of = () => result = Service.Bands("band");

        It has_returned_one_match = () => result.Length.ShouldEqual(2);
    }
    public class when_suggesting_bands_where_there_are_two_gigs_two_options_all_match : with_service<SuggestMe>
    {
        static string[] result;

        Establish context = () => Stub<IWillProvideBands>()
                                      .Setup(x => x.BandNames())
                                      .Returns(new[]
                                                   {
                                                       new BandName {Value = "band 1"},
                                                       new BandName { Value = "band 2"},
                                                       new BandName { Value = "band 3"},
                                                       new BandName { Value = "band 4"} 
                                                   });

        Because of = () => result = Service.Bands("band");

        It has_returned_one_match = () => result.Length.ShouldEqual(4);
    }
}
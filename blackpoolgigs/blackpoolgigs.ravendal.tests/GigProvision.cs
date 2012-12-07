using System;
using System.Collections.Generic;
using System.Linq;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using blackpoolgigs.ravendal.Providers;
using blackpoolgigs.ravendal.tests.contexts;
using coolbunny.common.Helpers;
using coolbunny.tests.common.contexts;
using Machine.Specifications;

namespace blackpoolgigs.ravendal.tests
{
    public class when_getting_future_gigs : with_raven_client_index<Gig>
    {
        static IEnumerable<Gig> gigs;
        Establish context = () =>
            {
                session.Store(new Gig
                {
                    BandNames = new[] { new BandName { Value = "The Silvertones" }, new BandName { Value = "The Beatles" } },
                    StartTime = "18:30",
                    Venue = "The Beach"
                });
                session.SaveChanges();
            };

        Because of = () => gigs = reading_index("Gigs/ByDate");

        It has_returned_one_gig = () => gigs.Count().ShouldEqual(1);
    }
    public class when_storing_a_gig : with_raven_integration<GigProvider, Gig>
    {
        Establish context = () =>
            {
                Service.Save(new Gig());
            };
        It has_stored_the_gig = () => session.Load<Gig>("gig/1").ShouldNotBeNull();
    }
    public class data_cleansing_gigs_coping_with_null_values : with_raven_integration<GigProvider, Gig>
    {
        static IEnumerable<Gig> result;
        Establish context = () =>
        {
            session.Store(new Gig
            {
                BandNames = new[] { new BandName { Value = "The Silvertones" }, new BandName { Value = "The Beatles" } },
                Venue = "The Beach",
                Price = "£2.00"
            });
            session.Store(new Gig
            {
                BandNames = new[] { new BandName { Value = "The Silvertones " }, new BandName { Value = "The Beatles" } },
                StartTime = "18:30 ",
                Price = "£2.00 "
            });
            session.Store(new Gig
            {
                BandNames = new[] { new BandName { Value = " The Silvertones" }, new BandName { Value = "The Beatles" } },
                StartTime = " 18:30",
                Venue = " The Beach"
            });
            session.SaveChanges();
        };

        Because of = () =>
        {
            Service.DataCleanseSpaces();
            result = session.Advanced.LuceneQuery<Gig>().WaitForNonStaleResults();
        };

        It has_not_thrown_an_error = () => result.Count().ShouldEqual(3);
    }
    public class getting_gig_counts_for_admin : with_raven_integration<CountProvider, Gig>
    {
        static int result;
        Establish context = () =>
        {
            session.Store(new Gig
            {
                BandNames = new[] { new BandName { Value = "The Silvertones" }, new BandName { Value = "The Beatles" } },
                Venue = "The Beach",
                Price = "£2.00"
            });
            session.Store(new Gig
            {
                BandNames = new[] { new BandName { Value = "The Silvertones " }, new BandName { Value = "The Beatles" } },
                StartTime = "18:30 ",
                Price = "£2.00 "
            });
            session.Store(new Gig
            {
                BandNames = new[] { new BandName { Value = " The Silvertones" }, new BandName { Value = "The Beatles" } },
                StartTime = " 18:30",
                Venue = " The Beach"
            });
            session.SaveChanges();
        };

        Because of = () => result = Service.Gigs;
        It has_three = () => result.ShouldEqual(3);
    }
    public class data_cleansing_gigs : with_raven_integration<GigProvider, Gig>
    {
        static IEnumerable<Gig> result;
        Establish context = () =>
        {
            session.Store(new Gig
            {
                BandNames = new[] { new BandName { Value = "The Silvertones" }, new BandName { Value = "The Beatles" } },
                StartTime = "18:30",
                Venue = "The Beach",
                Price = "£2.00"
            });
            session.Store(new Gig
            {
                BandNames = new[] { new BandName { Value = "The Silvertones " }, new BandName { Value = "The Beatles" } },
                StartTime = "18:30 ",
                Venue = "The Beach ",
                Price = "£2.00 "
            });
            session.Store(new Gig
            {
                BandNames = new[] { new BandName { Value = " The Silvertones" }, new BandName { Value = "The Beatles" } },
                StartTime = " 18:30",
                Venue = " The Beach",
                Price = " £2.00"
            });
            session.SaveChanges();
        };

        Because of = () =>
        {
            Service.DataCleanseSpaces();
            result = session.Advanced.LuceneQuery<Gig>().WaitForNonStaleResults();
        };

        It has_three_gigs = () => result.Count().ShouldEqual(3);

        It has_no_spaces_in_any_of_the_gigs = () =>
            {
                foreach (var gig in result)
                {
                    gig.BandNames[0].Value.ShouldEqual("The Silvertones");
                    gig.StartTime.ShouldEqual("18:30");
                    gig.Venue.ShouldEqual("The Beach");
                    gig.Price.ShouldEqual("£2.00");
                }
            };
    }
    public class storing_a_repeating_gig : with_raven_integration<RepeatingGigsProvider, RepeatingGig>
    {
        static RepeatingGig result;

        Establish context = () => Service.SaveRepeating(new RepeatingGig
                                                            {
                                                                SequenceStartFrom = DateTime.Now
                                                            });

        Because of = () => result = session.Load<RepeatingGig>("repeatinggig/1");
        It is_not_null = () => result.ShouldNotBeNull();
    }
    public class getting_a_repeating_gig : with_raven_integration<RepeatingGigsProvider, RepeatingGig>
    {
        static RepeatingGig result;

        Establish context = () => Service.SaveRepeating(new RepeatingGig
        {
            SequenceStartFrom = DateTime.Now
        });

        Because of = () => result = Service.GetRepeating("repeatinggig/1");
        It is_not_null = () => result.ShouldNotBeNull();
    }
    public class getting_repeating_gig_count : with_raven_integration<CountProvider, RepeatingGig>
    {
        static int result;
        Establish context = () =>
        {
            session.Store(new RepeatingGig
            {
                SequenceStartFrom = DateTime.Now
            });
            session.Store(new RepeatingGig
            {
                SequenceStartFrom = DateTime.Now
            });
            session.Store(new RepeatingGig
            {
                SequenceStartFrom = DateTime.Now
            });
            session.SaveChanges();
        };

        Because of = () => result = Service.RepeatingGigs;
        It should_return_3 = () => result.ShouldEqual(3);
    }
    public class getting_by_band_name : with_service<Bands>
    {
        static List<BandName> result;
        Establish context = () => Stub<ISummariseGigs>()
                                      .Setup(x => x.AllGigs())
                                      .Returns(new List<Gig>
                                                   {
                                                       new Gig
                                                           {
                                                               BandNames = new[] { new BandName { Value = "Pink Floyd" }, new BandName { Value = "The Beatles" } },
                                                               StartTime = "18:30",
                                                               Venue = "The Beach",
                                                               Price = "£2.00"
                                                           },
                                                       new Gig
                                                           {
                                                               BandNames = new[] { new BandName { Value = "Bob" }, new BandName { Value = "The Dropout Wives" } },
                                                               StartTime = "18:30 ",
                                                               Venue = "The Beach ",
                                                               Price = "£2.00 "
                                                           },
                                                       new Gig
                                                           {
                                                               BandNames = new[] { new BandName { Value = "Pink Floyd" }, new BandName { Value = "2 Blind Mice" } },
                                                               StartTime = " 18:30",
                                                               Venue = " The Beach",
                                                               Price = " £2.00"
                                                           }
                                                   });

        Because of = () => result = Service.BandNames().Where(x => x.Value == "Bob").ToList();

        It has_returned_one_gig = () => result.Count.ShouldEqual(1);
    }

    public class getting_gigs_by_date_when_there_are_lots_of_gigs : with_raven_integration<GigProvider, Gig>
    {
        static Diary diary;

        Establish context = () =>
            {
                for (int i = 0; i < 300; i++)
                {
                    session.Store(new Gig
                                      {
                                          Date = new DateTime(2011, 08, 19)
                                      });
                }
                session.SaveChanges();
            };

        Because of = () => diary = Service.Get(new DateTime(2011, 08, 19));

        It has_three_hundred_gigs_in_the_diary = () => diary.GigCount().ShouldEqual(300);
    }
}
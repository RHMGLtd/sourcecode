using System;
using System.Collections.Generic;
using System.Linq;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using blackpoolgigs.ravendal.Indexes;
using blackpoolgigs.ravendal.Providers;
using blackpoolgigs.ravendal.tests.contexts;
using coolbunny.ravendal.Extensions;
using Machine.Specifications;

namespace blackpoolgigs.ravendal.tests
{
    public class when_getting_all_unique_dates : with_raven_client_index<Months.ReduceResult>
    {
        static DateTime startingDate = new DateTime(2011, 04, 01);
        static IEnumerable<Months.ReduceResult> dates;
        Establish context = () =>
            {
                for (var i = 0; i < 5; i++)
                {
                    session.Store(new Gig
                                      {
                                          Date = startingDate.AddMonths(i)
                                      });
                }
                session.SaveChanges();
            };

        Because of = () =>
            {
                dates = session.Advanced.LuceneQuery<Months.ReduceResult>("Months").WaitForNonStaleResults().ToList();
                dates = dates.OrderBy(d => d.Month);
            };

        It has_returned_some_dates = () => dates.ShouldNotBeEmpty();
        It has_returned_five_months = () => dates.Count().ShouldEqual(5);
        It has_returned_apr_as_the_first_month = () => dates.First().Month.ShouldEqual(4);
        It has_returned_aug_as_the_last_month = () => dates.Last().Month.ShouldEqual(8);

        It has_returned_2011_as_the_year = () =>
            {
                foreach (var reduceResult in dates)
                {
                    reduceResult.Year.ShouldEqual(2011);
                }
            };
    }
    public class getting_distinct_venues : with_raven_client_index<Venues.ReduceResult>
    {
        static IEnumerable<Venues.ReduceResult> indexresult;
        Establish context = () =>
        {
            session.Store(new Gig
            {
                Venue = "The Blue Room"
            });
            session.Store(new Gig
            {
                Venue = "The Raikes"
            });
            session.Store(new Gig
            {
                Venue = "The Blue Room"
            });
            session.SaveChanges();
        };

        Because of = () =>
            {
                indexresult = session.Advanced.LuceneQuery<Venues.ReduceResult>("Venues").WaitForNonStaleResults().ToList();
            };

        It has_returned_results = () => indexresult.ShouldNotBeEmpty();
        It has_returned_two_options = () => indexresult.Count().ShouldEqual(2);
    }

    public class counting_dates_per_month : with_raven_integration<Summeries, MonthlyGigCounts>
    {
        static DateTime startingDate = new DateTime(2011, 04, 01);
        static MonthlyGigCounts result;
        Establish context = () =>
        {
            Stub<IHandleRepeatingGigs>()
                                  .Setup(s => s.AddGigsTo(Moq.It.IsAny<List<Gig>>()))
                                  .Returns((List<Gig> l) => l);
            for (var i = 0; i < 5; i++)
            {
                session.Store(new Gig
                {
                    Date = startingDate.AddMonths(i)
                });
                session.Store(new Gig
                {
                    Date = startingDate.AddMonths(i)
                });
            }
            session.SaveChanges();
        };

        Because of = () =>
            {
                result = Service.MonthlyCounts();
            };

        It has_two_gigs_each_month = () =>
            {
                foreach (var month in result.Counts)
                {
                    month.Count.ShouldEqual(2);
                }
            };
    }
    public class getting_monthly_diaries : with_raven_integration<Summeries, Diary>
    {
        static IEnumerable<Diary> result;
        static Diary may;
        static DateTime date = new DateTime(2011, 5, 31);
        Establish context = () =>
            {
                Stub<IHandleRepeatingGigs>()
                                      .Setup(s => s.AddGigsTo(Moq.It.IsAny<List<Gig>>()))
                                      .Returns((List<Gig> l) => l);
                for (var i = 0; i < 1000; i++)
                {
                    session.Store(new Gig
                                      {
                                          Date = date.AddDays(i),
                                      });
                }
                session.SaveChanges();
            };

        Because of = () =>
            {
                result = Service.GigDiaries();
                may = result.Where(x => x.FirstDayOfMonth.Month == 5 && x.FirstDayOfMonth.Year == 2011).First();
            };

        It has_returned_something = () => result.ShouldNotBeEmpty();
        It should_have_34_diaries = () => result.Count().ShouldEqual(34);
        It should_have_a_diary_for_May_2011 = () => may.ShouldNotBeNull();
        It should_have_gigs_in_May_2011 = () => may.HasGigs().ShouldBeTrue();
        It should_have_1_gigs_in_May_2011 = () => may.GigCount().ShouldEqual(1);
    }
    public class getting_venue_diaries : with_raven_integration<Summeries, VenueDiary>
    {
        static IEnumerable<VenueDiary> result;
        static DateTime date = new DateTime(2011, 5, 31);
        Establish context = () =>
        {
            Stub<IHandleRepeatingGigs>()
                                  .Setup(s => s.AddGigsTo(Moq.It.IsAny<List<Gig>>()))
                                  .Returns((List<Gig> l) => l);
            for (var i = 0; i < 1000; i++)
            {
                session.Store(new Gig
                {
                    Date = date.AddDays(i),
                    Venue = "venue " + i
                });
            }
            session.SaveChanges();
        };

        Because of = () =>
        {
            result = Service.VenueDiaries();
        };

        It has_returned_something = () => result.ShouldNotBeEmpty();
        It should_have_1000_diaries = () => result.Count().ShouldEqual(1000);
    }
    public class getting_daily_gig_counts : with_raven_integration<Summeries, Gig>
    {
        static CountCollection result;
        static DateTime date = new DateTime(2011, 5, 31);

        Establish context = () =>
        {
            Stub<IHandleRepeatingGigs>()
                                  .Setup(s => s.AddGigsTo(Moq.It.IsAny<List<Gig>>()))
                                  .Returns((List<Gig> l) => l);
            for (var i = 0; i < 100; i++)
            {
                session.Store(new Gig
                {
                    Date = date.AddDays(i),
                    Venue = "the venue"
                });
            }
            session.SaveChanges();
        };

        Because of = () => result = Service.GetCountCollection();

        It has_100_gigs_in_future_for_first_date = () => result.ForDate(date).FutureGigs.ShouldEqual(100);
        It has_1_gig_on_the_last_date = () => result.ForDate(date.AddDays(99)).FutureGigs.ShouldEqual(1);
    }

    public class when_there_are_two_gigs_at_the_same_venue_on_the_sameDay : with_raven_integration<Summeries, Gig>
    {
        static IEnumerable<Diary> result;
        Establish context = () =>
        {
            Stub<IHandleRepeatingGigs>()
                                  .Setup(s => s.AddGigsTo(Moq.It.IsAny<List<Gig>>()))
                                  .Returns((List<Gig> l) => l);
            session.Store(new Gig
            {
                Venue = "The North Euston",
                BandNames = new[] { new BandName { Value = "Heat" } },
                StartTime = "21:00",
                Date = new DateTime(2011, 05, 29)
            });
            session.Store(new Gig
            {
                Venue = "The North Euston",
                BandNames = new[] { new BandName { Value = "Troubadour" } },
                StartTime = "15:00",
                Date = new DateTime(2011, 05, 29)
            });
            session.SaveChanges();
            session.WaitForQueryToComplete("Months");
        };

        Because of = () => result = Service.GigDiaries();

        It has_stored_something = () => result.ShouldNotBeNull();
        It has_two_gigs = () => result.FirstOrDefault().GigCount().ShouldEqual(2);
        It has_the_troubadour_gig_first = () => result.FirstOrDefault().GetEntry(new DateTime(2011, 05, 29)).Gigs[0].StartTime.ShouldEqual("15:00");
        It has_the_heat_gig_second = () => result.FirstOrDefault().GetEntry(new DateTime(2011, 05, 29)).Gigs[1].StartTime.ShouldEqual("21:00");
    }
}
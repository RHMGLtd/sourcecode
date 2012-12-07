using System;
using System.Collections.Generic;
using blackpoolgigs.common.Extensions;
using blackpoolgigs.common.Models;
using Machine.Specifications;

namespace blackpoolgigs.common.tests
{
    public class detects_when_a_date_is_too_far_ahead
    {
        It returns_true_for_far_distant_date = () => new DateTime(DateTime.Now.AddYears(10).Year, 01, 01).YearTooFarInFuture().ShouldBeTrue();

        It returns_true_for_three_years_distant_date = () => new DateTime(DateTime.Now.AddYears(3).Year, 1, 1).YearTooFarInFuture().ShouldBeTrue();

        It returns_false_for_two_years_distant_date = () => new DateTime(DateTime.Now.AddYears(2).Year, 1, 1).YearTooFarInFuture().ShouldBeFalse();
    }

    public class sorting_diary_gigs_with_differing_dates
    {
        static List<VenueDiary> result;
        static DateTime date;
        Establish context = () =>
            {
                date = new DateTime(2011, 01, 06);
                result = new List<VenueDiary>();
                var gigs = new List<Gig>();
                for (var i = 0; i < 5; i++)
                {
                    gigs.Add(new Gig
                                 {
                                     Venue = "the venue",
                                     Date = date.AddDays(-i)
                                 });
                }
                result = new List<VenueDiary>
                             {
                                    new VenueDiary
                                        {
                                            Venue = "the venue",
                                            Gigs = gigs
                                        }
                             };
            };

        Because of = () => result = result.SortGigsInVenues();

        It has_put_them_in_the_correct_order = () =>
            {
                for (var i = 1; i < 5; i++)
                {
                    result[0].Gigs[i].Date.ShouldBeGreaterThan(result[0].Gigs[i - 1].Date);
                }
            };
    }
    public class sorting_diary_gigs_same_dates_different_times
    {
        static List<VenueDiary> result;
        static DateTime date;
        Establish context = () =>
        {
            date = new DateTime(2011, 01, 06);
            result = new List<VenueDiary>();
            var gigs = new List<Gig>
                           {
                               new Gig
                                   {
                                       Venue = "the venue",
                                       Date = date,
                                       StartTime = "17:00"
                                   },
                               new Gig
                                   {
                                       Venue = "the venue",
                                       Date = date,
                                       StartTime = "15:00"
                                   },
                               new Gig
                                   {
                                       Venue = "the venue",
                                       Date = date,
                                       StartTime = "13:00"
                                   }
                           };
            result = new List<VenueDiary>
                             {
                                    new VenueDiary
                                        {
                                            Venue = "the venue",
                                            Gigs = gigs
                                        }
                             };
        };

        Because of = () => result = result.SortGigsInVenues();

        It has_put_them_in_the_correct_order = () =>
        {
            result[0].Gigs[0].StartTime.ShouldEqual("13:00");
            result[0].Gigs[1].StartTime.ShouldEqual("15:00");
            result[0].Gigs[2].StartTime.ShouldEqual("17:00");
        };
    }
}
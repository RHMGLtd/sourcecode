using System;
using System.Linq;
using blackpoolgigs.common.Models;
using Machine.Specifications;

namespace blackpoolgigs.ravendal.tests
{
    public class extrapolating_gigs_from_a_repeating_gig_with_defined_end_date
    {
        static RepeatingGig result;
        Establish context = () =>
            {
                result = new RepeatingGig
                             {
                                 SequenceStartFrom = new DateTime(2011, 04, 25),
                                 SequenceEndOn = new DateTime(2011, 05, 30),
                                 Venue = "a venue",
                                 BandNames = new[] { new BandName { Value = "a band" } },
                                 Mondays = true
                             };
            };

        It returns_a_collection_of_gigs = () => result.AsGigs().ShouldNotBeEmpty();
        It returns_six_gigs = () => result.AsGigs().Count().ShouldEqual(6);
    }
    public class extrapolating_gigs_from_a_repeating_gig_without_defined_end_date
    {
        static RepeatingGig result;
        Establish context = () =>
        {
            result = new RepeatingGig
            {
                SequenceStartFrom = new DateTime(2011, 04, 25),
                Venue = "a venue",
                BandNames = new[] { new BandName { Value = "a band" } },
                Mondays = true,
                Tuesdays = true,
                Wednesdays = true,
                Thursdays = true,
                Fridays = true,
                Saturdays = true,
                Sundays = true
            };
        };

        It returns_a_collection_of_gigs = () => result.AsGigs().ShouldNotBeEmpty();

        It returns_a_last_gig_roughly_six_months_from_now = () => result.AsGigs().LastOrDefault().Date.Month.ShouldEqual(DateTime.Now.AddMonths(6).Month);
    }
}
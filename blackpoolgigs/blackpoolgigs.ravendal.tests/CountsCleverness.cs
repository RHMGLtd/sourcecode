using System;
using System.Collections.Generic;
using System.Linq;
using blackpoolgigs.common.Models;
using blackpoolgigs.ravendal.Providers;
using blackpoolgigs.ravendal.tests.contexts;
using Machine.Specifications;

namespace blackpoolgigs.ravendal.tests
{
    public abstract class for_counting_cleverness : with_raven_integration<GigProvider, Counts>
    {
        internal static IEnumerable<Counts> counts;

        Establish context = () =>
        {
            session.Store(new Gig
            {
                Venue = "Venue 1",
                Date = new DateTime(2011, 04, 19)
            });
            session.Store(new Gig
            {
                Venue = "Venue 2",
                Date = new DateTime(2011, 04, 19)
            });
            session.Store(new Gig
            {
                Venue = "Venue 2",
                Date = new DateTime(2011, 04, 23)
            });
            session.SaveChanges();
        };
    }

    public class accessing_the_index_directly : for_counting_cleverness
    {
        Because of = () => counts = session.Advanced.LuceneQuery<Counts>("GigCounts/EachDay").WaitForNonStaleResults();

        It has_two_entries = () => counts.Count().ShouldEqual(2);
    }

    public class accessing_the_index_with_param_before_both : for_counting_cleverness
    {
        Because of = () => counts = session.Advanced.LuceneQuery<Counts>("GigCounts/EachDay")
                                        .WhereGreaterThanOrEqual("AsOf", new DateTime(2011, 04, 18))
                                        .WaitForNonStaleResults();

        It has_two_entries = () => counts.Count().ShouldEqual(2);
    }

    public class accessing_the_index_with_param_between : for_counting_cleverness
    {
        Because of = () => counts = session.Advanced.LuceneQuery<Counts>("GigCounts/EachDay")
                                        .WhereGreaterThanOrEqual("AsOf", new DateTime(2011, 04, 21))
                                        .WaitForNonStaleResults();

        It has_two_entries = () => counts.Count().ShouldEqual(1);
        It has_the_correct_count = () => counts.First().FutureGigs.ShouldEqual(1);
    }

    public class accessing_the_index_with_param_after : for_counting_cleverness
    {
        Because of = () => counts = session.Advanced.LuceneQuery<Counts>("GigCounts/EachDay")
                                        .WhereGreaterThanOrEqual("AsOf", new DateTime(2011, 04, 25))
                                        .WaitForNonStaleResults();

        It has_two_entries = () => counts.Count().ShouldEqual(0);
    }

    public class expanding_out_gig_counts : for_counting_cleverness
    {
        static CountCollection coll;
        Because of = () =>
            {
                counts = session.Advanced.LuceneQuery<Counts>("GigCounts/EachDay")
                    .WhereGreaterThanOrEqual("AsOf", new DateTime(2011, 04, 18))
                    .WaitForNonStaleResults();
                coll = new CountCollection().Do(counts, 3, 3, 3);
            };

        It has_extrapolated_five_counts = () => coll.Counts.Count().ShouldEqual(5);

        It has_three_gigs_for_first_date = () => coll.ForDate(new DateTime(2011, 04, 19)).FutureGigs.ShouldEqual(3);
        It has_one_gig_for_a_middle_date = () => coll.ForDate(new DateTime(2011, 04, 21)).FutureGigs.ShouldEqual(1);
        It has_one_gigs_for_the_last_date = () => coll.ForDate(new DateTime(2011, 04, 23)).FutureGigs.ShouldEqual(1);

        It has_three_gigs_for_a_really_early_date = () => coll.ForDate(new DateTime(2011, 01, 01)).FutureGigs.ShouldEqual(3);
        It has_no_gigs_for_a_really_late_date = () => coll.ForDate(new DateTime(2012, 01, 01)).FutureGigs.ShouldEqual(0);
    }
}
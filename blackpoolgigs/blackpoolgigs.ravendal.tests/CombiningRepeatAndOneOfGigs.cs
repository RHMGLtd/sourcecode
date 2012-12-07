using System;
using System.Collections.Generic;
using System.Linq;
using blackpoolgigs.common.Models;
using blackpoolgigs.ravendal.Providers;
using blackpoolgigs.ravendal.tests.contexts;
using Machine.Specifications;

namespace blackpoolgigs.ravendal.tests
{
    public class when_a_repeat_gig_and_a_one_off_gig_clash : with_raven_integration<RepeatingGigsProvider, Gig>
    {
        static List<Gig> gigs;
        static List<Gig> result;
        static Gig target;
        Establish context = () =>
            {
                session.Store(new RepeatingGig
                                  {
                                      SequenceStartFrom = new DateTime(2011, 05, 01),
                                      SequenceEndOn = new DateTime(2011, 06, 10),
                                      Venue = "a venue",
                                      StartTime = "21:00",
                                      GigTitle = "Jam Night",
                                      Mondays = true
                                  });
                session.SaveChanges();
                gigs = new List<Gig>
                             {
                                 new Gig
                                     {
                                         Date = new DateTime(2011, 05, 30),
                                         Venue = "a venue",
                                         StartTime = "15:00",
                                         BandNames = new[]
                                                         {
                                                             new BandName { Value = "a band"}
                                                        }
                                     },
                                 new Gig
                                     {
                                         Date = new DateTime(2011, 05, 30),
                                         Venue = "another venue",
                                         StartTime = "15:00",
                                         BandNames = new[]
                                                         {
                                                             new BandName { Value = "a band"}
                                                        }
                                     },
                                 new Gig
                                     {
                                         Date = new DateTime(2011, 05, 30),
                                         Venue = "a third venue",
                                         StartTime = "15:00",
                                         BandNames = new[]
                                                         {
                                                             new BandName { Value = "a band"}
                                                        }
                                     }
                             };
            };

        Because of = () =>
            {
                result = Service.AddGigsTo(gigs);
                target =
                    result.Where(x => x.Date.Day == 30 && x.Date.Month == 5 && x.Venue == "a venue").FirstOrDefault();
            };

        It has_not_doubled_up_on_30th_may = () => result.Where(x => x.Date.Day == 30 && x.Date.Month == 5 && x.Venue == "a venue").Count().ShouldEqual(1);
        It has_the_correct_gig = () => target.StartTime.ShouldEqual("15:00");
    }

    public class when_two_gigs_on_the_same_day_and_a_repeating_gig : with_raven_integration<RepeatingGigsProvider, Gig>
    {
        static List<Gig> gigs;
        static List<Gig> result;
        Establish context = () =>
        {
            session.Store(new RepeatingGig
            {
                SequenceStartFrom = new DateTime(2011, 05, 01),
                SequenceEndOn = new DateTime(2011, 06, 10),
                Venue = "a venue",
                StartTime = "21:00",
                GigTitle = "Jam Night",
                Mondays = true
            });
            session.SaveChanges();
            gigs = new List<Gig>
                             {
                                 new Gig
                                     {
                                         Date = new DateTime(2011, 05, 30),
                                         Venue = "a venue",
                                         StartTime = "15:00",
                                         BandNames = new[]
                                                         {
                                                             new BandName { Value = "a band"}
                                                        }
                                     },
                                 new Gig
                                     {
                                         Date = new DateTime(2011, 05, 30),
                                         Venue = "a venue",
                                         StartTime = "17:00",
                                         BandNames = new[]
                                                         {
                                                             new BandName { Value = "a band"}
                                                        }
                                     },
                                 new Gig
                                     {
                                         Date = new DateTime(2011, 05, 30),
                                         Venue = "a third venue",
                                         StartTime = "15:00",
                                         BandNames = new[]
                                                         {
                                                             new BandName { Value = "a band"}
                                                        }
                                     }
                             };
        };

        Because of = () =>
        {
            result = Service.AddGigsTo(gigs).Where(x => x.Date.Day == 30 && x.Date.Month == 5 && x.Venue == "a venue").ToList();
        };

        It has_returned_two_gigs = () => result.Count.ShouldEqual(2);
        It has_not_returned_the_repeating_one = () => result.Where(x => x.StartTime == "21:00").Count().ShouldEqual(0);
    }
}
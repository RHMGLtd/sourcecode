using System;
using System.Collections.Generic;
using blackpoolgigs.common.Contracts;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using blackpoolgigs.web.Controllers;
using blackpoolgigs.web.Models;
using blackpoolgigs.web.Models.ViewModels;
using coolbunny.tests.common.contexts;
using coolbunny.tests.common.shared.Behaviours;
using Machine.Specifications;

namespace blackpoolgigs.tests
{
    public class routing_to_today_special_route : with_controller<DayViewModel, DiaryController>
    {
        Because of = () => GET("today");
        Behaves_like<ok> get;
    }
    public class routing_to_today : with_controller<DayViewModel, DiaryController>
    {
        static DateTime input = DateTime.Now.Date;
        Because of = () => GET(input.ToString("dd/MMM/yyyy"));
        Behaves_like<ok> get;
    }
    public class routing_to_today_map : with_controller<DayMapViewModel, DiaryController>
    {
        static DateTime input = DateTime.Now.Date;
        Establish context = () =>
        {
            Stub<IGiveYouGigs>()
                .Setup(s => s.GetEntry(input))
                .Returns(new DiaryEntry
                {
                    Gigs = new List<Gig>
                                                    {
                                                        new Gig
                                                            {
                                                                Date = input,
                                                                BandNames = new[] {new BandName {Value = "A band"}},
                                                                Venue = "a venue"
                                                            }
                                                    }
                });
            Stub<IGiveYouVenues>()
                .Setup(s => s.GetMetadata(Moq.It.IsAny<VenueName>()))
                .Returns(new VenueMetadata());
        };
        Because of = () => GET(input.ToString("dd/MMM/yyyy") + "/map");
        Behaves_like<ok> get;
    }
    public class routing_to_one_years_time_no_gigs : with_controller<MonthViewModel, DiaryController>
    {
        static DateTime input = DateTime.Now.AddYears(1).Date;
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.GetEntry(input))
                                      .Returns(new DiaryEntry());
        Because of = () => GET(input.ToString("dd/MMM/yyyy"));
        Behaves_like<ok> get;
    }
    public class routing_to_one_years_time_with_gigs : with_controller<MonthViewModel, DiaryController>
    {
        static DateTime input = DateTime.Now.AddYears(1).Date;
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.GetEntry(input))
                                      .Returns(new DiaryEntry
                                                   {
                                                       Gigs = new List<Gig>
                                                                      {
                                                                          new Gig
                                                                              {
                                                                                  Date = input
                                                                              }
                                                                    }
                                                   });
        Because of = () => GET(input.ToString("dd/MMM/yyyy"));
        Behaves_like<ok> get;
    }
    public class routing_to_two_years_time_no_gigs : with_controller<MonthViewModel, DiaryController>
    {
        static DateTime input = DateTime.Now.AddYears(2).Date;
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.GetEntry(input))
                                      .Returns(new DiaryEntry());
        Because of = () => GET(input.ToString("dd/MMM/yyyy"));
        Behaves_like<ok> get;
    }
    public class routing_to_two_years_time_with_gigs : with_controller<MonthViewModel, DiaryController>
    {
        static DateTime input = DateTime.Now.AddYears(2).Date;
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.GetEntry(input))
                                      .Returns(new DiaryEntry
                                      {
                                          Gigs = new List<Gig>
                                                                      {
                                                                          new Gig
                                                                              {
                                                                                  Date = input
                                                                              }
                                                                    }
                                      });
        Because of = () => GET(input.ToString("dd/MMM/yyyy"));
        Behaves_like<ok> get;
    }
    public class routing_to_three_years_time_no_gigs : with_controller<DiaryNotFoundViewModel, DiaryController>
    {
        static DateTime input = DateTime.Now.AddYears(3).Date;
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.GetEntry(input))
                                      .Returns(new DiaryEntry());
        Because of = () => GET(input.ToString("dd/MMM/yyyy"));
        Behaves_like<not_found> get;
        It knows_that_you_are_asking_for_too_far_in_the_future =
            () => Resource.Reason.ShouldEqual(FourOhFourReason.TooFarInTheFuture);
    }
    public class routing_to_three_years_time_with_gigs : with_controller<DiaryNotFoundViewModel, DiaryController>
    {
        static DateTime input = DateTime.Now.AddYears(3).Date;
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.GetEntry(input))
                                      .Returns(new DiaryEntry
                                      {
                                          Gigs = new List<Gig>
                                                                      {
                                                                          new Gig
                                                                              {
                                                                                  Date = input
                                                                              }
                                                                    }
                                      });
        Because of = () => GET(input.ToString("dd/MMM/yyyy"));
        Behaves_like<not_found> get;
        It knows_that_you_are_asking_for_too_far_in_the_future =
            () => Resource.Reason.ShouldEqual(FourOhFourReason.TooFarInTheFuture);
    }
    public class routing_to_one_years_ago_no_gigs : with_controller<MonthViewModel, DiaryController>
    {
        static DateTime input = DateTime.Now.AddYears(-1).Date;
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.GetEntry(input))
                                      .Returns(new DiaryEntry());
        Because of = () => GET(input.ToString("dd/MMM/yyyy"));
        Behaves_like<ok> get;
    }
    public class routing_to_one_years_ago_with_gigs : with_controller<MonthViewModel, DiaryController>
    {
        static DateTime input = DateTime.Now.AddYears(-1).Date;
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.GetEntry(input))
                                      .Returns(new DiaryEntry
                                      {
                                          Gigs = new List<Gig>
                                                                      {
                                                                          new Gig
                                                                              {
                                                                                  Date = input
                                                                              }
                                                                    }
                                      });
        Because of = () => GET(input.ToString("dd/MMM/yyyy"));
        Behaves_like<ok> get;
    }
    public class routing_to_two_years_ago_no_gigs : with_controller<DiaryNotFoundViewModel, DiaryController>
    {
        static DateTime input = DateTime.Now.AddYears(-2).Date;
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.GetEntry(input))
                                      .Returns(new DiaryEntry());
        Because of = () => GET(input.ToString("dd/MMM/yyyy"));
        It knows_that_you_are_asking_for_nonexistant_archive =
            () => Resource.Reason.ShouldEqual(FourOhFourReason.HistoricalNoGigs);
    }
    public class routing_to_two_years_ago_with_gigs : with_controller<MonthViewModel, DiaryController>
    {
        static DateTime input = DateTime.Now.AddYears(-2).Date;
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.GetEntry(input))
                                      .Returns(new DiaryEntry
                                      {
                                          Gigs = new List<Gig>
                                                                      {
                                                                          new Gig
                                                                              {
                                                                                  Date = input
                                                                              }
                                                                    }
                                      });
        Because of = () => GET(input.ToString("dd/MMM/yyyy"));
        Behaves_like<ok> get;
    }

    public class routing_to_a_non_date : with_controller<DiaryNotFoundViewModel, DiaryController>
    {
        Because of = () => GET("45/blah/1");
        Behaves_like<not_found> get;
        It has_the_correct_fourohfour_reason = () => Resource.Reason.ShouldEqual(FourOhFourReason.DateNotValid);
    }
    public class routing_to_another_non_date : with_controller<DiaryNotFoundViewModel, DiaryController>
    {
        Because of = () => GET("15/Juf/2011");
        Behaves_like<not_found> get;
        It has_the_correct_fourohfour_reason = () => Resource.Reason.ShouldEqual(FourOhFourReason.DateNotValid);
    }
}

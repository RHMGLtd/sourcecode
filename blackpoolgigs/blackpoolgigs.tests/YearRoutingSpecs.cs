using System;
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
    public class routing_to_this_year : with_controller<YearViewModel, DiaryController>
    {
        static int year = DateTime.Now.Year;
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.MonthlyCounts(year))
                                      .Returns(new MonthlyGigCounts
                                      {
                                          Counts = new[]
                                                                    {
                                                                        new MonthCount
                                                                            {
                                                                                Count = 10
                                                                            }, 
                                                                    }
                                      });
        Because of = () => GET(year.ToString());
        Behaves_like<ok> get;
    }
    public class routing_to_one_years_time : with_controller<YearViewModel, DiaryController>
    {
        static int year = DateTime.Now.AddYears(1).Year;
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.MonthlyCounts(year))
                                      .Returns(new MonthlyGigCounts
                                      {
                                          Counts = new[]
                                                                    {
                                                                        new MonthCount
                                                                            {
                                                                                Count = 10
                                                                            }, 
                                                                    }
                                      });
        Because of = () => GET(year.ToString());
        Behaves_like<ok> get;
    }
    public class routing_to_two_years_time : with_controller<YearViewModel, DiaryController>
    {
        static int year = DateTime.Now.AddYears(2).Year;
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.MonthlyCounts(year))
                                      .Returns(new MonthlyGigCounts
                                      {
                                          Counts = new[]
                                                                    {
                                                                        new MonthCount
                                                                            {
                                                                                Count = 10
                                                                            }, 
                                                                    }
                                      });
        Because of = () => GET(year.ToString());
        Behaves_like<ok> get;
    }
    public class routing_to_three_years_time : with_controller<DiaryNotFoundViewModel, DiaryController>
    {
        static int year = DateTime.Now.AddYears(3).Year;
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.MonthlyCounts(year))
                                      .Returns(new MonthlyGigCounts
                                      {
                                          Counts = new[]
                                                                    {
                                                                        new MonthCount
                                                                            {
                                                                                Count = 10
                                                                            }, 
                                                                    }
                                      });
        Because of = () => GET(year.ToString());
        Behaves_like<not_found> get;

        It knows_that_you_are_asking_for_too_far_in_the_future =
            () => Resource.Reason.ShouldEqual(FourOhFourReason.TooFarInTheFuture);
    }
    public class routing_to_year_in_two_years_ago_with_gigs : with_controller<YearViewModel, DiaryController>
    {
        static int year = DateTime.Now.AddYears(-2).Year;
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.MonthlyCounts(year))
                                      .Returns(new MonthlyGigCounts
                                      {
                                          Counts = new[]
                                                                    {
                                                                        new MonthCount
                                                                            {
                                                                                Count = 10
                                                                            }, 
                                                                    }
                                      });
        Because of = () => GET(year.ToString());
        Behaves_like<ok> get;
    }
    public class routing_to_one_year_ago_no_gigs : with_controller<YearViewModel, DiaryController>
    {
        static int year = DateTime.Now.AddYears(-1).Year;
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.MonthlyCounts(year))
                                      .Returns(new MonthlyGigCounts());
        Because of = () => GET(year.ToString());
        Behaves_like<ok> get;
    }
    public class routing_to_year_in_two_years_ago_no_gigs : with_controller<DiaryNotFoundViewModel, DiaryController>
    {
        static int year = DateTime.Now.AddYears(-2).Year;
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.MonthlyCounts(year))
                                      .Returns(new MonthlyGigCounts());
        Because of = () => GET(year.ToString());
        Behaves_like<not_found> get;
        It knows_that_you_are_asking_for_nonexistant_archive =
            () => Resource.Reason.ShouldEqual(FourOhFourReason.HistoricalNoGigs);
    }
    public class routing_to_an_invalid_year : with_controller<DiaryNotFoundViewModel, DiaryController>
    {
        Because of = () => GET("-100");
        Behaves_like<not_found> get;
        It has_the_correct_failure_reason = () => Resource.Reason.ShouldEqual(FourOhFourReason.DateNotValid);
    }
    public class routing_to_a_totally_invalid_year : with_controller<DiaryNotFoundViewModel, DiaryController>
    {
        Because of = () => GET("blahhhh");
        Behaves_like<not_found> get;
        It has_the_correct_failure_reason = () => Resource.Reason.ShouldEqual(FourOhFourReason.DateNotValid);
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class routing_to_diary : with_controller<MonthViewModel, DiaryController>
    {
        Because of = () => GET("diary");
        Behaves_like<ok> get;
    }
    public class routing_to_a_month_in_this_year : with_controller<MonthViewModel, DiaryController>
    {
        static int year = DateTime.Now.Year;
        Because of = () => GET("april/" + year);
        Behaves_like<ok> get;
    }
    public class routing_to_month_one_years_time_no_gigs : with_controller<MonthViewModel, DiaryController>
    {
        static DateTime input = DateTime.Now.AddYears(1).Date;
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.Get(input))
                                      .Returns(new Diary(input));
        Because of = () => GET("april/" + input.Year);
        Behaves_like<ok> get;
    }
    public class routing_to_month_one_years_time_with_gigs : with_controller<MonthViewModel, DiaryController>
    {
        static DateTime input = DateTime.Now.AddYears(1).Date;
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.Get(input))
                                      .Returns(new Diary(input).AddGigs(new List<Gig>
                                                                                {
                                                                                    new Gig
                                                                                        {
                                                                                            Date = new DateTime(input.Year, input.Month, input.Day)
                                                                                        }
                                                                                },false
                                                       )
                                                 );
        Because of = () => GET("april/" + input.Year);
        Behaves_like<ok> get;
    }
    public class routing_to_month_two_years_time_no_gigs : with_controller<MonthViewModel, DiaryController>
    {
        static DateTime input = DateTime.Now.AddYears(2).Date;
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.Get(input))
                                      .Returns(new Diary(input));
        Because of = () => GET("april/" + input.Year);
        Behaves_like<ok> get;
    }
    public class routing_to_month_two_years_time_with_gigs : with_controller<MonthViewModel, DiaryController>
    {
        static DateTime input = DateTime.Now.AddYears(2).Date;
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.Get(input))
                                      .Returns(new Diary(input).AddGigs(new List<Gig>
                                                                                {
                                                                                    new Gig
                                                                                        {
                                                                                            Date = new DateTime(input.Year, input.Month, input.Day)
                                                                                        }
                                                                                }, false
                                                       )
                                                 );
        Because of = () => GET("april/" + input.Year);
        Behaves_like<ok> get;
    }
    public class routing_to_month_three_years_time_no_gigs : with_controller<DiaryNotFoundViewModel, DiaryController>
    {
        static DateTime input = DateTime.Now.AddYears(3).Date;
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.Get(input))
                                      .Returns(new Diary(input));
        Because of = () => GET("april/" + input.Year);
        Behaves_like<not_found> get;
        It knows_that_you_are_asking_for_too_far_in_the_future =
            () => Resource.Reason.ShouldEqual(FourOhFourReason.TooFarInTheFuture);
    }
    public class routing_to_month_three_years_time_with_gigs : with_controller<DiaryNotFoundViewModel, DiaryController>
    {
        static DateTime input = DateTime.Now.AddYears(3).Date;
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.Get(input))
                                      .Returns(new Diary(input).AddGigs(new List<Gig>
                                                                                {
                                                                                    new Gig
                                                                                        {
                                                                                            Date = new DateTime(input.Year, input.Month, input.Day)
                                                                                        }
                                                                                }, false
                                                       )
                                                 );
        Because of = () => GET("april/" + input.Year);
        Behaves_like<not_found> get;
        It knows_that_you_are_asking_for_too_far_in_the_future = () => Resource.Reason.ShouldEqual(FourOhFourReason.TooFarInTheFuture);
    }
    public class routing_to_month_one_years_ago_no_gigs : with_controller<MonthViewModel, DiaryController>
    {
        static DateTime input = DateTime.Now.AddYears(-1).Date;
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.Get(input))
                                      .Returns(new Diary(input));
        Because of = () => GET("april/" + input.Year);
        Behaves_like<ok> get;
    }
    public class routing_to_month_one_years_ago_with_gigs : with_controller<MonthViewModel, DiaryController>
    {
        static DateTime input = DateTime.Now.AddYears(-1).Date;
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.Get(input))
                                      .Returns(new Diary(input).AddGigs(new List<Gig>
                                                                                {
                                                                                    new Gig
                                                                                        {
                                                                                            Date = new DateTime(input.Year, input.Month, input.Day)
                                                                                        }
                                                                                }, false
                                                       )
                                                 );
        Because of = () => GET("april/" + input.Year);
        Behaves_like<ok> get;
    }
    public class routing_to_month_two_years_ago_no_gigs : with_controller<DiaryNotFoundViewModel, DiaryController>
    {
        static DateTime input = new DateTime(DateTime.Now.AddYears(-2).Year, DateTime.Now.Month, 1);
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.Get(input))
                                      .Returns(new Diary(input));
        Because of = () => GET(new DateTimeFormatInfo().GetMonthName(input.Month) + "/" + input.Year);
        Behaves_like<not_found> get;
        It knows_that_you_are_asking_for_nonexistant_archive =
            () => Resource.Reason.ShouldEqual(FourOhFourReason.HistoricalNoGigs);
    }
    public class routing_to_month_two_years_ago_with_gigs : with_controller<MonthViewModel, DiaryController>
    {
        static DateTime input = new DateTime(DateTime.Now.AddYears(-2).Year, DateTime.Now.Month, 1);
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.Get(input))
                                      .Returns(new Diary(input).AddGigs(new List<Gig>
                                                                                {
                                                                                    new Gig
                                                                                        {
                                                                                            Date = new DateTime(input.Year, input.Month, input.Day)
                                                                                        }
                                                                                }, false
                                                       )
                                                 );
        Because of = () => GET(new DateTimeFormatInfo().GetMonthName(input.Month) + "/" + input.Year);
        Behaves_like<ok> get;
    }
    public class routing_to_a_badly_formed_month : with_controller<DiaryNotFoundViewModel, DiaryController>
    {
        Because of = () => GET("bloa/2342");
        Behaves_like<not_found> get;
        It has_the_correct_failure_reason = () => Resource.Reason.ShouldEqual(FourOhFourReason.DateNotValid);
    }
}

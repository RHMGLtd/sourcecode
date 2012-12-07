using System;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using blackpoolgigs.common.ViewModels;
using blackpoolgigs.web.Controllers;
using blackpoolgigs.web.Models;
using blackpoolgigs.web.Models.ViewModels;
using blackpoolgigs.web.Services.Interfaces;
using coolbunny.tests.common.contexts;
using coolbunny.tests.common.shared.Behaviours;
using Machine.Specifications;

namespace blackpoolgigs.tests
{
    public class routing_to_home : with_controller<HomeViewModel, HomeController>
    {
        Establish context = () => Stub<IGiveYouGigs>()
                                      .Setup(s => s.Counts(Moq.It.IsAny<DateTime>()))
                                      .Returns(new Counts());
        Because of = () => GET("");
        Behaves_like<ok> get;
    }
    public class routing_to_contact : with_controller<ContactViewModel, ContactController>
    {
        Because of = () => GET("/Contact");
        Behaves_like<ok> get;
    }
    public class routing_to_band_looking_for_a_gig : with_controller<BandLookingForAGigViewModel, ContentController>
    {
        Because of = () => GET("/BandLookingForAGig");
        Behaves_like<ok> get;
    }
    public class routing_to_stolen_gear_list : with_controller<StolenGearListViewModel, ContentController>
    {
        Because of = () => GET("StolenGear");
        Behaves_like<ok> get;
    }
    public class routing_to_contact_form_post : with_controller<ContactViewModel, ContactController>
    {
        Because of = () => POST("/Contact", new ContactViewModel()
                                                {
                                                    whatAbout = "About something",
                                                    yourEmail = "roo@rhmg.co.uk",
                                                    yourName = "me"
                                                });
        Behaves_like<ok> ok;
    }
    public class routing_to_thankyou : with_controller<ThanksViewModel, ContactController>
    {
        Establish context = () => Stub<ISayThanks>()
                                      .Setup(t => t.Gimme())
                                      .Returns(new ThanksImageInfo());
        Because of = () => GET("thanks");
        Behaves_like<ok> get;
    }

    public class routing_to_about : with_controller<AboutViewModel, ContentController>
    {
        Because of = () => GET("about");
        Behaves_like<ok> get;
    }

    public class routing_to_a_totally_non_standard_route : with_controller<FourOhFourViewModel, ErrorController>
    {
        Because of = () => GET("blahhhh/dshfdsds/sdfsdfsdf/sdfsdfsdf");
        Behaves_like<not_found> get;
        It has_the_correct_failure_reason = () => Resource.Reason.ShouldEqual(FourOhFourReason.InvalidRouteRequested);
    }
    public class routing_to_another_totally_non_standard_route : with_controller<DiaryNotFoundViewModel, DiaryController>
    {
        Because of = () => GET("balh.asdfrs./sdfsdf");
        Behaves_like<not_found> get;
        It has_the_correct_failure_reason = () => Resource.Reason.ShouldEqual(FourOhFourReason.DateNotValid);
    }
}
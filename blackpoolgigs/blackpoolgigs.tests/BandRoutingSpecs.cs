using blackpoolgigs.web.Controllers;
using blackpoolgigs.web.Models;
using blackpoolgigs.web.Models.ViewModels;
using blackpoolgigs.web.Services.Interfaces;
using coolbunny.tests.common.contexts;
using coolbunny.tests.common.shared.Behaviours;
using Machine.Specifications;

namespace blackpoolgigs.tests
{
    public class when_routing_to_band_list : with_controller<BandsViewModel,BandController>
    {
        Because of = () => GET("bands");
        Behaves_like<ok> get;
    }

    public class when_routing_to_a_band : with_controller<BandViewModel, BandController>
    {
        Because of = () => GET("bands/a band name");
        Behaves_like<ok> get;
    }

    public class when_getting_to_claim_a_band : with_controller<ClaimBandViewModel, BandController>
    {
        Because of = () => GET("bands/a band name/claim");
        Behaves_like<ok> get;
    }

    public class when_getting_claim_thanks : with_controller<ClaimThanksViewModel, BandController>
    {
        Establish context = () => Stub<ISayThanks>()
                                 .Setup(t => t.Gimme())
                                 .Returns(new ThanksImageInfo());

        Because of = () => GET("bands/a band name/thanks");
        Behaves_like<ok> get;
    }

    public class when_posting_a_claim_to_a_band : with_controller<ClaimBandViewModel, BandController>
    {
        Because of = () => POST("bands/a band name/claim", new ClaimBandViewModel());
        Behaves_like<ok> post;
    }
}
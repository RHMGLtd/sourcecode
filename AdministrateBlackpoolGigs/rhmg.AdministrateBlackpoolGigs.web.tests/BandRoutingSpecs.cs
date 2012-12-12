using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using coolbunny.tests.common.contexts;
using coolbunny.tests.common.shared.Behaviours;
using Machine.Specifications;
using rhmg.AdministrateBlackpoolGigs.web.Controllers;
using rhmg.AdministrateBlackpoolGigs.web.Models.ViewModels;

namespace rhmg.AdministrateBlackpoolGigs.web.tests
{
    public class getting_a_band_list : with_controller<BandsViewModel, BandController>
    {
        Because of = () => GET("bands");
        Behaves_like<ok> get;
    }

    public class getting_a_band : with_controller<BandViewModel, BandController>
    {
        Establish context = () => Stub<IWillProvideBands>()
                                      .Setup(b => b.GetMetadata(Moq.It.IsAny<BandName>()))
                                      .Returns(new BandMetadata());
        Because of = () => GET("bands/band name");
        Behaves_like<ok> get;
    }

    public class posting_a_band : with_controller<BandViewModel, BandController>
    {
        Establish context = () => Stub<ISaveBands>()
                                      .Setup(b => b.Save(Moq.It.IsAny<BandMetadata>()))
                                      .Returns((BandMetadata m) => m);
        Because of = () => POST("bands/band name", new BandViewModel { BandName = "a band" });
        Behaves_like<ok> post;
    }
}
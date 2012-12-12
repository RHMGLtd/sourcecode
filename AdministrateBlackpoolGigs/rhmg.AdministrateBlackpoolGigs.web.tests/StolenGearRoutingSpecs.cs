using System.Web;
using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using blackpoolgigs.common.ViewModels;
using coolbunny.tests.common.contexts;
using coolbunny.tests.common.shared.Behaviours;
using Machine.Specifications;
using rhmg.AdministrateBlackpoolGigs.web.Controllers;
using rhmg.AdministrateBlackpoolGigs.web.Models.ViewModels;

namespace rhmg.AdministrateBlackpoolGigs.web.tests
{
    public class StolenGearRoutingSpecs
    {
        public class when_routing_to_list_of_stolen_gear : with_controller<StolenGearListViewModel, StolenGearController>
        {
            Because of = () => GET("stolengearlist");
            Behaves_like<ok> get;
        }
        public class when_getting_to_add_a_new_stolen_gear : with_controller<StolenGearViewModel, StolenGearController>
        {
            Because of = () => GET("stolengear");
            Behaves_like<ok> get;
        }
        public class when_posting_a_new_stolen_gear : with_controller<StolenGearViewModel, StolenGearController>
        {
            Establish context = () => Stub<IProcessStolenGear>()
                                          .Setup(s => s.Save(Moq.It.IsAny<StolenGear>(), Moq.It.IsAny<HttpPostedFileBase>(), Moq.It.IsAny<HttpPostedFileBase>(), Moq.It.IsAny<HttpPostedFileBase>(), Moq.It.IsAny<HttpPostedFileBase>(), Moq.It.IsAny<HttpPostedFileBase>()))
                                          .Returns(new StolenGear { Id = "stolengear/1" });
            Because of = () => POST("stolengear", new StolenGearViewModel());
            Behaves_like<ok> post;
        }
        public class when_getting_a_stolen_gear : with_controller<StolenGearViewModel, StolenGearController>
        {
            Establish context = () => Stub<IDealInStolenGear>()
                                          .Setup(s => s.Get(Moq.It.IsAny<string>()))
                                          .Returns(new StolenGear());
            Because of = () => GET("stolengear/1");
            Behaves_like<ok> get;
        }
        public class when_updating_a_stolen_gear : with_controller<StolenGearViewModel, StolenGearController>
        {
            Establish context = () => Stub<IProcessStolenGear>()
                                          .Setup(s => s.Save(Moq.It.IsAny<StolenGear>(), Moq.It.IsAny<HttpPostedFileBase>(), Moq.It.IsAny<HttpPostedFileBase>(), Moq.It.IsAny<HttpPostedFileBase>(), Moq.It.IsAny<HttpPostedFileBase>(), Moq.It.IsAny<HttpPostedFileBase>()))
                                          .Returns((StolenGear s, HttpPostedFileBase[] f) => s);
            Because of = () => POST("stolengear/1", new StolenGearViewModel());
            Behaves_like<ok> post;
        }
        public class when_deleting_a_stolen_gear : with_controller<bool, StolenGearController>
        {
            Because of = () => DELETE("stolengear/1");
            Behaves_like<ok> delete;
        }
    }
}
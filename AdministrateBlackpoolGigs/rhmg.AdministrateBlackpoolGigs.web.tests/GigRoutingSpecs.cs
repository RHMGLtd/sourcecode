using blackpoolgigs.common.Interfaces;
using blackpoolgigs.common.Models;
using coolbunny.tests.common.contexts;
using coolbunny.tests.common.shared.Behaviours;
using Machine.Specifications;
using rhmg.AdministrateBlackpoolGigs.web.Controllers;
using rhmg.AdministrateBlackpoolGigs.web.Models.ViewModels;

namespace rhmg.AdministrateBlackpoolGigs.web.tests
{
    public class GigRoutingSpecs
    {
        public class when_routing_to_add : with_controller<ManageViewModel, ManageGigsController>
        {
            Because of = () => GET("/Add");
            Behaves_like<ok> get;
            It has_the_correct_resource = () => Resource.ShouldNotBeNull();
        }
        public class when_posting_to_add : with_controller<ManageViewModel, ManageGigsController>
        {
            Establish context = () => Stub<IStoreGigs>()
                                          .Setup(s => s.Save(Moq.It.IsAny<Gig>()))
                                          .Returns(new Gig { Id = "gig/1" });
            Because of = () => POST("/Add", new ManageViewModel());
            Behaves_like<see_other> post;
        }
        public class posting_record_after_editing : with_controller<ManageViewModel, ManageGigsController>
        {
            Establish context = () => Stub<IStoreGigs>()
                                          .Setup(s => s.Save(Moq.It.IsAny<Gig>()))
                                          .Returns(new Gig());
            Because of = () => POST("gig/1", new ManageViewModel());
            Behaves_like<ok> post;
        }
        public class getting_record_for_editing : with_controller<ManageViewModel, ManageGigsController>
        {
            Establish context = () => Stub<IGiveYouGigs>()
                                          .Setup(s => s.Get(Moq.It.IsAny<string>()))
                                          .Returns(new Gig());
            Because of = () => GET("gig/1");
            Behaves_like<ok> get;
        }
        public class when_routing_to_add_repeating_gig : with_controller<RepeatingGigViewModel, ManageGigsController>
        {
            Because of = () => GET("RepeatingGig/Add");
            Behaves_like<ok> get;
            It has_the_correct_resource = () => Resource.ShouldNotBeNull();
        }
        public class when_posting_a_new_repeating_gig : with_controller<RepeatingGigViewModel, ManageGigsController>
        {
            Establish context = () => Stub<IHandleRepeatingGigs>()
                                              .Setup(s => s.SaveRepeating(Moq.It.IsAny<RepeatingGig>()))
                                              .Returns(new RepeatingGig { Id = "gig/1" });
            Because of = () => POST("RepeatingGig/Add", new RepeatingGigViewModel());
            Behaves_like<see_other> post;
        }
        public class when_getting_a_repeating_gig : with_controller<RepeatingGigViewModel, ManageGigsController>
        {
            Establish context = () => Stub<IHandleRepeatingGigs>()
                                          .Setup(s => s.GetRepeating(Moq.It.IsAny<string>()))
                                          .Returns(new RepeatingGig());
            Because of = () => GET("RepeatingGig/1");
            Behaves_like<ok> get;
        }
        public class posting_repeating_gig_after_editing : with_controller<RepeatingGigViewModel, ManageGigsController>
        {
            Establish context = () => Stub<IHandleRepeatingGigs>()
                                          .Setup(s => s.SaveRepeating(Moq.It.IsAny<RepeatingGig>()))
                                          .Returns(new RepeatingGig());
            Because of = () => POST("repeatinggig/1", new RepeatingGigViewModel());
            Behaves_like<ok> post;
        }
        public class when_routing_to_list_of_repeating_gigs : with_controller<RepeatingGigsViewModel, ManageGigsController>
        {
            Because of = () => GET("repeatinggigs");
            Behaves_like<ok> get;
        }
        public class when_getting_the_list : with_controller<ListViewModel, ManageGigsController>
        {
            Because of = () => GET("list");
            Behaves_like<ok> get;
        }
        public class deleting_a_gig : with_controller<bool, ManageGigsController>
        {
            Because of = () => DELETE("gig/1");
            Behaves_like<ok> delete;
        }
        public class deleting_a_repeating_gig : with_controller<bool, ManageGigsController>
        {
            Because of = () => DELETE("repeatinggig/1");
            Behaves_like<ok> delete;
        }
    }
}
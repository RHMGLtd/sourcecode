using coolbunny.tests.common.contexts;
using coolbunny.tests.common.shared.Behaviours;
using Machine.Specifications;
using rhmg.AdministrateBlackpoolGigs.web.Controllers;
using rhmg.AdministrateBlackpoolGigs.web.Models.ViewModels;

namespace rhmg.AdministrateBlackpoolGigs.web.tests
{
    public class when_routing_to_home : with_controller<HomeViewModel, HomeController>
    {
        Because of = () => GET("");
        Behaves_like<ok> get;
        It has_the_correct_resource = () => Resource.ShouldNotBeNull();
    }
}
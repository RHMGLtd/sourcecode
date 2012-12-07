using coolbunny.tests.common.contexts;
using coolbunny.tests.common.shared.Behaviours;
using Machine.Specifications;
using rhmgOnline.Controllers;
using rhmgOnline.Models.ViewModels;

namespace rhmgOnline.tests
{
    public class RoutingSpecs
    {
        public class routing_to_home : with_controller<HomeViewModel, HomeController>
        {
            Because of = () => GET("");
            Behaves_like<ok> get;
        }
    }
}
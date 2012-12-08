using System.Web.Mvc;
using Machine.Specifications;

namespace coolbunny.tests.common.shared.Behaviours
{
    [Behaviors]
    public class not_authenticated
    {
        protected static ActionResult result;

        It Should_have_a_status_code_of_OK = () => result.ShouldBeOfType(typeof (ActionResult));
    }
}
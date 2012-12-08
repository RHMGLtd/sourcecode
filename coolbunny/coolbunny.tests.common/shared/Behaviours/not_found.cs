using Machine.Specifications;
using Snooze;

namespace coolbunny.tests.common.shared.Behaviours
{
    [Behaviors]
    public class not_found
    {
        protected static ResourceResult result;

        It Should_have_a_status_code_of_NOT_FOUND = () => result.StatusCode.ShouldEqual(404);
    }
}
using Machine.Specifications;
using Snooze;

namespace coolbunny.tests.common.shared.Behaviours
{
    [Behaviors]
    public class badRequest
    {
        protected static ResourceResult result;

        It Should_have_a_status_code_of_bad_request = () => result.StatusCode.ShouldEqual(400);
    }
}
using Machine.Specifications;
using Snooze;

namespace coolbunny.tests.common.shared.Behaviours
{
    [Behaviors]
    public class forbidden
    {
        protected static ResourceResult result;

        It Should_have_a_status_code_of_Forbidden = () => result.StatusCode.ShouldEqual(403);
    }
}
using Machine.Specifications;
using Snooze;

namespace coolbunny.tests.common.shared.Behaviours
{
    [Behaviors]
    public class ok
    {
        protected static ResourceResult result;

        It Should_have_a_status_code_of_OK = () => result.StatusCode.ShouldEqual(200);
    }
}
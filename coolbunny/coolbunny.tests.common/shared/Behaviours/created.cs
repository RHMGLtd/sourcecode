using Machine.Specifications;
using Snooze;

namespace coolbunny.tests.common.shared.Behaviours
{
    [Behaviors]
    public class created
    {
        protected static ResourceResult result;

        It Should_have_a_status_code_of_CREATED = () => result.StatusCode.ShouldEqual(201);
    }
}
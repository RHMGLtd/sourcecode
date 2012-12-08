using Machine.Specifications;
using Snooze;

namespace coolbunny.tests.common.shared.Behaviours
{
    [Behaviors]
    public class see_other
    {
        protected static ResourceResult result;

        It Should_have_a_status_code_of_SEE_OTHER = () => result.StatusCode.ShouldEqual(303);
    }
}
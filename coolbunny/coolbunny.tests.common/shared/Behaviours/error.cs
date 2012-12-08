﻿using Machine.Specifications;
using Snooze;

namespace coolbunny.tests.common.shared.Behaviours
{
    [Behaviors]
    public class Error
    {
        protected static ResourceResult result;

        It Should_have_a_status_code_of_500 = () => result.StatusCode.ShouldEqual(500);
    }
}

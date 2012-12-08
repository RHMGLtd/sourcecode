using Machine.Specifications;

namespace coolbunny.tests.common.contexts
{

    public class with_service<TUnderTest> : with_auto_mocking<TUnderTest> where TUnderTest : class
    {
        public class with_automocking : with_auto_mocking<TUnderTest> { }
        protected static TUnderTest Service
        {
            get { return autoMocker.ClassUnderTest; }
        }
        Establish context = with_automocking.Make;
    }
}
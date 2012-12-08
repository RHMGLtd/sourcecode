using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using Moq;
using StructureMap.AutoMocking;

namespace coolbunny.tests.common.contexts
{
    public class with_auto_mocking<TUnderTest> where TUnderTest : class
    {
        protected static MoqAutoMocker<TUnderTest> autoMocker;

        Establish automocking = () => Make();

        public static void Make()
        {
            autoMocker = new MoqAutoMocker<TUnderTest>();
        }

        protected static void Inject<T>(T instance)
        {
            autoMocker.Inject(typeof(T), instance);
        }


        protected static void InjectArray<T>(T[] objects)
        {
            autoMocker.InjectArray(objects);
        }

        protected static Mock<TInterface> Stub<TInterface>() where TInterface : class
        {
            var mocked = autoMocker.Get<TInterface>();
            return Mock.Get(mocked);
        }

        protected static bool AreEquivalent<T>(IEnumerable<T> lhs, IEnumerable<T> rhs)
        {
            if (lhs.Count() != rhs.Count())
            {
                FormatEnumerables(lhs, rhs);
                return false;
            }

            for (var i = 0; i != lhs.Count(); i++)
            {
                if (!rhs.ElementAt(i).Equals(lhs.ElementAt(i)))
                {
                    FormatEnumerables(lhs, rhs);
                    return false;
                }
            }

            return true;
        }

        protected static string FormatEnumerables<T>(IEnumerable<T> lhs, IEnumerable<T> rhs)
        {
            return string.Format("Have {0} but expected {1}",
                                 string.Join(",", lhs.Select(s => s == null ? "null" : s.ToString()).ToArray()),
                                 string.Join(",", rhs.Select(s => s == null ? "null" : s.ToString()).ToArray()));
        }
    }
}

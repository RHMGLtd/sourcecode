// Type: Machine.Specifications.ShouldExtensionMethods
// Assembly: Machine.Specifications, Version=0.3.0.0, Culture=neutral, PublicKeyToken=5c474de7a495cff1
// Assembly location: C:\business websites\lib\mspec\Machine.Specifications.dll

using Machine.Specifications.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Machine.Specifications
{
    public static class ShouldExtensionMethods
    {
        [AssertionMethod]
        public static void ShouldBeFalse([AssertionCondition(AssertionConditionType.IS_FALSE)] this bool condition);

        [AssertionMethod]
        public static void ShouldBeTrue([AssertionCondition(AssertionConditionType.IS_TRUE)] this bool condition);

        public static T ShouldEqual<T>(this T actual, T expected);
        public static object ShouldNotEqual<T>(this T actual, T expected);

        [AssertionMethod]
        public static void ShouldBeNull([AssertionCondition(AssertionConditionType.IS_NULL)] this object anObject);

        [AssertionMethod]
        public static void ShouldNotBeNull([AssertionCondition(AssertionConditionType.IS_NOT_NULL)] this object anObject);

        public static object ShouldBeTheSameAs(this object actual, object expected);
        public static object ShouldNotBeTheSameAs(this object actual, object expected);
        public static void ShouldBeOfType(this object actual, Type expected);
        public static void ShouldBeOfType<T>(this object actual);
        public static void ShouldBe(this object actual, Type expected);
        public static void ShouldNotBeOfType(this object actual, Type expected);
        public static void ShouldEachConformTo<T>(this IEnumerable<T> list, Func<T, bool> condition);
        public static void ShouldContain(this IEnumerable list, params object[] items);
        public static void ShouldContain<T>(this IEnumerable<T> list, params T[] items);
        public static void ShouldNotContain(this IEnumerable list, params object[] items);
        public static void ShouldNotContain<T>(this IEnumerable<T> list, params T[] items);
        public static IComparable ShouldBeGreaterThan(this IComparable arg1, IComparable arg2);
        public static IComparable ShouldBeGreaterThanOrEqualTo(this IComparable arg1, IComparable arg2);
        public static IComparable ShouldBeLessThan(this IComparable arg1, IComparable arg2);
        public static IComparable ShouldBeLessThanOrEqualTo(this IComparable arg1, IComparable arg2);
        public static void ShouldBeCloseTo(this float actual, float expected);
        public static void ShouldBeCloseTo(this float actual, float expected, float tolerance);
        public static void ShouldBeCloseTo(this double actual, double expected);
        public static void ShouldBeCloseTo(this double actual, double expected, double tolerance);
        public static void ShouldBeCloseTo(this TimeSpan actual, TimeSpan expected, TimeSpan tolerance);
        public static void ShouldBeEmpty(this IEnumerable collection);
        public static void ShouldBeEmpty(this string aString);
        public static void ShouldNotBeEmpty(this IEnumerable collection);
        public static void ShouldNotBeEmpty(this string aString);
        public static void ShouldMatch(this string actual, string pattern);
        public static void ShouldMatch(this string actual, Regex pattern);
        public static void ShouldContain(this string actual, string expected);
        public static void ShouldNotContain(this string actual, string notExpected);
        public static string ShouldBeEqualIgnoringCase(this string actual, string expected);
        public static void ShouldStartWith(this string actual, string expected);
        public static void ShouldEndWith(this string actual, string expected);

        public static void ShouldBeSurroundedWith(this string actual, string expectedStartDelimiter,
                                                  string expectedEndDelimiter);

        public static void ShouldBeSurroundedWith(this string actual, string expectedDelimiter);
        public static void ShouldContainErrorMessage(this Exception exception, string expected);
        public static void ShouldContainOnly<T>(this IEnumerable<T> list, params T[] items);
        public static void ShouldContainOnly<T>(this IEnumerable<T> list, IEnumerable<T> items);
        public static Exception ShouldBeThrownBy(this Type exceptionType, Action method);
    }
}

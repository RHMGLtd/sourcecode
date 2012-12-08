using coolbunny.common.Extensions;
using Machine.Specifications;

namespace coolbunny.tests.common
{
    public class between
    {
        It when_number_is_low_bound = () => 0.Between(0, 23).ShouldBeTrue();
        It when_number_is_between = () => 10.Between(0, 23).ShouldBeTrue();
        It when_number_is_upper_bound = () => 23.Between(0, 23).ShouldBeTrue();
    }
    public class getting_a_random_number
    {
        static int val1;
        static int val2;
        Establish context = () =>
            {
                val1 = 10.RandomFromZeroTo();
                val2 = 10.RandomFromZeroTo();
                for (int i = 0; i < 1000; i++)
                {
                    if (val1 != val2)
                        break;
                    val2 = 10.RandomFromZeroTo();
                }
            };

        It has_given_different_numbers = () => val1.ShouldNotEqual(val2);
    }
    public class dateifying_ints
    {
        It does_the_1st = () => 1.Dateify().ShouldEqual("1st");
        It does_the_2nd = () => 2.Dateify().ShouldEqual("2nd");
        It does_the_3rd = () => 3.Dateify().ShouldEqual("3rd");
        It does_the_21st = () => 21.Dateify().ShouldEqual("21st");
        It does_the_22nd = () => 22.Dateify().ShouldEqual("22nd");
        It does_the_23rd = () => 23.Dateify().ShouldEqual("23rd");
        It does_the_31st = () => 31.Dateify().ShouldEqual("31st");
        It does_the_4th = () => 4.Dateify().ShouldEqual("4th");
    }
}
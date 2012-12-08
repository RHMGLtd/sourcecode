using System;
using coolbunny.common.Extensions;
using Machine.Specifications;

namespace coolbunny.tests.common
{
    public class getting_the_monday_of_a_week_when_current_day_is_monday
    {
        static DateTime result;
        static DateTime source = new DateTime(2011, 04, 11);
        Because of = () => result = source.GetMondayOfWeek();
        It has_returned_a_monday = () => result.DayOfWeek.ShouldEqual(DayOfWeek.Monday);
        It has_returned_the_date_passed_in = () => result.ShouldEqual(source);
    }
    public class getting_the_monday_of_a_week_when_the_day_is_not_a_monday
    {
        static DateTime result;
        static DateTime source = new DateTime(2011, 04, 10);
        Because of = () => result = source.GetMondayOfWeek();
        It has_returned_a_monday = () => result.DayOfWeek.ShouldEqual(DayOfWeek.Monday);
        It has_returned_the_date_passed_in = () => result.ShouldEqual(new DateTime(2011, 04, 4));
    }

    public class getting_the_sunday_of_a_week_when_current_day_is_sunday
    {
        static DateTime result;
        static DateTime source = new DateTime(2011, 04, 10);
        Because of = () => result = source.GetSundayOfWeek();
        It has_returned_a_monday = () => result.DayOfWeek.ShouldEqual(DayOfWeek.Sunday);
        It has_returned_the_date_passed_in = () => result.ShouldEqual(source);
    }
    public class getting_the_sunday_of_a_week_when_the_day_is_not_a_sunday
    {
        static DateTime result;
        static DateTime source = new DateTime(2011, 04, 4);
        Because of = () => result = source.GetSundayOfWeek();
        It has_returned_a_monday = () => result.DayOfWeek.ShouldEqual(DayOfWeek.Sunday);
        It has_returned_the_date_passed_in = () => result.ShouldEqual(new DateTime(2011, 04, 10));
    }
}

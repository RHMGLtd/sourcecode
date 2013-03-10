using System;
using System.Collections.Generic;
using Machine.Specifications;
using rhmg.StudioDiary.Extensions;

namespace rhmg.StudioDiary.Tests
{
    public class when_to_separated_and_string_list_which_is_empty
    {
        static List<string> test;
        Because of = () => test = new List<string>();
        It returns_string_empty = () => test.ToStringSeparatedWithAnd().ShouldEqual(string.Empty);
    }
    public class when_to_separated_and_string_list_which_has_one_value
    {
        static List<string> test;
        Because of = () => test = new List<string>
                                      {
                                          "one"
                                      };
        It returns_string_empty = () => test.ToStringSeparatedWithAnd().ShouldEqual("one");
    }
    public class when_to_separated_and_string_list_which_has_two_values
    {
        static List<string> test;
        Because of = () => test = new List<string>
                                      {
                                          "one",
                                          "two"
                                      };
        It returns_string_empty = () => test.ToStringSeparatedWithAnd().ShouldEqual("one and two");
    }
    public class when_to_separated_and_string_list_which_has_three_values
    {
        static List<string> test;
        Because of = () => test = new List<string>
                                      {
                                          "one",
                                          "two",
                                          "three"
                                      };
        It returns_string_empty = () => test.ToStringSeparatedWithAnd().ShouldEqual("one, two and three");
    }

    public class when_calculating_monday_date
    {
        static readonly DateTime mondayDate = new DateTime(2012, 12, 03);

        It does_the_whole_week_correctly = () =>
        {
            var startDate = new DateTime(2012, 12, 03);

            for (var i = 1; i < 7; i++)
            {
                startDate.MondayDate().ShouldEqual(
                    mondayDate);
                startDate = startDate.AddDays(1);
            }
        };
    }

    public class when_calculating_sunday_date
    {
        static readonly DateTime sundayDate = new DateTime(2012, 12, 09);

        It does_the_whole_week_correctly = () =>
        {
            var startDate = new DateTime(2012, 12, 03);

            for (var i = 1; i < 7; i++)
            {
                startDate.SundayDate().ShouldEqual(
                    sundayDate);
                startDate = startDate.AddDays(1);
            }
        };
    }
}
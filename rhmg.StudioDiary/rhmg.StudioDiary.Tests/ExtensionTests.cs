using System;
using Machine.Specifications;
using rhmg.StudioDiary.Extensions;

namespace rhmg.StudioDiary.Tests
{
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
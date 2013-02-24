using System;
using Machine.Specifications;

namespace rhmg.StudioDiary.Tests
{
    public class when_converting_from_string
    {
        static TimePart result;

        Because of = () => result = TimePart.FromString("20:00");

        It has_converted_hour_correctly = () => result.Hour.ShouldEqual((short)20);
        It has_converted_mins_correctly = () => result.Minute.ShouldEqual((short)00);
    }

    public class when_calculating_duration_no_minutes_confusion
    {
        static TimeSpan result;

        Because of = () => result = TimePart.Duration("19:00", "23:00");

        It has_calculated_duration_correctly = () => result.TotalHours.ShouldEqual(4);
    }
    public class when_calculating_duration_minutes_confusion
    {
        static TimeSpan result;

        Because of = () => result = TimePart.Duration("19:30", "23:00");

        It has_calculated_duration_correctly = () => result.TotalHours.ShouldEqual(3.5);
    }
}
using System;
using Machine.Specifications;
using OpenFileSystem.IO;
using OpenFileSystem.IO.FileSystems.InMemory;
using coolbunny.tests.common.Fakes;
using rhmg.StudioDiary.Tests.Contexts;
using rhmg.StudioDiary.Tests.Contexts.test_entities;

namespace rhmg.StudioDiary.Tests
{
    public class when_an_unsaved_booking_attempts_to_save_an_attachment
    {
        static Booking booking;
        static IFileSystem fs;
        static Exception exception;
        Establish context = () =>
                                {
                                    booking = Bookings.standard_4_hour_evening_rehearsal_booking;
                                    fs = new InMemoryFileSystem();
                                };

        Because of = () => exception = Catch.Exception(() => booking.SaveAttachment(new FakeFile().Create("testfile1.pdf"), fs, "test/"));

        It has_thrown_an_exception = () => exception.ShouldNotBeNull();
    }

    public class when_a_saved_booking_attempts_to_save_an_attachment : with_raven_integration<Booking, Booking>
    {
        static Booking booking;
        static IFileSystem fs;
        Establish context = () =>
        {
            booking = Bookings.standard_4_hour_evening_rehearsal_booking.Save(new Repository<Booking>(session));
            fs = new InMemoryFileSystem();
        };

        Because of = () => booking.SaveAttachment(new FakeFile().Create("testfile1.pdf"), fs, "test/");

        It has_saved_a_file = () => fs.GetFile("testfile1.pdf").Exists.ShouldBeTrue();
    }
}
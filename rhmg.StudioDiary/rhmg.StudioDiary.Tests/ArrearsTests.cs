using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using rhmg.StudioDiary.Raven.Indexes;
using rhmg.StudioDiary.Tests.Contexts;
using rhmg.StudioDiary.Tests.Contexts.test_entities;

namespace rhmg.StudioDiary.Tests
{
    public class getting_a_list_of_amounts_owing_for_cancelled_bookings : with_raven_integration<Booking, Booking>
    {
        static Booking twoWeeksAgoAndCancelledWithNoNotice;
        static Booking oneWeekAgoAndComplete;
        static Booking thisWeekAndCancelledWithOneDayNotice;
        static List<Booking> result;

        Establish context = () =>
        {
            twoWeeksAgoAndCancelledWithNoNotice =
                Booking.Create(Contacts.TheBeatles.Id,
                               DateTime.Now.AddDays(-14),
                               new TimePart { Hour = 18 },
                               new TimeSpan(4, 0, 0), TestRooms.room4, Rates.standardEveningRate, Products.rehearsal);
            twoWeeksAgoAndCancelledWithNoNotice.Cancel(CancellationType.FullCost, "nobbers", twoWeeksAgoAndCancelledWithNoNotice.Date);
            twoWeeksAgoAndCancelledWithNoNotice.Save(session);

            oneWeekAgoAndComplete =
                Booking.Create(Contacts.TheBeatles.Id,
                               DateTime.Now.AddDays(-7),
                               new TimePart { Hour = 18 },
                               new TimeSpan(4, 0, 0), TestRooms.room4, Rates.standardEveningRate, Products.rehearsal);
            oneWeekAgoAndComplete.CheckIn();
            oneWeekAgoAndComplete.Save(session);
            oneWeekAgoAndComplete.ApplyPayment(new Payment
            {
                Amount = 25.00,
                DateMade = DateTime.Now.AddDays(-7),
                Method = PaymentMethod.Cash,
                PaymentType = PaymentType.Standard
            });
            oneWeekAgoAndComplete.Save(session);

            thisWeekAndCancelledWithOneDayNotice =
                Booking.Create(Contacts.TheBeatles.Id,
                               DateTime.Now,
                               new TimePart { Hour = 18 },
                               new TimeSpan(4, 0, 0), TestRooms.room4, Rates.standardEveningRate, Products.rehearsal);
            thisWeekAndCancelledWithOneDayNotice.Cancel(CancellationType.HalfCost, "nobbers", twoWeeksAgoAndCancelledWithNoNotice.Date.AddDays(-1));
            thisWeekAndCancelledWithOneDayNotice.Save(session);
        };

        Because of = () => result = AccountsJobby.BookingsWithOutstandingOwings(session);

        It has_returned_two_results = () => result.Count.ShouldEqual(2);
    }

    public class getting_a_list_of_customer_arrears_for_cancelled_bookings : with_raven_integration<Booking, Booking>
    {
        static Booking twoWeeksAgoAndCancelledWithNoNotice;
        static Booking oneWeekAgoAndComplete;
        static Booking thisWeekAndCancelledWithOneDayNotice;
        static List<CustomerArrears.Result> result;
        static Contact _theBeatles;

        Establish context = () =>
        {
            _theBeatles = Contacts.TheBeatles.Save(session);

            twoWeeksAgoAndCancelledWithNoNotice =
                Booking.Create(_theBeatles.Id,
                               DateTime.Now.AddDays(-14),
                               new TimePart { Hour = 18 },
                               new TimeSpan(4, 0, 0), TestRooms.room4, Rates.standardEveningRate, Products.rehearsal);
            twoWeeksAgoAndCancelledWithNoNotice.Cancel(CancellationType.FullCost, "nobbers", twoWeeksAgoAndCancelledWithNoNotice.Date);
            twoWeeksAgoAndCancelledWithNoNotice.Save(session);

            oneWeekAgoAndComplete =
                Booking.Create(_theBeatles.Id,
                               DateTime.Now.AddDays(-7),
                               new TimePart { Hour = 18 },
                               new TimeSpan(4, 0, 0), TestRooms.room4, Rates.standardEveningRate, Products.rehearsal);
            oneWeekAgoAndComplete.CheckIn();
            oneWeekAgoAndComplete.Save(session);
            oneWeekAgoAndComplete.ApplyPayment(new Payment
            {
                Amount = 25.00,
                DateMade = DateTime.Now.AddDays(-7),
                Method = PaymentMethod.Cash,
                PaymentType = PaymentType.Standard
            });
            oneWeekAgoAndComplete.Save(session);

            thisWeekAndCancelledWithOneDayNotice =
                Booking.Create(_theBeatles.Id,
                               DateTime.Now,
                               new TimePart { Hour = 18 },
                               new TimeSpan(4, 0, 0), TestRooms.room4, Rates.standardEveningRate, Products.rehearsal);
            thisWeekAndCancelledWithOneDayNotice.Cancel(CancellationType.HalfCost, "nobbers", twoWeeksAgoAndCancelledWithNoNotice.Date.AddDays(-1));
            thisWeekAndCancelledWithOneDayNotice.Save(session);
            wait();
        };

        Because of = () =>
        {
            result = AccountsJobby.CustomerArrears(session);
            twoWeeksAgoAndCancelledWithNoNotice = session.Load<Booking>("booking/1");
            oneWeekAgoAndComplete = session.Load<Booking>("booking/2");
            thisWeekAndCancelledWithOneDayNotice = session.Load<Booking>("booking/3");
        };

        It has_returned_one_customer_result = () => result.Count.ShouldEqual(1);
        It has_returned_the_correct_customer = () => result.First().Contact.Id.ShouldEqual("contact/1");
        It has_returned_the_correct_owings = () => result.First().AmountOwed.ShouldEqual(37.50);

        It has_cancelled_first_one = () => twoWeeksAgoAndCancelledWithNoNotice.IsCancelled.ShouldBeTrue();
        It has_owings_on_first_one = () => twoWeeksAgoAndCancelledWithNoNotice.HasOutstandingOwings.ShouldBeTrue();

        It has_customer_id_on_first_one =
            () => twoWeeksAgoAndCancelledWithNoNotice.MainContactId.ShouldEqual("contact/1");
        It has_customer_id_on_second_one =
            () => oneWeekAgoAndComplete.MainContactId.ShouldEqual("contact/1");
        It has_customer_id_on_third_one =
            () => thisWeekAndCancelledWithOneDayNotice.MainContactId.ShouldEqual("contact/1");

        It has_created_the_index = () => store.DocumentDatabase.GetIndexDefinition("CustomerArrears").IsMapReduce.ShouldBeTrue();
    }

    public class getting_arrears_for_one_customer_for_cancelled_bookings : with_raven_integration<Booking, Booking>
    {
        static Booking twoWeeksAgoAndCancelledWithNoNotice;
        static Booking oneWeekAgoAndComplete;
        static Booking thisWeekAndCancelledWithOneDayNotice;
        static CustomerArrears.Result result;
        static Contact _theBeatles;

        Establish context = () =>
        {
            _theBeatles = Contacts.TheBeatles.Save(session);

            twoWeeksAgoAndCancelledWithNoNotice =
                Booking.Create(_theBeatles.Id,
                               DateTime.Now.AddDays(-14),
                               new TimePart { Hour = 18 },
                               new TimeSpan(4, 0, 0), TestRooms.room4, Rates.standardEveningRate, Products.rehearsal);
            twoWeeksAgoAndCancelledWithNoNotice.Cancel(CancellationType.FullCost, "nobbers", twoWeeksAgoAndCancelledWithNoNotice.Date);
            twoWeeksAgoAndCancelledWithNoNotice.Save(session);

            oneWeekAgoAndComplete =
                Booking.Create(_theBeatles.Id,
                               DateTime.Now.AddDays(-7),
                               new TimePart { Hour = 18 },
                               new TimeSpan(4, 0, 0), TestRooms.room4, Rates.standardEveningRate, Products.rehearsal);
            oneWeekAgoAndComplete.CheckIn();
            oneWeekAgoAndComplete.Save(session);
            oneWeekAgoAndComplete.ApplyPayment(new Payment
            {
                Amount = 25.00,
                DateMade = DateTime.Now.AddDays(-7),
                Method = PaymentMethod.Cash,
                PaymentType = PaymentType.Standard
            });
            oneWeekAgoAndComplete.Save(session);

            thisWeekAndCancelledWithOneDayNotice =
                Booking.Create(_theBeatles.Id,
                               DateTime.Now,
                               new TimePart { Hour = 18 },
                               new TimeSpan(4, 0, 0), TestRooms.room4, Rates.standardEveningRate, Products.rehearsal);
            thisWeekAndCancelledWithOneDayNotice.Cancel(CancellationType.HalfCost, "nobbers", twoWeeksAgoAndCancelledWithNoNotice.Date.AddDays(-1));
            thisWeekAndCancelledWithOneDayNotice.Save(session);
            wait();
        };

        Because of = () => result = _theBeatles.GetArrears(session);

        It has_returned_the_correct_customer = () => result.Contact.Id.ShouldEqual("contact/1");
        It has_returned_the_correct_owings = () => result.AmountOwed.ShouldEqual(37.50);
    }

    public class getting_arrears_for_one_customer_for_attended_bookings : with_raven_integration<Booking, Booking>
    {
        static Booking oneWeekAgoAndComplete;
        static CustomerArrears.Result result;
        static Contact _theBeatles;

        Establish context = () =>
        {
            _theBeatles = Contacts.TheBeatles.Save(session);

            oneWeekAgoAndComplete =
                Booking.Create(_theBeatles.Id,
                               DateTime.Now.AddDays(-7),
                               new TimePart { Hour = 18 },
                               new TimeSpan(4, 0, 0), TestRooms.room4, Rates.standardEveningRate, Products.rehearsal);
            oneWeekAgoAndComplete.CheckIn();
            oneWeekAgoAndComplete.Save(session);
            oneWeekAgoAndComplete.ApplyPayment(new Payment
                                                   {
                                                       Amount = 20.00,
                                                       DateMade = DateTime.Now.AddDays(-7),
                                                       Method = PaymentMethod.Cash,
                                                       PaymentType = PaymentType.Standard
                                                   });
            oneWeekAgoAndComplete.Save(session);
            wait();
        };

        Because of = () => result = _theBeatles.GetArrears(session);

        It has_returned_the_correct_customer = () => result.Contact.Id.ShouldEqual("contact/1");
        It has_returned_the_correct_owings = () => result.AmountOwed.ShouldEqual(5.00);
    }
}
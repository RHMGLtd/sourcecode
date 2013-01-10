using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Machine.Specifications;
using rhmg.StudioDiary.Raven.Indexes;
using rhmg.StudioDiary.Tests.Contexts;
using rhmg.StudioDiary.Tests.Contexts.test_entities;

namespace rhmg.StudioDiary.Tests
{
    public class when_a_new_booking_is_made : with_booking
    {
        It has_the_correct_value_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Value().ShouldEqual(Rates.standardEveningRate.PoundsAmount);

        It has_the_correct_outstanding_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Outstanding().ShouldEqual(Rates.standardEveningRate.PoundsAmount);
    }

    public class when_a_deposit_is_taken : with_booking
    {
        Because of = () => standard_4_hour_evening_rehearsal_booking.ApplyPayment(new Payment
                                                                                      {
                                                                                          Amount = 5.00,
                                                                                          DateMade = DateTime.Now,
                                                                                          Method = PaymentMethod.Cash,
                                                                                          PaymentType = PaymentType.Deposit
                                                                                      });

        It has_the_correct_value_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Value().ShouldEqual(Rates.standardEveningRate.PoundsAmount);

        It has_the_correct_outstanding_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Outstanding().ShouldEqual(Rates.standardEveningRate.PoundsAmount - 5.00);
    }

    public class when_a_payment_in_full_is_taken : with_booking
    {
        Because of = () => standard_4_hour_evening_rehearsal_booking.ApplyPayment(new Payment
        {
            Amount = 25.00,
            DateMade = DateTime.Now,
            Method = PaymentMethod.Cash,
            PaymentType = PaymentType.Standard
        });

        It has_the_correct_value_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Value().ShouldEqual(Rates.standardEveningRate.PoundsAmount);

        It has_the_correct_outstanding_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Outstanding().ShouldEqual(0.00);
    }

    public class when_a_balance_is_paid : with_booking
    {
        Establish context = () => standard_4_hour_evening_rehearsal_booking.ApplyPayment(new Payment
        {
            Amount = 5.00,
            DateMade = DateTime.Now,
            Method = PaymentMethod.Cash,
            PaymentType = PaymentType.Deposit
        });

        Because of = () => standard_4_hour_evening_rehearsal_booking.ApplyPayment(new Payment
        {
            Amount = 20.00,
            DateMade = DateTime.Now,
            Method = PaymentMethod.Cash,
            PaymentType = PaymentType.Standard
        });

        It has_the_correct_value_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Value().ShouldEqual(Rates.standardEveningRate.PoundsAmount);

        It has_the_correct_outstanding_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Outstanding().ShouldEqual(0.00);
    }

    public class when_a_booking_is_cancelled_at_no_cost : with_booking
    {
        Because of = () => standard_4_hour_evening_rehearsal_booking.Cancel(CancellationType.NoCost, "illness", DateTime.Now);

        It has_the_correct_value_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Value().ShouldEqual(Rates.standardEveningRate.PoundsAmount);

        It has_the_correct_outstanding_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Outstanding().ShouldEqual(0.00);
    }

    public class when_a_booking_is_cancelled_at_half_cost : with_booking
    {
        Because of = () => standard_4_hour_evening_rehearsal_booking.Cancel(CancellationType.HalfCost, "illness", DateTime.Now);

        It has_the_correct_value_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Value().ShouldEqual(Rates.standardEveningRate.PoundsAmount);

        It has_the_correct_outstanding_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Outstanding().ShouldEqual(12.50);
    }

    public class when_a_booking_is_cancelled_at_full_cost : with_booking
    {
        Because of = () => standard_4_hour_evening_rehearsal_booking.Cancel(CancellationType.FullCost, "illness", DateTime.Now);

        It has_the_correct_value_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Value().ShouldEqual(Rates.standardEveningRate.PoundsAmount);

        It has_the_correct_outstanding_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Outstanding().ShouldEqual(25.00);
    }

    public class when_a_booking_is_cancelled_at_half_cost_after_a_deposit_is_paid : with_booking
    {
        Establish context = () => standard_4_hour_evening_rehearsal_booking.ApplyPayment(new Payment
        {
            Amount = 5.00,
            DateMade = DateTime.Now,
            Method = PaymentMethod.Cash,
            PaymentType = PaymentType.Deposit
        });

        Because of = () => standard_4_hour_evening_rehearsal_booking.Cancel(CancellationType.HalfCost, "illness", DateTime.Now);

        It has_the_correct_value_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Value().ShouldEqual(Rates.standardEveningRate.PoundsAmount);

        It has_the_correct_outstanding_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Outstanding().ShouldEqual(12.50 - 5.00);
    }

    public class when_a_booking_is_cancelled_at_full_cost_after_a_deposit_is_paid : with_booking
    {
        Establish context = () => standard_4_hour_evening_rehearsal_booking.ApplyPayment(new Payment
        {
            Amount = 5.00,
            DateMade = DateTime.Now,
            Method = PaymentMethod.Cash,
            PaymentType = PaymentType.Deposit
        });

        Because of = () => standard_4_hour_evening_rehearsal_booking.Cancel(CancellationType.FullCost, "illness", DateTime.Now);

        It has_the_correct_value_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Value().ShouldEqual(Rates.standardEveningRate.PoundsAmount);

        It has_the_correct_outstanding_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Outstanding().ShouldEqual(25.00 - 5.00);
    }

    public class when_multiple_ad_hoc_payments_are_made : with_booking
    {
        Establish context = () =>
                                {
                                    standard_4_hour_evening_rehearsal_booking.ApplyPayment(new Payment
                                    {
                                        Amount = 6.25,
                                        DateMade = DateTime.Now,
                                        Method = PaymentMethod.Cash,
                                        PaymentType = PaymentType.Standard
                                    });
                                    standard_4_hour_evening_rehearsal_booking.ApplyPayment(new Payment
                                    {
                                        Amount = 6.25,
                                        DateMade = DateTime.Now,
                                        Method = PaymentMethod.Cash,
                                        PaymentType = PaymentType.Standard
                                    });
                                };

        It has_the_correct_value_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Value().ShouldEqual(Rates.standardEveningRate.PoundsAmount);

        It has_the_correct_outstanding_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Outstanding().ShouldEqual(25.00 - 6.25 - 6.25);
    }

    public class when_additional_equipment_is_booked : with_booking
    {
        Because of = () => standard_4_hour_evening_rehearsal_booking.AddAdditionalEquipment(AdditionalEquipments.cymbalSet);

        It has_the_correct_value_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Value().ShouldEqual(Rates.standardEveningRate.PoundsAmount + 15.00);

        It has_the_correct_outstanding_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Outstanding().ShouldEqual(25.00 + 15.00);
    }

    public class when_adding_a_note_to_a_booking : with_booking
    {
        Because of = () => standard_4_hour_evening_rehearsal_booking.AddNote(new Note
                                                                                 {
                                                                                     Content = "This is a note to say this band are great"
                                                                                 });

        It has_added_the_note_to_the_collection =
            () => standard_4_hour_evening_rehearsal_booking.Notes.Count.ShouldEqual(1);

        It has_todays_date_as_the_date_created =
            () => standard_4_hour_evening_rehearsal_booking.Notes[0].DateCreated.Date.ShouldEqual(DateTime.Now.Date);
    }

    public class when_a_new_booking : with_booking
    {
        static Booking booking;

        Establish context = () => booking = Bookings.standard_4_hour_evening_rehearsal_booking;

        It is_not_checked_in = () => booking.CheckedIn.ShouldBeFalse();
    }

    public class when_arriving_a_booking : with_booking
    {
        static Booking booking;

        Establish context = () => booking = Bookings.standard_4_hour_evening_rehearsal_booking;

        Because of = () => booking.CheckIn();

        It sets_the_booking_as_checked_in = () => booking.CheckedIn.ShouldBeTrue();
    }

    public class when_saving_a_new_booking : with_raven_integration<Booking, Booking>
    {
        static Booking booking;

        Establish context = () => booking = Bookings.standard_4_hour_evening_rehearsal_booking;

        Because of = () => booking = booking.Save(new Repository<Booking>(session));

        It has_provided_an_id = () => booking.Id.ShouldEqual("booking/1");
    }

    public class when_retrieving_a_booking : with_raven_integration<Booking, Booking>
    {
        static Booking booking;

        Establish context = () =>
                                {
                                    var bob = Bookings.standard_4_hour_evening_rehearsal_booking;
                                    bob.Save(new Repository<Booking>(session));
                                };

        Because of = () => booking = Booking.Get("booking/1", new Repository<Booking>(session));

        It has_loaded = () => booking.ShouldNotBeNull();
    }

    public class is_a_booking_peak_or_not
    {
        static Booking sixPmOnAMondayForOneHour;
        static Booking sixPmOnAMondayForFourHours;
        static Booking sevenPmOnAMonday;

        Establish context = () =>
        {
            sixPmOnAMondayForOneHour = Booking.Create(new List<Contact> { Contacts.TheBeatles }, Dates.monday, new TimePart { Hour = 18 },
                                                      new TimeSpan(1, 0, 0), Rooms.room4,
                                                      Rates.standardEveningRate);
            sixPmOnAMondayForFourHours = Booking.Create(new List<Contact> { Contacts.TheBeatles }, Dates.monday, new TimePart { Hour = 18 },
                                                      new TimeSpan(4, 0, 0), Rooms.room4,
                                                      Rates.standardEveningRate);
            sevenPmOnAMonday = Booking.Create(new List<Contact> { Contacts.TheBeatles }, Dates.monday, new TimePart { Hour = 19 },
                                                      new TimeSpan(4, 0, 0), Rooms.room4,
                                                      Rates.standardEveningRate);
        };

        It works_out_six_pm_for_one_hour_is_not_peak_time =
            () => sixPmOnAMondayForOneHour.IsInWeekdayPeakTime().ShouldBeFalse();
        It works_out_six_pm_for_four_hours_is_peak_time =
            () => sixPmOnAMondayForFourHours.IsInWeekdayPeakTime().ShouldBeTrue();
        It works_out_seven_pm_for_four_hours_is_peak_time =
            () => sevenPmOnAMonday.IsInWeekdayPeakTime().ShouldBeTrue();
    }

    public class getting_a_list_of_amounts_owing : with_raven_integration<Booking, Booking>
    {
        static Booking twoWeeksAgoAndCancelledWithNoNotice;
        static Booking oneWeekAgoAndComplete;
        static Booking thisWeekAndCancelledWithOneDayNotice;
        static List<Booking> result;

        Establish context = () =>
                                {
                                    twoWeeksAgoAndCancelledWithNoNotice =
                                        Booking.Create(new List<Contact> {Contacts.TheBeatles},
                                                       DateTime.Now.AddDays(-14),
                                                       new TimePart {Hour = 18},
                                                       new TimeSpan(4, 0, 0), Rooms.room4, Rates.standardEveningRate);
                                    twoWeeksAgoAndCancelledWithNoNotice.Cancel(CancellationType.FullCost, "nobbers", twoWeeksAgoAndCancelledWithNoNotice.Date);
                                    twoWeeksAgoAndCancelledWithNoNotice.Save(new Repository<Booking>(session));

                                    oneWeekAgoAndComplete =
                                        Booking.Create(new List<Contact> { Contacts.TheBeatles },
                                                       DateTime.Now.AddDays(-7),
                                                       new TimePart { Hour = 18 },
                                                       new TimeSpan(4, 0, 0), Rooms.room4, Rates.standardEveningRate);
                                    oneWeekAgoAndComplete.Save(new Repository<Booking>(session));

                                    thisWeekAndCancelledWithOneDayNotice =
                                        Booking.Create(new List<Contact> { Contacts.TheBeatles },
                                                       DateTime.Now,
                                                       new TimePart { Hour = 18 },
                                                       new TimeSpan(4, 0, 0), Rooms.room4, Rates.standardEveningRate);
                                    thisWeekAndCancelledWithOneDayNotice.Cancel(CancellationType.HalfCost, "nobbers", twoWeeksAgoAndCancelledWithNoNotice.Date.AddDays(-1));
                                    thisWeekAndCancelledWithOneDayNotice.Save(new Repository<Booking>(session));
                                };

        Because of = () => result = AccountsJobby.BookingsWithOutstandingOwings(new Repository<Booking>(session));

        It has_returned_two_results = () => result.Count.ShouldEqual(2);
    }

    public class getting_a_list_of_customer_arrears : with_raven_integration<Booking, Booking>
    {
        static Booking twoWeeksAgoAndCancelledWithNoNotice;
        static Booking oneWeekAgoAndComplete;
        static Booking thisWeekAndCancelledWithOneDayNotice;
        static List<CustomerArrears.Result> result;

        Establish context = () =>
        {
            twoWeeksAgoAndCancelledWithNoNotice =
                Booking.Create(new List<Contact> { Contacts.TheBeatles },
                               DateTime.Now.AddDays(-14),
                               new TimePart { Hour = 18 },
                               new TimeSpan(4, 0, 0), Rooms.room4, Rates.standardEveningRate);
            twoWeeksAgoAndCancelledWithNoNotice.Cancel(CancellationType.FullCost, "nobbers", twoWeeksAgoAndCancelledWithNoNotice.Date);
            twoWeeksAgoAndCancelledWithNoNotice.Save(new Repository<Booking>(session));

            oneWeekAgoAndComplete =
                Booking.Create(new List<Contact> { Contacts.TheBeatles },
                               DateTime.Now.AddDays(-7),
                               new TimePart { Hour = 18 },
                               new TimeSpan(4, 0, 0), Rooms.room4, Rates.standardEveningRate);
            oneWeekAgoAndComplete.Save(new Repository<Booking>(session));

            thisWeekAndCancelledWithOneDayNotice =
                Booking.Create(new List<Contact> { Contacts.TheBeatles },
                               DateTime.Now,
                               new TimePart { Hour = 18 },
                               new TimeSpan(4, 0, 0), Rooms.room4, Rates.standardEveningRate);
            thisWeekAndCancelledWithOneDayNotice.Cancel(CancellationType.HalfCost, "nobbers", twoWeeksAgoAndCancelledWithNoNotice.Date.AddDays(-1));
            thisWeekAndCancelledWithOneDayNotice.Save(new Repository<Booking>(session));
            wait();
        };

        Because of = () =>
                        {
                            result = AccountsJobby.CustomerArrears(new Repository<Booking>(session));
                            twoWeeksAgoAndCancelledWithNoNotice = new Repository<Booking>(session).Get("booking/1");
                            oneWeekAgoAndComplete = new Repository<Booking>(session).Get("booking/2");
                            thisWeekAndCancelledWithOneDayNotice = new Repository<Booking>(session).Get("booking/3");
                        };

        It has_returned_one_customer_result = () => result.Count.ShouldEqual(1);
        It has_bob = () => twoWeeksAgoAndCancelledWithNoNotice.ShouldNotBeNull();
        It has_bob2 = () => oneWeekAgoAndComplete.ShouldNotBeNull();
        It has_bob3 = () => thisWeekAndCancelledWithOneDayNotice.ShouldNotBeNull();

        It has_cancelled_first_one = () => twoWeeksAgoAndCancelledWithNoNotice.IsCancelled.ShouldBeTrue();
        It has_owings_on_first_one = () => twoWeeksAgoAndCancelledWithNoNotice.HasOutstandingOwings.ShouldBeTrue();

        It has_customer_id_on_first_one =
            () => twoWeeksAgoAndCancelledWithNoNotice.Contacts.First().Id.ShouldEqual("contact/1");
        It has_customer_id_on_second_one =
            () => oneWeekAgoAndComplete.Contacts.First().Id.ShouldEqual("contact/1");
        It has_customer_id_on_third_one =
            () => thisWeekAndCancelledWithOneDayNotice.Contacts.First().Id.ShouldEqual("contact/1");

        It has_created_the_index = () => store.DocumentDatabase.GetIndexDefinition("CustomerArrears").IsMapReduce.ShouldBeTrue();
    }
}
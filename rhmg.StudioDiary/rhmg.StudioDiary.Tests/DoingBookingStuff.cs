using System;
using System.Collections.Generic;
using Machine.Specifications;
using rhmg.StudioDiary.Tests.Contexts;

namespace rhmg.StudioDiary.Tests
{
    public class when_a_new_booking_is_made : with_booking
    {
        It has_the_correct_value_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Value().ShouldEqual(test_entities.standardEveningRate.PoundsAmount);

        It has_the_correct_outstanding_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Outstanding().ShouldEqual(test_entities.standardEveningRate.PoundsAmount);
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
            () => standard_4_hour_evening_rehearsal_booking.Value().ShouldEqual(test_entities.standardEveningRate.PoundsAmount);

        It has_the_correct_outstanding_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Outstanding().ShouldEqual(test_entities.standardEveningRate.PoundsAmount - 5.00);
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
            () => standard_4_hour_evening_rehearsal_booking.Value().ShouldEqual(test_entities.standardEveningRate.PoundsAmount);

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
            () => standard_4_hour_evening_rehearsal_booking.Value().ShouldEqual(test_entities.standardEveningRate.PoundsAmount);

        It has_the_correct_outstanding_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Outstanding().ShouldEqual(0.00);
    }

    public class when_a_booking_is_cancelled_at_no_cost : with_booking
    {
        Because of = () => standard_4_hour_evening_rehearsal_booking.Cancel(CancellationType.NoCost, "illness", DateTime.Now);

        It has_the_correct_value_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Value().ShouldEqual(test_entities.standardEveningRate.PoundsAmount);

        It has_the_correct_outstanding_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Outstanding().ShouldEqual(0.00);
    }

    public class when_a_booking_is_cancelled_at_half_cost : with_booking
    {
        Because of = () => standard_4_hour_evening_rehearsal_booking.Cancel(CancellationType.HalfCost, "illness", DateTime.Now);

        It has_the_correct_value_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Value().ShouldEqual(test_entities.standardEveningRate.PoundsAmount);

        It has_the_correct_outstanding_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Outstanding().ShouldEqual(12.50);
    }

    public class when_a_booking_is_cancelled_at_full_cost : with_booking
    {
        Because of = () => standard_4_hour_evening_rehearsal_booking.Cancel(CancellationType.FullCost, "illness", DateTime.Now);

        It has_the_correct_value_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Value().ShouldEqual(test_entities.standardEveningRate.PoundsAmount);

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
            () => standard_4_hour_evening_rehearsal_booking.Value().ShouldEqual(test_entities.standardEveningRate.PoundsAmount);

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
            () => standard_4_hour_evening_rehearsal_booking.Value().ShouldEqual(test_entities.standardEveningRate.PoundsAmount);

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
            () => standard_4_hour_evening_rehearsal_booking.Value().ShouldEqual(test_entities.standardEveningRate.PoundsAmount);

        It has_the_correct_outstanding_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Outstanding().ShouldEqual(25.00 - 6.25 - 6.25);
    }

    public class when_additional_equipment_is_booked : with_booking
    {
        Because of = () => standard_4_hour_evening_rehearsal_booking.AddAdditionalEquipment(test_entities.cymbalSet);

        It has_the_correct_value_calculated =
            () => standard_4_hour_evening_rehearsal_booking.Value().ShouldEqual(test_entities.standardEveningRate.PoundsAmount + 15.00);

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

    public class when_saving_a_new_booking : with_raven_integration<Booking, Booking>
    {
        static Booking booking;

        Establish context = () => booking = test_entities.standard_4_hour_evening_rehearsal_booking;

        Because of = () => booking = booking.Save(new Repository<Booking>(session));

        It has_provided_an_id = () => booking.Id.ShouldEqual("booking/1");
    }

    public class when_retrieving_a_booking : with_raven_integration<Booking, Booking>
    {
        static Booking booking;

        Establish context = () =>
                                {
                                    var bob = test_entities.standard_4_hour_evening_rehearsal_booking;
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
            sixPmOnAMondayForOneHour = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.monday, new TimePart { Hour = 18 },
                                                      new TimeSpan(1, 0, 0), test_entities.Rooms.room4,
                                                      test_entities.standardEveningRate);
            sixPmOnAMondayForFourHours = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.monday, new TimePart { Hour = 18 },
                                                      new TimeSpan(4, 0, 0), test_entities.Rooms.room4,
                                                      test_entities.standardEveningRate);
            sevenPmOnAMonday = Booking.Create(new List<Contact> { test_entities.TheBeatles }, test_entities.Dates.monday, new TimePart { Hour = 19 },
                                                      new TimeSpan(4, 0, 0), test_entities.Rooms.room4,
                                                      test_entities.standardEveningRate);
        };

        It works_out_six_pm_for_one_hour_is_not_peak_time =
            () => sixPmOnAMondayForOneHour.IsInWeekdayPeakTime().ShouldBeFalse();
        It works_out_six_pm_for_four_hours_is_peak_time =
            () => sixPmOnAMondayForFourHours.IsInWeekdayPeakTime().ShouldBeTrue();
        It works_out_seven_pm_for_four_hours_is_peak_time =
            () => sevenPmOnAMonday.IsInWeekdayPeakTime().ShouldBeTrue();
    }
}
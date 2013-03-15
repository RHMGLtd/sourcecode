using System;
using Machine.Specifications;
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
    public class when_checking_a_booking_is_valid_for_a_time : with_booking
    {
        It knows_it_is_not_valid_for_10_am = () => standard_4_hour_evening_rehearsal_booking.IsValidFor(10).ShouldBeFalse(); //10am-11am
        It knows_it_is_valid_for_4_pm = () => standard_4_hour_evening_rehearsal_booking.IsValidFor(16).ShouldBeFalse(); //4pm-5pm
        It knows_it_is_not_valid_for_5_pm = () => standard_4_hour_evening_rehearsal_booking.IsValidFor(17).ShouldBeFalse(); //5pm-6pm

        It knows_it_is_valid_for_midday = () => standard_4_hour_evening_rehearsal_booking.IsValidFor(12).ShouldBeTrue(); //12pm-1pm
        It knows_it_is_valid_for_1_pm = () => standard_4_hour_evening_rehearsal_booking.IsValidFor(13).ShouldBeTrue(); //1pm-2pm
        It knows_it_is_valid_for_2_pm = () => standard_4_hour_evening_rehearsal_booking.IsValidFor(14).ShouldBeTrue(); //2pm-3pm
        It knows_it_is_valid_for_3_pm = () => standard_4_hour_evening_rehearsal_booking.IsValidFor(15).ShouldBeTrue(); //3pm-4pm
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

        Establish context = () => booking = Bookings.rehearsals.standard_4_hour_evening_rehearsal_booking;

        It is_not_checked_in = () => booking.CheckedIn.ShouldBeFalse();
    }

    public class when_arriving_a_booking : with_booking
    {
        static Booking booking;

        Establish context = () => booking = Bookings.rehearsals.standard_4_hour_evening_rehearsal_booking;

        Because of = () => booking.CheckIn();

        It sets_the_booking_as_checked_in = () => booking.CheckedIn.ShouldBeTrue();
    }

    public class when_no_showing_a_booking : with_booking
    {
        static Booking booking;

        Establish context = () => booking = Bookings.rehearsals.standard_4_hour_evening_rehearsal_booking;

        Because of = () => booking.NoShow();

        It sets_the_booking_as_checked_in = () => booking.IsNoShow.ShouldBeTrue();
    }


    public class when_saving_a_new_booking : with_raven_integration<Booking, Booking>
    {
        static Booking booking;

        Establish context = () => booking = Bookings.rehearsals.standard_4_hour_evening_rehearsal_booking;

        Because of = () => booking = booking.Save(session);

        It has_provided_an_id = () => booking.Id.ShouldEqual("booking/1");
    }

    public class when_retrieving_a_booking : with_raven_integration<Booking, Booking>
    {
        static Booking booking;

        Establish context = () =>
                                {
                                    var contact = Contacts.TheBeatles.Save(session);
                                    var bob = Bookings.rehearsals.standard_4_hour_evening_rehearsal_booking;
                                    bob.MainContactId = contact.Id;
                                    bob.Save(session);
                                };

        Because of = () => booking = Booking.Get("booking/1", session);

        It has_loaded = () => booking.ShouldNotBeNull();
    }

    public class is_a_booking_peak_or_not
    {
        static Booking sixPmOnAMondayForOneHour;
        static Booking sixPmOnAMondayForFourHours;
        static Booking sevenPmOnAMonday;

        Establish context = () =>
        {
            sixPmOnAMondayForOneHour = Booking.Create(Contacts.TheBeatles.Id, Dates.monday, new TimePart { Hour = 18 },
                                                      new TimeSpan(1, 0, 0), TestRooms.room4,
                                                      Rates.standardEveningRate, Products.rehearsal);
            sixPmOnAMondayForFourHours = Booking.Create(Contacts.TheBeatles.Id, Dates.monday, new TimePart { Hour = 18 },
                                                      new TimeSpan(4, 0, 0), TestRooms.room4,
                                                      Rates.standardEveningRate, Products.rehearsal);
            sevenPmOnAMonday = Booking.Create(Contacts.TheBeatles.Id, Dates.monday, new TimePart { Hour = 19 },
                                                      new TimeSpan(4, 0, 0), TestRooms.room4,
                                                      Rates.standardEveningRate, Products.rehearsal);
        };

        It works_out_six_pm_for_one_hour_is_not_peak_time =
            () => sixPmOnAMondayForOneHour.IsInWeekdayPeakTime().ShouldBeFalse();
        It works_out_six_pm_for_four_hours_is_peak_time =
            () => sixPmOnAMondayForFourHours.IsInWeekdayPeakTime().ShouldBeTrue();
        It works_out_seven_pm_for_four_hours_is_peak_time =
            () => sevenPmOnAMonday.IsInWeekdayPeakTime().ShouldBeTrue();
    }

    public class booking_with_a_one_off_charge : with_raven_integration<Booking, Booking>
    {
        static Booking toTest;
        static double currentlyOwing;
        Establish context = () =>
                                {
                                    toTest = Bookings.rehearsals.standard_4_hour_evening_rehearsal_booking;
                                    toTest.Rate = null;
                                    toTest.OneOffCharge = 150.00;
                                    toTest.Save(session);
                                };

        Because of = () => currentlyOwing = toTest.CurrentlyOwed;

        It ignores_rate_and_uses_one_off_charge = () => currentlyOwing.ShouldEqual(150.00);
    }
    /*
    public class when_arrears_are_shared_between_multiple_customers : with_raven_integration<Booking, Booking>
    {
        static Booking twoWeeksAgoAndCancelledWithNoNotice;
        static Booking oneWeekAgoAndComplete;
        static Booking thisWeekAndCancelledWithOneDayNotice;
        static List<CustomerArrears.Result> result;

        Establish context = () =>
        {
            Contacts.TheBeatles.Save(session);
            Contacts.TheStones.Save(session);

            twoWeeksAgoAndCancelledWithNoNotice =
                Booking.Create(new List<Contact> { Contacts.TheBeatles, Contacts.TheStones },
                               DateTime.Now.AddDays(-14),
                               new TimePart { Hour = 18 },
                               new TimeSpan(4, 0, 0), TestRooms.room4, Rates.standardEveningRate);
            twoWeeksAgoAndCancelledWithNoNotice.Cancel(CancellationType.FullCost, "nobbers", twoWeeksAgoAndCancelledWithNoNotice.Date);
            twoWeeksAgoAndCancelledWithNoNotice.Save(session);

            oneWeekAgoAndComplete =
                Booking.Create(new List<Contact> { Contacts.TheBeatles, Contacts.TheStones },
                               DateTime.Now.AddDays(-7),
                               new TimePart { Hour = 18 },
                               new TimeSpan(4, 0, 0), TestRooms.room4, Rates.standardEveningRate);
            oneWeekAgoAndComplete.Save(session);

            thisWeekAndCancelledWithOneDayNotice =
                Booking.Create(new List<Contact> { Contacts.TheBeatles, Contacts.TheStones },
                               DateTime.Now,
                               new TimePart { Hour = 18 },
                               new TimeSpan(4, 0, 0), TestRooms.room4, Rates.standardEveningRate);
            thisWeekAndCancelledWithOneDayNotice.Cancel(CancellationType.HalfCost, "nobbers", twoWeeksAgoAndCancelledWithNoNotice.Date.AddDays(-1));
            thisWeekAndCancelledWithOneDayNotice.Save(session);
            wait();
        };

        Because of = () => result = AccountsJobby.CustomerArrears(session);

        It has_returned_two_customer_results = () => result.Count.ShouldEqual(2);
        It has_returned_the_correct_first_customer = () => result.First().Contact.Id.ShouldEqual("contact/1");
        It has_returned_the_correct_first_owings = () => result.First().AmountOwed.ShouldEqual(37.50);
        It has_returned_the_correct_second_customer = () => result.Last().Contact.Id.ShouldEqual("contact/2");
        It has_returned_the_correct_second_owings = () => result.Last().AmountOwed.ShouldEqual(37.50);
    }

    public class getting_bookings_by_contact : with_raven_integration<Booking, Booking>
    {
        static Booking twoWeeksAgoAndCancelledWithNoNotice;
        static Booking oneWeekAgoAndComplete;
        static Booking thisWeekAndCancelledWithOneDayNotice;
        static List<Bookings_ByContact.Result> result;
        static List<Booking> beatlesBookings;
        static List<Booking> stonesBookings;

        Establish context = () =>
        {
            Contacts.TheBeatles.Save(session);
            Contacts.TheStones.Save(session);
            twoWeeksAgoAndCancelledWithNoNotice =
            Booking.Create(Contacts.TheBeatles.Id,
                           DateTime.Now.AddDays(-14),
                           new TimePart { Hour = 18 },
                           new TimeSpan(4, 0, 0), TestRooms.room4, Rates.standardEveningRate);
            twoWeeksAgoAndCancelledWithNoNotice.Save(session);

            oneWeekAgoAndComplete =
                Booking.Create(new List<Contact> { Contacts.TheBeatles, Contacts.TheStones },
                               DateTime.Now.AddDays(-7),
                               new TimePart { Hour = 18 },
                               new TimeSpan(4, 0, 0), TestRooms.room4, Rates.standardEveningRate);
            oneWeekAgoAndComplete.Save(session);

            thisWeekAndCancelledWithOneDayNotice =
                Booking.Create(new List<Contact> { Contacts.TheStones },
                               DateTime.Now,
                               new TimePart { Hour = 18 },
                               new TimeSpan(4, 0, 0), TestRooms.room4, Rates.standardEveningRate);
            thisWeekAndCancelledWithOneDayNotice.Save(session);
            wait();
        };

        Because of = () =>
                         {
                             result = session.Query<Bookings_ByContact.Result>("bookings/bycontact").ToList();
                             beatlesBookings = Contact.Get(Contacts.TheBeatles.Id, session).Bookings;
                             stonesBookings = Contact.Get(Contacts.TheStones.Id, session).Bookings;
                         };

        It has_got_two_results = () => result.Count.ShouldEqual(2);
        It each_bookings_has_two_bookings = () => result.First().BookingIds.Length.ShouldEqual(2);
        It beatles_have_two_bookings = () => beatlesBookings.Count.ShouldEqual(2);
        It stones_have_two_bookings = () => stonesBookings.Count.ShouldEqual(2);
    }*/
}

/*docs.Where(x => x.Contacts.Any(y => y.Id == contact.Id))*/
using System;
using System.Collections.Generic;
using System.Linq;
using blackpoolgigs.common.Models;
using Machine.Specifications;

namespace blackpoolgigs.common.tests
{
    public class when_creating_a_new_diary
    {
        static Diary diary;
        Establish context = () =>
            {
                diary = new Diary(new DateTime(2011, 04, 15));
            };

        It has_set_first_day_correctly = () => diary.FirstDayOfMonth.Day.ShouldEqual(1);
        It has_five_diary_lines = () => diary.Lines.Count().ShouldEqual(5);
        It has_three_days_in_the_first_week = () => diary.Lines[0].Entries.Count().ShouldEqual(3);
        It has_six_days_in_the_last_week = () => diary.Lines[4].Entries.Count().ShouldEqual(6);
        It has_no_gigs_though = () => diary.HasGigs().ShouldBeFalse();
    }
    public class when_creating_a_new_diary_when_month_begins_on_monday
    {
        static Diary diary;
        Establish context = () =>
        {
            diary = new Diary(new DateTime(2010, 11, 15));
        };

        It has_set_first_day_correctly = () => diary.FirstDayOfMonth.Day.ShouldEqual(1);
        It has_five_diary_lines = () => diary.Lines.Count().ShouldEqual(5);
        It has_three_days_in_the_first_week = () => diary.Lines[0].Entries.Count().ShouldEqual(7);
        It has_two_days_in_the_last_week = () => diary.Lines[4].Entries.Count().ShouldEqual(2);
    }
    public class when_creating_a_new_diary_when_month_begins_on_sunday
    {
        static Diary diary;
        Establish context = () =>
        {
            diary = new Diary(new DateTime(2011, 05, 15));
        };

        It has_set_first_day_correctly = () => diary.FirstDayOfMonth.Day.ShouldEqual(1);
        It has_five_diary_lines = () => diary.Lines.Count().ShouldEqual(6);
        It has_three_days_in_the_first_week = () => diary.Lines[0].Entries.Count().ShouldEqual(1);
    }
    public class when_passing_a_gig_collection_into_a_diary
    {
        static Diary diary;
        static List<Gig> gigs = new List<Gig>();
        Establish context = () =>
        {
            diary = new Diary(new DateTime(2011, 04, 15));
            for (var i = 1; i < 11; i++)
            {
                gigs.Add(new Gig()
                             {
                                 BandNames = new[]
                                                 {
                                                     new BandName
                                                         {
                                                             Value = "test1" + i
                                                         },
                                                     new BandName
                                                         {
                                                             Value = "test2" + i
                                                         },
                                                     new BandName
                                                         {
                                                             Value = "test3" + i
                                                         }
                                                 },
                                 Date = new DateTime(2011, 04, i),
                                 Price = "free",
                                 StartTime = "18:00",
                                 Venue = "The Pub"
                             });
            }
        };

        Because of = () => diary.AddGigs(gigs, false);

        It the_first_friday_has_a_gig = () => diary.Lines[0].Friday.Gigs.Count().ShouldEqual(1);
        It the_first_saturday_has_a_gig = () => diary.Lines[0].Saturday.Gigs.Count().ShouldEqual(1);
        It has_gigs = () => diary.HasGigs().ShouldBeTrue();
    }

    public class when_passing_a_gig_collection_across_multiple_months_into_a_diary
    {
        static Diary diary;
        static List<Gig> gigs = new List<Gig>();
        Establish context = () =>
        {
            diary = new Diary(new DateTime(2011, 04, 15));
            for (var i = 1; i < 11; i++)
            {
                gigs.Add(new Gig
                             {
                                 BandNames = new[]
                                                 {
                                                     new BandName
                                                         {
                                                             Value = "test1" + i
                                                         },
                                                     new BandName
                                                         {
                                                             Value = "test2" + i
                                                         },
                                                     new BandName
                                                         {
                                                             Value = "test3" + i
                                                         }
                                                 },
                                 Date = new DateTime(2011, 04, i),
                                 Price = "free",
                                 StartTime = "18:00",
                                 Venue = "The Pub"
                             });
            }
            gigs.Add(new Gig
                         {
                             BandNames = new[]
                                             {
                                                 new BandName()
                                                     {
                                                         Value = "another test"
                                                     }

                                               },
                             Date = new DateTime(2011, 05, 12)
                         });
        };

        Because of = () => diary.AddGigs(gigs, false);

        It the_first_friday_has_a_gig = () => diary.Lines[0].Friday.Gigs.Count().ShouldEqual(1);
        It the_first_saturday_has_a_gig = () => diary.Lines[0].Saturday.Gigs.Count().ShouldEqual(1);
        It only_has_11_gigs = () => diary.GigCount().ShouldEqual(10);
        It has_gigs = () => diary.HasGigs().ShouldBeTrue();
    }
    public class when_getting_an_entry_from_a_populated_diary
    {
        static Diary diary;
        static List<Gig> gigs = new List<Gig>();
        static DiaryEntry entry;
        Establish context = () =>
        {
            diary = new Diary(new DateTime(2011, 04, 15));
            for (var i = 1; i < 11; i++)
            {
                gigs.Add(new Gig
                             {
                                 BandNames = new[]
                                                 {
                                                     new BandName
                                                         {
                                                             Value = "test1" + i
                                                         },
                                                     new BandName
                                                         {
                                                             Value = "test2" + i
                                                         },
                                                     new BandName
                                                         {
                                                             Value = "test3" + i
                                                         }
                                                 },
                                 Date = new DateTime(2011, 04, i),
                                 Price = "free",
                                 StartTime = "18:00",
                                 Venue = "The Pub"
                             });
            }
            diary.AddGigs(gigs, false);
        };

        Because of = () =>
            {
                entry = diary.GetEntry(new DateTime(2011, 04, 1));
            };

        It has_retrieved_an_entry = () => entry.ShouldNotBeNull();
        It has_retrieved_an_entry_for_the_correct_date = () => entry.Date.Date.ShouldEqual(new DateTime(2011, 04, 1));
        It a_non_populated_date_retrieves = () => diary.GetEntry(new DateTime(2010, 04, 1)).ShouldNotBeNull();
    }

    public class diaryentry_testage
    {
        static DiaryEntry entry;

        Establish context = () => entry = new DiaryEntry()
                                              {
                                                  Date = DateTime.Now,
                                                  Gigs = new List<Gig>
                                                             {
                                                                 new Gig
                                                                     {
                                                                        BandNames = new []
                                                                                        {
                                                                                            new BandName
                                                                                                {
                                                                                                    Value = "Band 1"
                                                                                                },
                                                                                            new BandName
                                                                                                {
                                                                                                    Value = "Band 2"
                                                                                                },
                                                                                            new BandName
                                                                                                {
                                                                                                    Value = "Band 2"
                                                                                                }
                                                                                        },
                                                                                        Venue = "The Venue"
                                                                     },
                                                                 new Gig
                                                                     {
                                                                        BandNames = new []
                                                                                        {
                                                                                            new BandName
                                                                                                {
                                                                                                    Value = "Band 4"
                                                                                                },
                                                                                            new BandName
                                                                                                {
                                                                                                    Value = "Band 5"
                                                                                                },
                                                                                            new BandName
                                                                                                {
                                                                                                    Value = "Band 6"
                                                                                                }
                                                                                        },
                                                                                        Venue = "The Other Venue"
                                                                     }
                                                             }
                                              };

        It generates_the_summary_correctly = () => entry.Summary.ShouldEqual("Band 1, Band 2, Band 2 at The Venue | &#10;Band 4, Band 5, Band 6 at The Other Venue");
    }

    public class when_adding_a_duplicate_gig_to_a_diary
    {
        static Diary diary;
        static List<Gig> gigs = new List<Gig>();
        Establish context = () =>
        {
            diary = new Diary(new DateTime(2011, 04, 15));
            for (var i = 1; i < 11; i++)
            {
                gigs.Add(new Gig
                             {
                                 BandNames = new[]
                                                 {
                                                     new BandName
                                                         {
                                                             Value = "test1" + i
                                                         },
                                                     new BandName
                                                         {
                                                             Value = "test2" + i
                                                         },
                                                     new BandName
                                                         {
                                                             Value = "test3" + i
                                                         }
                                                 },
                                 Date = new DateTime(2011, 04, i),
                                 Price = "free",
                                 StartTime = "18:00",
                                 Venue = "The Pub"
                             });
            }
            diary.AddGigs(gigs, false);
        };

        Because of = () => diary.AddGigs(new List<Gig>
                                             {
                                                 new Gig
                                                         {
                                                             BandNames = new[]
                                                                             {
                                                                                 new BandName
                                                                                     {
                                                                                         Value = "another band"
                                                                                     },
                                                                                 new BandName
                                                                                     {
                                                                                         Value = "another band2"
                                                                                     },
                                                                                 new BandName
                                                                                     {
                                                                                         Value = "another band3"
                                                                                     }
                                                                             },
                                                             Date = new DateTime(2011, 04, 1),
                                                             Price = "free",
                                                             StartTime = "18:00",
                                                             Venue = "The Pub"
                                                         }
                                             }, true);

        It has_only_one_gig_on_duplicated_date = () => diary.GetEntry(new DateTime(2011, 04, 1)).Gigs.Count().ShouldEqual(1);
    }
}
using System;
using System.Collections.Generic;
using blackpoolgigs.common.Models;
using blackpoolgigs.web.Models.ViewModels;
using Machine.Specifications;

namespace blackpoolgigs.tests
{
    public class testing_future_gig
    {
        static BandViewModel vm;
        static List<Gig> future;
        static List<Gig> historic;
        Establish context = () =>
            {
                var gigs = new List<Gig>();
                for (var i = -5; i < 5; i++)
                {
                    gigs.Add(new Gig
                                 {
                                     Date = DateTime.Now.AddDays(i)
                                 });
                }
                vm = new BandViewModel()
                         {
                             Diary = new BandDiary
                                         {
                                             Gigs = gigs
                                         }
                         };
            };

        Because of = () =>
            {
                future = vm.FutureGigs;
                historic = vm.HistoricGigs;
            };

        It has_the_correct_future_gigs = () => future.Count.ShouldEqual(5);
        It has_the_correct_historic_gigs = () => historic.Count.ShouldEqual(5);
    }
}
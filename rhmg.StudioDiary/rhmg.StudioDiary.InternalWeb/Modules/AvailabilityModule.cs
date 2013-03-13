using System;
using Nancy;
using rhmg.StudioDiary.InternalWeb.ViewModels;
using rhmg.StudioDiary.Raven;

namespace rhmg.StudioDiary.InternalWeb.Modules
{

    public class AvailabilityModule : NancyModule
    {
        public AvailabilityModule(IRavenSessionProvider store)
        {
            using (var session = store.GetSession())
            {
                Get["/"] = parameters =>
                               {
                                   var thisWeek = DiaryManager.WeekToAViewFor(DateTime.Now, session);
                                   return View[new AvailabilityWeekModel
                                   {
                                       PreviousWeekMonday = thisWeek.MondayDate.AddDays(-7),
                                       NextWeekMonday = thisWeek.MondayDate.AddDays(7),
                                       ThisWeek = thisWeek,
                                       Rooms = Room.All(session),
                                       Products = Product.All(session)
                                   }];
                               };
                Get[@"/(?<day>[\d]{1,2})/(?<month>[\d]{1,2})/(?<year>[\d]{1,4})/"] = parameters =>
                                                   {
                                                       var date = new DateTime(parameters.year, parameters.month, parameters.day);
                                                       var thisWeek = DiaryManager.WeekToAViewFor(date, session);
                                                       return View[new AvailabilityWeekModel
                                                       {
                                                           PreviousWeekMonday = thisWeek.MondayDate.AddDays(-7),
                                                           NextWeekMonday = thisWeek.MondayDate.AddDays(7),
                                                           ThisWeek = thisWeek,
                                                           Rooms = Room.All(session),
                                                           Products = Product.All(session)
                                                       }];
                                                   };
            }
        }
    }
}
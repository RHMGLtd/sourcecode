using System;
using Nancy;
using Raven.Client;
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
                Get["/"] = parameters => GetDiaryForWeek(session, DateTime.Now.Date);
                Get[@"/(?<day>[\d]{1,2})/(?<month>[\d]{1,2})/(?<year>[\d]{1,4})/"] = parameters =>
                                                   {
                                                       var date = new DateTime(parameters.year, parameters.month, parameters.day);
                                                       return GetDiaryForWeek(session, date);
                                                   };
            }
        }

        dynamic GetDiaryForWeek(IDocumentSession session, DateTime date)
        {
            var thisWeek = DiaryManager.WeekToAViewFor(date, session);
            return View[new AvailabilityWeekModel
                            {
                                PreviousWeekMonday = thisWeek.MondayDate.AddDays(-7),
                                NextWeekMonday = thisWeek.MondayDate.AddDays(7),
                                ThisWeek = thisWeek,
                                Rooms = Room.All(session),
                                Products = Product.All(session)
                            }];
        }
    }
}
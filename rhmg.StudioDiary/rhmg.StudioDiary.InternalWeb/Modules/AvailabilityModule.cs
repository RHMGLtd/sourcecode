using System;
using System.Globalization;
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
                Get["/month"] = parameters =>
                                    {
                                        var thisMonth = DiaryManager.FullMonthToAViewFor(DateTime.Now.Date, session);
                                        return View[new AvailabilityMonthModel
                                                        {
                                                            ThisMonth = thisMonth,
                                                            MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month)
                                                        }];
                                    };
                Get[@"/(?<day>[\d]{1,2})/(?<month>[\d]{1,2})/(?<year>[\d]{1,4})/"] = 
                    parameters => GetDiaryForWeek(session, new DateTime(parameters.year, parameters.month, parameters.day));
            }
        }

        dynamic GetDiaryForWeek(IDocumentSession session, DateTime date)
        {
            var thisWeek = DiaryManager.WeekToAViewFor(date, session);
            return View[new AvailabilityWeekModel
                            {
                                ThisWeek = thisWeek,
                                Rooms = Room.All(session),
                                Products = Product.All(session)
                            }];
        }
    }
}
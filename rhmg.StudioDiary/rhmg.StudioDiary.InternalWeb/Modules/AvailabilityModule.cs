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
                Get["/month"] = parameters => GetDiaryForMonth(session, DateTime.Now);
                Get["/day"] = parameters => GetDiaryForDay(session, DateTime.Now.Date);
                Get[@"/day/(?<day>[\d]{1,2})/(?<month>[\d]{1,2})/(?<year>[\d]{1,4})/"] =
                    parameters => GetDiaryForDay(session, new DateTime(parameters.year, parameters.month, parameters.day));
                Get["/month/{month}/{year}"] = parameters => GetDiaryForMonth(session, new DateTime(parameters.year, parameters.month, 1));
                Get[@"/(?<day>[\d]{1,2})/(?<month>[\d]{1,2})/(?<year>[\d]{1,4})/"] =
                    parameters => GetDiaryForWeek(session, new DateTime(parameters.year, parameters.month, parameters.day));
            }
        }

        static AvailabilityDayModel GetDiaryForDay(IDocumentSession session, DateTime date)
        {
            return new AvailabilityDayModel
                       {
                           ThisDay = DiaryManager.DayCheck(date, session),
                           AllRooms = Room.All(session)
                       };
        }

        dynamic GetDiaryForMonth(IDocumentSession session, DateTime dateTime)
        {
            var thisMonth = DiaryManager.FullMonthToAViewFor(dateTime.Date, session);
            return View[new AvailabilityMonthModel
                            {
                                ThisMonth = thisMonth,
                                MonthNumber = dateTime.Month,
                                MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month)
                            }];
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
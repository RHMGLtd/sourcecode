using System;
using System.Linq;
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
                Get["/"] = parameters => View[new AvailabilityWeekModel
                                                  {
                                                      ThisWeek = DiaryManager.WeekToAViewFor(DateTime.Now, session),
                                                      Rooms = session.Query<Room>().ToList()
                                                  }];
            }
            //Get["/"] = parameters => "Hello World";
        }
    }
}
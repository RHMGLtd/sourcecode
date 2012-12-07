using System.Web.Routing;
using blackpoolgigs.web.Urls;
using Snooze.Routing;

namespace blackpoolgigs.web
{
    public class RouteRegistration : IRouteRegistration
    {
        public void Register(RouteCollection routes)
        {
            routes.Map<HomeUrl>(u => "");
            routes.Map<ContactUrl>(u => "Contact");
            routes.Map<DiaryUrl>(u => "diary");
            routes.Map<TodayUrl>(u => "today");
            routes.Map<ThanksUrl>(u => "thanks");
            routes.Map<AboutUrl>(u => "about");
            routes.Map<BandLookingForAGigUrl>(u => "bandlookingforagig");
            routes.Map<StolenGearListUrl>(u => "stolengear");
            routes.Map<StolenGearUrl>(u => "stolengear/" + u.Id);

            routes.Map<VenuesUrl>(u => "Venues/");
            routes.Map<VenuesMapUrl>(u => "Venues/Map");
            routes.Map<VenueUrl>(u => "Venues/" + u.Venue);
            routes.Map<VenueMapUrl>(u => "Venues/" + u.Venue + "/Map");
            routes.Map<GigUrl>(u => "Venues/" +  u.Venue + "/" +u.Date + "/" + u.Month + "/" + u.Year);
            routes.Map<VenueUrl>(u => "Venues/" + u.Venue.CatchAll());

            routes.Map<BandsUrl>(u => "bands");
            routes.Map<BandUrl>(u => "bands/" + u.BandName);
            routes.Map<ClaimBandUrl>(u => "bands/" + u.BandName + "/claim");
            routes.Map<ClaimThanksUrl>(u => "bands/" + u.BandName + "/thanks");

            routes.Map<YearUrl>(u => u.Year.ToString());
            routes.Map<MonthUrl>(u => u.Month + "/" + u.Year);
            routes.Map<DayUrl>(u => u.Date + "/" + u.Month + "/" + u.Year);
            routes.Map<DayMapUrl>(u => u.Date + "/" + u.Month + "/" + u.Year + "/map");

            // catchalls
            routes.Map<ErrorUrl>(u => u.ContentPath.CatchAll());
        }
    }
}
using rhmg.AdministrateBlackpoolGigs.web.Urls;
using Snooze.Routing;
using System.Web.Routing;

namespace rhmg.AdministrateBlackpoolGigs.web
{
    public class RouteRegistration : IRouteRegistration
    {
        public void Register(RouteCollection routes)
        {
            routes.Map<HomeUrl>(u => "");
            routes.Map<AddUrl>(u => "Add");

            routes.Map<AddRepeatingGigUrl>(u => "RepeatingGig/Add");
            routes.Map<RepeatingGigUrl>(u => "RepeatingGig/" + u.Id);
            routes.Map<RepeatingGigsUrl>(u => "RepeatingGigs");

            routes.Map<AdminPanelUrl>(u => "adminpanel");
            routes.Map<AddCreatedDatesUrl>(u => "adminpanel/addcreateddates");
            routes.Map<DataCleanseSpacesUrl>(u => "adminpanel/datacleanse/spaces");

            routes.Map<LookupUrl>(u => "Lookup/" + u.Which);
            routes.Map<ExportUrl>(u => "export");
            routes.Map<ListUrl>(u => "list");
            routes.Map<EditUrl>(u => "gig/" + u.RecordId);
            routes.Map<CheckDateUrl>(u => "checkdate");
            routes.Map<VenuesUrl>(u => "venues");
            routes.Map<EditVenueUrl>(u => "venues/" + u.VenueName);
            routes.Map<VenueMainImageUrl>(u => "venues/" + u.VenueName + "/mainimage");
            routes.Map<MetadataUrl>(u => "metadata/" + u.Id);

            routes.Map<BandsUrl>(u => "bands");
            routes.Map<BandUrl>(u => "bands/" + u.BandName);

            routes.Map<StolenGearListUrl>(u => "stolengearlist");
            routes.Map<AddStolenGearUrl>(u => "stolengear");
            routes.Map<StolenGearUrl>(u => "stolengear/" + u.Id);
        }
    }

}
using System;
using System.Web.Routing;
using rhmgOnline.Urls;
using Snooze.Routing;

namespace rhmgOnline
{
    public class RouteRegistration : IRouteRegistration
    {
        public void Register(RouteCollection routes)
        {
            routes.Map<HomeUrl>(u => "");
        }
    }
}
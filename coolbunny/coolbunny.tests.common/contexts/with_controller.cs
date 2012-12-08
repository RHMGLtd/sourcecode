using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using coolbunny.tests.common.Fakes;
using MvcContrib.TestHelper.Fakes;
using nVentive.Umbrella.Extensions;
using Snooze;
using Snooze.Routing;

namespace coolbunny.tests.common.contexts
{
    public class with_controller<TResource, TController> : with_service<TController>
        where TController : ResourceController
    {
        protected static ResourceResult result;
        static with_controller()
        {
            if (RouteTable.Routes.Empty())
                RouteTable.Routes.FromAssemblyWithType<TController>();
        }

        protected static TResource Resource
        {
            get
            {
                if (result is ResourceResult)
                    return (TResource)(result).Resource;

                throw new ArgumentException("This is not a resource result, perhaps you're returning a redirect ?");
            }
        }
        protected static void GET(string uri, params object[] @params)
        {
            EnsureControllerContext(uri, "GET");
            var routeData = Service.ControllerContext.RouteData;

            InvokeAction("GET", routeData, @params, GetQueryString(uri));
        }
        protected static void POST(string uri, params object[] @params)
        {
            EnsureControllerContext(uri, "POST");
            var routeData = Service.ControllerContext.RouteData;

            InvokeAction("POST", routeData, @params, GetQueryString(uri));
        }

        protected static void PUT(string uri, params object[] @params)
        {
            EnsureControllerContext(uri, "PUT");
            var routeData = Service.ControllerContext.RouteData;

            InvokeAction("PUT", routeData, @params, GetQueryString(uri));
        }
        protected static void DELETE(string uri, params object[] @params)
        {
            EnsureControllerContext(uri, "DELETE");
            var routeData = Service.ControllerContext.RouteData;
            InvokeAction("DELETE", routeData, @params, GetQueryString(uri));
        }

        static NameValueCollection GetQueryString(string path)
        {
            return
                HttpUtility.ParseQueryString(
                    new Uri("http://local.com" + (path.StartsWith("/") ? path : "/" + path)).Query);
        }
        protected static void InvokeAction(string httpMethod, RouteData route, object[] additionalParameters,
                                           NameValueCollection queryString)
        {
            var urlType = route.Route.GetType().GetGenericArguments()[0];

            var methods =
                from m in typeof(TController).GetMethods()
                where m.Name.Equals(httpMethod, StringComparison.OrdinalIgnoreCase)
                let parameters = m.GetParameters()
                where parameters.Length > 0
                      && parameters[0].ParameterType.Equals(route.Route.GetType().GetGenericArguments()[0])
                select m;

            if (methods.Count() == 0)
                throw new InvalidOperationException("No action for uri " + urlType.Name);


            var args = new List<object>(new[] { FromContext(route, queryString) });
            args.AddRange(additionalParameters);

            var method = methods.First();

            var arguments = ResolveParameters(method.GetParameters(), args);

            result = (ResourceResult)method.Invoke(Service, arguments);
        }
        protected static Url FromContext(RouteData data, NameValueCollection queryString)
        {
            var url = Activator.CreateInstance(data.Route.GetType().GetGenericArguments()[0]);

            AssignParentUrl(url, data, queryString);

            AssignUrlProperties(data, url, queryString);
            return (Url)url;
        }

        static object[] ResolveParameters(IList<ParameterInfo> parameters, ICollection<object> values)
        {
            if (parameters.Count == values.Count)
                return values.ToArray();

            if (parameters.Count < values.Count)
                values.Take(parameters.Count).ToArray();

            for (var i = values.Count; i < parameters.Count; i++)
            {
                var type = parameters[i].ParameterType;
                var value = type.IsValueType ? Activator.CreateInstance(type) : null;
                values.Add(value);
            }
            return values.ToArray();
        }

        static void AssignUrlProperties(RouteData data, object url, NameValueCollection queryString)
        {
            foreach (var v in data.Values.Where(v => url.GetType().GetProperty(v.Key) != null))
            {
                var pInfo = url.GetType().GetProperty(v.Key);

                pInfo.SetValue(url, v.Value.Conversion().To(pInfo.PropertyType), null);
            }

            foreach (var key in queryString.AllKeys.Where(k => url.GetType().GetProperty(k) != null))
            {
                var pInfo = url.GetType().GetProperty(key);

                pInfo.SetValue(url, queryString[key].Conversion().To(pInfo.PropertyType), null);
            }
        }

        static void AssignParentUrl(object url, RouteData data, NameValueCollection queryString)
        {
            if (!url.GetType().BaseType.IsGenericType)
                return;

            var parentType = url.GetType().BaseType
                .GetGenericArguments()[0];

            var parentUrl = Activator.CreateInstance(parentType);

            AssignUrlProperties(data, parentUrl, queryString);

            url.Reflection().Set("Parent", parentUrl);
        }

        static void EnsureControllerContext(string path, string method)
        {
            var uri = string.IsNullOrEmpty(path)
                          ? new Uri("http://local.com")
                          : new Uri("http://local.com" + (path.StartsWith("/") ? path : "/" + path));
            var absolutePath = uri.AbsolutePath;
            var qs = uri.Query;
            var queryStringParms = HttpUtility.ParseQueryString(qs);
            var httpContext = new FakeHttpContext("~" + absolutePath, null, null, queryStringParms, null, null);

            var routeData = RouteTable.Routes.GetRouteData(httpContext);

            httpContext.SetRequest(new FakerHttpRequest(httpContext, "~" + absolutePath, method, uri,
                                                        new Uri("http://local.com"), null, queryStringParms, null));

            if (routeData == null)
                throw new InvalidOperationException("No route for " + uri);

            Service.ControllerContext = new ControllerContext { HttpContext = httpContext, RouteData = routeData, Controller = Service };
        }
    }
}
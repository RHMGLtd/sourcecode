using System;
using System.Collections.Specialized;
using System.Web;
using MvcContrib.TestHelper.Fakes;

namespace coolbunny.tests.common.Fakes
{
    public class FakerHttpRequest : FakeHttpRequest
    {
        readonly HttpContextBase context;


        public FakerHttpRequest(HttpContextBase context, string relativeUrl, string method, Uri url, Uri urlReferrer,
                                NameValueCollection formParams, NameValueCollection queryStringParams,
                                HttpCookieCollection cookies)
            : base(relativeUrl, method, url, urlReferrer, formParams, queryStringParams, cookies)
        {
            this.context = context;
        }


        public override bool IsAuthenticated
        {
            get
            {
                return context != null && context.User != null && context.User.Identity != null &&
                       context.User.Identity.IsAuthenticated;
            }
        }

        public override NameValueCollection ServerVariables
        {
            get
            {
                base.ServerVariables.Add("HTTP_HOST", "http://localhost");
                return base.ServerVariables;
            }
        }

    }
}
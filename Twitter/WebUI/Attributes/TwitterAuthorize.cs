using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebUI.Attributes
{
    public class TwitterAuthorize : System.Web.Mvc.AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Session["CurrentUser"] == null)
                filterContext.Result = new RedirectToRouteResult(
                                    new RouteValueDictionary(new { controller = "Home", action = "Index" }));
        }
    }
}
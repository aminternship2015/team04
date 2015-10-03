using System;
using System.Web;
using System.Web.Mvc;

namespace Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,AllowMultiple=true)]
    public class LoginVerification : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
           if(HttpContext.Current.Session["CurrentUser"] == null)
           {
               filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Account", action = "Login" }));
           }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using WebUI.App_Start;
using DAL.Entities;
using Models;
using StaticLogger;

namespace WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DependencyConfig.Config();
            Logger.InitLogger();
        }


        protected void Session_Start(Object sender, EventArgs e)
        {
            UserViewModel currentUser = null;
            HttpContext.Current.Session.Timeout = 800;
            HttpContext.Current.Session.Add("CurrentUser", currentUser);
        }
    }
}
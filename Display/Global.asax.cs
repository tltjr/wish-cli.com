using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Display.Data;

namespace Display
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "RSS",
                "rss",
                new {controller = "Blog", action = "Rss"});

            routes.MapRoute(
                "Feed",
                "Feed",
                new {controller = "Blog", action = "Rss"});

            routes.MapRoute(
                "Post",
                "Blog/{year}/{month}/{slug}",
                new {controller = "Blog", action = "Single" });

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            RegisterUser();
            //RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
        }

        private static void RegisterUser()
        {
            var repo = new UserRepository();
            var user = repo.GetUser("tltjr");
            if (null != user) return;
            var password = ConfigurationManager.AppSettings["password"];
            repo.CreateUser("tltjr", password, "");
        }

    }
}
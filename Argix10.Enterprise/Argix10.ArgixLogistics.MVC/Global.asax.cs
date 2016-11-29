using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Argix {
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication:System.Web.HttpApplication {
        //Members

        //Interface
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(name: "Default", url: "{controller}/{action}/{id}", defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }, namespaces: new string[]{"Argix.Controllers"});
            //routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home",action = "Index",id = UrlParameter.Optional }, new[] { "Argix.Controllers" } );

        }

        protected void Application_Start() {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
        protected void Session_Start() {
            //Redirect mobile users to the mobile home page    
            HttpRequest httpRequest = HttpContext.Current.Request;
            if (httpRequest.Browser.IsMobileDevice) {
                //Redirect to Mobile Home unless the mobile user is requesting a mobile page from the start
                bool isOnMobilePage = httpRequest.Url.PathAndQuery.StartsWith("/Mobile/",StringComparison.OrdinalIgnoreCase);
                if (!isOnMobilePage) {
                    //Could also add special logic to redirect from certain recognized pages to the mobile 
                    //equivalents of those pages (where they exist). For example:
                    // if(HttpContext.Current.Handler is UserRegistration)
                    //     redirectTo = "~/Mobile/Register.aspx";
                    HttpContext.Current.Response.Redirect("~/Mobile/Home");
                }
            }
        }
    }
}
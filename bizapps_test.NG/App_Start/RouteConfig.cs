using System.Web.Mvc;
using System.Web.Routing;

namespace bizapps_test.NG
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new
                {
                    serverRoute = new App_Start.ServerRouteConstraint(url =>
                    {
                        return false;
                    } )
                }
            );

            routes.MapRoute(
                name: "angular",
                url: "{*url}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );


        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RESTful_Flight_Simulator
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            //
            // Dynamic routes
            //

            // Display route of plane as it flies (update per interval (in seconds))
            routes.MapRoute(
                name: "Route",
                url: "{controller}/{ip}/{port}/{interval}",
                defaults: new { controller = "display", action = "DisplayLiveRoute", ip = "127.0.0.1", port = 5400, interval = 1 }
            );

            // Determine whether to route to DisplayLocation or DisplaySavedRoute
            routes.MapRoute(
                name: "Display",
                url: "{controller}/{param1}/{param2}/",
                defaults: new { controller = "display", action = "DetermineRoute" }
            );

            // Save route of plane as it flies into a file
            routes.MapRoute(
                name: "Save",
                url: "{controller}/{ip}/{port}/{interval}/{duration}/{flight_id}",
                defaults: new { controller = "save", action = "SaveRoute", ip = "127.0.0.1", port = 5400, interval = 1, duration = 10, flight_id = "flight1" }
            );


            //
            // Hardcoded routes
            //

            // Display current location of plane
            routes.MapRoute(
                name: "Dot",
                url: "{controller}/127.0.0.1/5400",
                defaults: new { controller = "display", action = "DisplayLocation" }
            );

            // Load saved route from memory and animate it (4 updates per second)
            routes.MapRoute(
                    name: "Load",
                    url: "{controller}/flight1/4",
                    defaults: new { controller = "display", action = "DisplaySavedRoute" }
            );

            // Save route of plane as it flies into a file
            routes.MapRoute(
                name: "Save",
                url: "{controller}/127.0.0.1/5400/4/10/flight1",
                defaults: new { controller = "save", action = "SaveRoute" }
            );
        }
    }
}

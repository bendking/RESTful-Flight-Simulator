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
            
            // Display current location of plane
            routes.MapRoute(
                name: "Dot",
                url: "{controller}/127.0.0.1/5400/",
                defaults: new { controller = "Display", action = "DisplayLocation" }
            );

            // Display route of plane as it flies (4 updates per second)
            routes.MapRoute(
                name: "Route",
                url: "{controller}/127.0.0.1/5400/4",
                defaults: new { controller = "Display", action = "DisplayLiveRoute" }
            );

            // Load saved route from memory and animate it (4 updates per second)
            routes.MapRoute(
                    name: "Load",
                    url: "{controller}/flight1/4",
                    defaults: new { controller = "Display", action = "DisplaySavedRoute" }
                );

            // Save route of plane as it flies into a file (4 updates per second, for 10 seconds)
            routes.MapRoute(
                name: "Save",
                url: "{controller}/127.0.0.1/5400/4/10/flight1",
                defaults: new { controller = "Save", action = "SaveRoute" }
            );
        }
    }
}

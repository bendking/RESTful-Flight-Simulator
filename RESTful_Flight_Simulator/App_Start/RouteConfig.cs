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


            routes.MapRoute(
                name: "first",
                url: "{controller}/{action}"
               
            );

            //
            // Dynamic routes
            //

            // Save route of plane as it flies into a file
            routes.MapRoute(
                name: "SaveRoute",
                url: "{controller}/{ip}/{port}/{interval}/{duration}/{file}",
                defaults: new { controller = "save", action = "SaveRoute" }
            );

            // Display route of plane as it flies (update per interval (in seconds))
            routes.MapRoute(
                "LiveRoute",
                "{controller}/{ip}/{port}/{interval}",
                new { controller = "display", action = "DisplayLiveRoute"},
                new {ip = @"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$"}
            );

            // DisplayLocation
            routes.MapRoute(
                "CurrentLocation",
                "{controller}/{ip}/{port}/",
                new { controller = "display", action = "DisplayLocation"},
                new { ip = @"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$" }
            );

            // DisplaySavedRoute
            routes.MapRoute(
                name: "SavedRoute",
                url: "{controller}/{file}/{interval}/",
                defaults: new { controller = "display", action = "DisplaySavedRoute"}
            );

            // for debug 
            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{ip}/{port}",
               defaults: new { controller = "display", action = "DisplayLocation", ip = "127.0.0.1", port = 5400 }
           );

            //
            // Map files
            //

            // Map saved routes
            routes.MapRoute(
               name: "ReturnRoute",
               url: "{controller}/{file_name}",
               defaults: new { controller = "save", action = "ReturnContent" }
           );

            // Map images
            routes.MapRoute(
               name: "ReturnImage",
               url: "{controller}/{image_path}",
               defaults: new { controller = "files", action = "ReturnImage" }
           );



        }
    }
}

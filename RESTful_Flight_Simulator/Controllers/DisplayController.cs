using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RESTful_Flight_Simulator.Models;

namespace RESTful_Flight_Simulator.Controllers
{
    public class displayController : Controller
    {

        /*
        // GET
        public ActionResult DetermineRoute(string param1, int param2)
        {
            IPAddress ip;
            // Determine if first parameter is an IP or a file name
            if (IPAddress.TryParse(param1, out ip)) {
                return DisplayLocation(param1, param2);
            }
            // Else
            return DisplaySavedRoute(param1, param2);
        }
        */

        [HttpGet]
        public ActionResult DisplayLocation(string ip, int port)
        {
            // TODO: Insert ip and port as necessary to model
            displayModel model = new displayModel(ip, ""+port, null, null, null);
            return View(model);
        }

        [HttpGet]
        public ActionResult DisplayLiveRoute(string ip, int port)
        {
            // TODO: Insert ip and port as necessary to model
            displayModel model = new displayModel(ip, ""+port, null, null, null);
            return View(model);
        }

        [HttpGet]
        public ActionResult DisplaySavedRoute(string fileName, int interval)
        {
            // TODO: Insert file name and interval as necessary to model
            displayModel model = new displayModel(null, null,fileName, ""+interval, null);
            return View(model);
        }
    }
}
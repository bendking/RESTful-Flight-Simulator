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
        [HttpGet]
        public ActionResult DisplayLocation(string ip, int port)
        {
            displayModel model = new displayModel(ip, ""+port, null, null, null);
            return View(model);
        }

        [HttpGet]
        public ActionResult DisplayLiveRoute(string ip, int port)
        {
            displayModel model = new displayModel(ip, ""+port, null, null, null);
            return View(model);
        }

        [HttpGet]
        public ActionResult DisplaySavedRoute(string fileName, int interval)
        {

            displayModel model = new displayModel(null, null,fileName, ""+interval, null);
            return View(model);
        }
    }
}
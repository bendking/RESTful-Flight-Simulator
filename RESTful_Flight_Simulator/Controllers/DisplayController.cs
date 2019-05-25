using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RESTful_Flight_Simulator.Controllers
{
    public class displayController : Controller
    {
        // GET
        public ActionResult DisplayLocation()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DisplayLiveRoute()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DisplaySavedRoute()
        {
            return View();
        }
    }
}
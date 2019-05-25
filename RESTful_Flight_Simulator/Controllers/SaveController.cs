using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RESTful_Flight_Simulator.Controllers
{
    public class saveController : Controller
    {
        // GET: Save
        public ActionResult SaveRoute()
        {
            // TODO: Save route - 4 times per second, for 10 seconds
            return View(); // Should return nothing
        }
    }
}
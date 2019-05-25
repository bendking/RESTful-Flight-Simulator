using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RESTful_Flight_Simulator.Controllers
{
    public class DisplayController : Controller
    {
        // GET: Direction
        public ActionResult Index()
        {
            return View();
        }
    }
}
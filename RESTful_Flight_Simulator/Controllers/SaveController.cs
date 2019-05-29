using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using RESTful_Flight_Simulator.Models;

namespace RESTful_Flight_Simulator.Controllers
{
    public class saveController : Controller
    {
        // GET: Save
        public ActionResult SaveRoute()
        {
            saveModel model = new saveModel();
            // Start saving route
            Thread thread = new Thread(model.SaveRoute);
            thread.Start();
            // Return view
            return View(model);
        }
    }
}
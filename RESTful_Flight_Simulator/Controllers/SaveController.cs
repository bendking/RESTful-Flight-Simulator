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
        public ActionResult SaveRoute(string ip, int port, float interval, int duration, string file)
        {
            saveModel model = new saveModel(ip, port, interval, duration, file);
            // Save route
            //model.SaveRoute();
            // Save route in the background
            //Thread thread = new Thread(model.SaveRoute);
            //thread.Start();
            // Show view
            return View(model);
        }

        public String ReturnContent(string file)
        {
            System.Diagnostics.Debug.WriteLine("Getting file: " + @"../Views/Save/Routes/" + @file);
            return System.IO.File.ReadAllText(@"../Views/Save/Routes/" + @file);
        }
    }
}
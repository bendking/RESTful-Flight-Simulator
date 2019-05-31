using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RESTful_Flight_Simulator.Models;

namespace RESTful_Flight_Simulator.Controllers
{
    public class dataController : Controller
    {
        public ActionResult Index()
        {
            return View ();
        }

        [HttpGet]
        public string GetCoordinates(string vars)
        {
            LonLat x = FacadeModel.GetInstance().GetCoordinates("vars",int.Parse(vars));
            return x.ToXml();
        }

        [HttpGet]
        public string GetCoordinatesAndSave(string vars)
        {
            LonLat x = FacadeModel.GetInstance().GetCoordinates("vars", int.Parse(vars));
            return x.ToXml();
        }


        private string CoordinatesFromFile(string file, int index)
        {
            return "";
        }
        private string CoordinatesFromServer(string ip, int port)
        {
            return "";
        }


    }
}

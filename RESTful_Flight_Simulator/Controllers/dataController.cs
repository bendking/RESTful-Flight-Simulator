using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
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
            List<string> l = extractVars(vars);
            var regex = "^(?:[0-9]{1,3}\\.){3}[0-9]{1,3}$";

            var match = Regex.Match(l[0], regex, RegexOptions.IgnoreCase);

            LonLat x;
            if (match.Success)
            {
                string ip = l[0];
                int port = 5400;
                int.TryParse(l[1], out port);
              
                x = CoordinatesFromServer(ip, port);
            }
            else {
                string file = l[0];
                int index = 0;
                int.TryParse(l[1], out index);

                x = CoordinatesFromFile(file, index);
            }
            return x.ToXml();
        }

        [HttpGet]
        public string GetCoordinatesAndSave(string vars)
        {
            LonLat x = FacadeModel.GetInstance().GetCoordinatesFromServer("vars", int.Parse(vars));
            return x.ToXml();
        }


        private LonLat CoordinatesFromFile(string file, int index)
        {
            return FacadeModel.GetInstance().GetCoordinatesFromFile(file, index); ;
        }
        private LonLat CoordinatesFromServer(string ip, int port)
        {
            return FacadeModel.GetInstance().GetCoordinatesFromServer(ip, port);
        }

        private List<string> extractVars(string vars)
        {
            List<string> l = vars.Split(',').ToList<string>();
            l.Reverse();
            return l;
        }
    }
}

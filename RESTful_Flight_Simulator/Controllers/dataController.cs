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
            List<string> list = extractVars(vars);
            string regex = "^(?:[0-9]{1,3}\\.){3}[0-9]{1,3}$";
            Match match = Regex.Match(list[0], regex, RegexOptions.IgnoreCase);

            LonLat x;
            if (match.Success)
            {
                string ip = list[0];
                int port = 5400;
                int.TryParse(list[1], out port);
                x = CoordinatesFromServer(ip, port);
            }
            else
            {
                string file = list[0];
                int index = 0;
                int.TryParse(list[1], out index);
                x = CoordinatesFromFile(file, index);
            }

            if (x != null) {
                return x.ToXml();
            }
            else {
                return LonLat.NullLonLatToXML();
            }
        }

        [HttpGet]
        public string GetCoordinatesAndSave(string vars)
        {
            List<string> l = extractVars(vars);
            // Default value
            int port = 5400;
            int.TryParse(l[1], out port);
            LonLat x = FacadeModel.GetInstance().GetCoordinatesAndSave(l[0], port, l[2]);

            if (x == null) {
                return LonLat.NullLonLatToXML();
            }
            // Else
            return x.ToXml();
        }


        private LonLat CoordinatesFromFile(string file, int index) {
            return FacadeModel.GetInstance().GetCoordinatesFromFile(file, index);

        }
        private LonLat CoordinatesFromServer(string ip, int port) {
            return FacadeModel.GetInstance().GetCoordinatesFromServer(ip, port);
        }

        private List<string> extractVars(string vars) {
            return vars.Split(',').ToList<string>();
        }
    }
}

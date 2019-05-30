using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RESTful_Flight_Simulator.Controllers
{
    public class filesController : Controller
    {
        // GET: files
        public ActionResult ReturnImage(string image_path)
        {
            System.Diagnostics.Debug.WriteLine("Image path requested: " + "../src/" + @image_path + " , Image path computed: " + "image/" + System.IO.Path.GetExtension(@image_path));
            return base.File("../src/" + @image_path, "image/" + System.IO.Path.GetExtension(@image_path));
        }
    }
}
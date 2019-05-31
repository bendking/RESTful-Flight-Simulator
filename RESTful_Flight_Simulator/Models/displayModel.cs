using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTful_Flight_Simulator.Models
{
    public class displayModel
    {
        public string ip;
        public string port;
        public string fileName;
        public string interval;
        public string duration;

        public displayModel(string _ip, string _port, string _fileName, string _interval, string _duration)
        {
            ip = _ip;
            fileName = _fileName;
            port = _port;
            interval = _interval;
            duration = _duration;
        }

      
    }
}
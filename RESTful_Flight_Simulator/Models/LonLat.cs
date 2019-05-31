using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Text;

namespace RESTful_Flight_Simulator.Models
{
    public class LonLat
    {
        public double Lon { get; set; }
        public double Lat { get; set; }

        public LonLat(double _Lon, double _Lat)
        {
            Lon = _Lon;
            Lat = _Lat;
        }

        public string ToXml()
        {
            //Initiate XML stuff
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlWriter writer = XmlWriter.Create(sb, settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("Coordinates");

            writer.WriteElementString("Lon", this.Lon.ToString());
            writer.WriteElementString("Lat", this.Lat.ToString());

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            return sb.ToString();
        }

        public string toString()
        {
            return "" + Lon + "," + Lat;
        }

        public static LonLat FromString(string s)
        {
            List<string> x = s.Split(',').ToList<string>();
            //x.Reverse();

            return new LonLat(int.Parse(x[0]), int.Parse(x[1]));
        }

        public static string NullLonLatToXML()
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlWriter writer = XmlWriter.Create(sb, settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("Coordinates");

            writer.WriteElementString("Lon", "EOF");
            writer.WriteElementString("Lat", "EOF");

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            return sb.ToString();
        }
    }
}

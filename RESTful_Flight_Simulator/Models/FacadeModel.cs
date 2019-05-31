using System;
using RESTful_Flight_Simulator.Models;
namespace RESTful_Flight_Simulator.Models
{
    public class FacadeModel
    {

        private static readonly FacadeModel _singleton = new FacadeModel();

        public static FacadeModel GetInstance()
        {
            return _singleton;
        }

        private FacadeModel()
        {

        }

        public LonLat GetCoordinatesFromServer(string ip, int port)
        {
            return new LonLat(50, 50);
        }

        public LonLat GetCoordinatesAndSave(string ip, int port, string file)
        {
            LonLat x = GetCoordinatesFromServer(ip, port);
            // TODO - save x!!!
            return x;
        }

        public LonLat GetCoordinatesFromFile(string file, int index)
        {
            // TODO - get next coordinate from file!!!
            return new LonLat(50, 50);
        }

    }
}

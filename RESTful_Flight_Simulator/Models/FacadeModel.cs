using System;
using RESTful_Flight_Simulator.Models;

//TODO - mutex!

namespace RESTful_Flight_Simulator.Models
{
    public class FacadeModel
    {
        private DataRequester dataRequester;
        private FileManagerModel fileManager;
        private static readonly FacadeModel _singleton = new FacadeModel();

        public static FacadeModel GetInstance()
        {
            return _singleton;
        }

        private FacadeModel()
        {
            fileManager = new FileManagerModel();
            dataRequester = new DataRequester();
        }

        public LonLat GetCoordinatesFromServer(string ip, int port)
        {
            dataRequester.ChangeConnectionIfNeeded(ip, port);
            double[] arr = dataRequester.RequestData();
            LonLat x = new LonLat(arr[0], arr[1]);
            return x;
        }

        public LonLat GetCoordinatesAndSave(string ip, int port, string file)
        {
            LonLat x = GetCoordinatesFromServer(ip, port);
            if (x == null)
            {
                return null;
            }
            fileManager.AppendFile(x, file);
            return x;
        }

        public LonLat GetCoordinatesFromFile(string file, int index)
        {
            // TODO - get next coordinate from file!!!
            return fileManager.GetFromFile(index, file);
        }

    }
}

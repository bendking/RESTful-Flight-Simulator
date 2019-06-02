using System;
using RESTful_Flight_Simulator.Models;



namespace RESTful_Flight_Simulator.Models
{
    public class FacadeModel
    {
        private DataRequester dataRequester;
        private FileManager fileManager;
        private readonly object syncLock;
        // Initiate singleston on program start
        private static readonly FacadeModel _singleton = new FacadeModel();

        public static FacadeModel GetInstance()
        {
            return _singleton;
        }

        private FacadeModel()
        {
            fileManager = new FileManager();
            dataRequester = new DataRequester();
            syncLock = new object();
        }

        public LonLat GetCoordinatesFromServer(string ip, int port)
        {
            double[] arr;
            lock (syncLock)
            {
                /* critical code */
                dataRequester.ChangeConnectionIfNeeded(ip, port);
                arr = dataRequester.RequestData();
            }
            LonLat x = new LonLat(arr[0], arr[1]);
            return x;
        }

        public LonLat GetCoordinatesAndSave(string ip, int port, string file)
        {
            LonLat x = GetCoordinatesFromServer(ip, port);
            if (x == null) {
                return null;
            }

            fileManager.AppendToFile(x, file);
            return x;
        }

        public LonLat GetCoordinatesFromFile(string file, int index)
        {
            return fileManager.GetFromFile(index, file);
        }

    }
}

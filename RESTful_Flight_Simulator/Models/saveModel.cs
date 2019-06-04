using System;
using System.Diagnostics;
using System.IO;
using System.Web.UI;

namespace RESTful_Flight_Simulator.Models
{
    public class saveModel 
    {


        public string ip { get; set; }
        public string port { get; set; }
        public string fileName { get; set; }
        public string interval { get; set; }
        public string duration { get; set; }

        public saveModel(string _ip, string _port, string _fileName, string _interval, string _duration)
        {
            ip = _ip;
            fileName = _fileName;
            port = _port;
            interval = _interval;
            duration = _duration;
        }


        /*
        private DataMiner miner;
        public string dataMined;
        public bool dataReady;
        public Action notify;
        public string fileName;


        public saveModel(string ip, int port, float interval, int duration, string file)
        {
            // Initialize miner
            Debug.WriteLine(ip + ',' + port.ToString() + ',' + interval.ToString() + ',' + duration.ToString() + ',' + file);
            miner = new DataMiner(ip, port, (interval * 1000), duration, true);
            dataReady = false;
            fileName = file;
        }


        public void SaveRoute()
        {
            Debug.WriteLine("Saving route...");
            // Mine and retrieve data
            miner.Mine();
            double[][] data = miner.GetData();
            int lines = data.GetLength(0);

            string[] str_data = new string[lines];
            for (int i = 0; i < lines; ++i)
            {
                // Turn double data into strings to write
                str_data[i] = String.Join(",", data[i]);
            }

            // Write values to member
            string values = String.Join(" ", str_data);
            dataReady = true;

            // TODO: REMOVE
            // DEBUG (Only use when running from Visual Studio)
            changeDir(@"D:\Projects\RESTful-Flight-Simulator\RESTful_Flight_Simulator\Views\Save\Routes");
            // Write to file
            System.IO.File.WriteAllText(@fileName, values);
        }

        private void changeDir(string dir)
        {
            try
            {
                //Set the current directory.
                Directory.SetCurrentDirectory(dir);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("The specified directory does not exist. {0}", e);
            }
        }
        */
    }
}
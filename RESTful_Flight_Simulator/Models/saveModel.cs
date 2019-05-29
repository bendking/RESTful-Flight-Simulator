using System;
using System.Diagnostics;
using System.Web.UI;

namespace RESTful_Flight_Simulator.Models
{
    public class saveModel
    {
        private DataMiner miner;
        public string dataMined;
        public bool dataReady;
        public Action notify;

        public saveModel(string ip = "127.0.0.1", int port = 5400, int duration = 10, float interval = 1)
        {
            // Initialize miner
            miner = new DataMiner(ip, port, duration, (int) (interval * 1000), true);
            dataReady = false;
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
            dataMined = values;
            dataReady = true;

            // Write to file
            System.IO.File.WriteAllLines(@"file1.txt", str_data);
        }
    }
}
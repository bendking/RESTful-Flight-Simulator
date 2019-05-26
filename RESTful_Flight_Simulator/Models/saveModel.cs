using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTful_Flight_Simulator.Models
{
    public class saveModel
    {
        private DataMiner miner;
        public saveModel(string ip = "127.0.0.1", int port = 5400, int duration = 10, int interval = 4)
        {
            // Initialize miner
            miner = new DataMiner(ip, port, duration, interval);
        }

        public void SaveRoute()
        {
            // Mine and retrieve data
            miner.Mine();
            double[][] data = miner.GetData();
            int lines = data.GetLength(0);
            int cols = data.GetLength(1);

            string[] str_data = new string[lines * cols];
            for (int i = 0; i < lines; ++i)
            {
                // Turn double data into strings to write
                str_data[i] = data[i].ToString();
            }

            // Write to file
            System.IO.File.WriteAllLines(@"file1.txt", str_data);
        }
    }
}
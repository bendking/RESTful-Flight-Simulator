using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTful_Flight_Simulator.Models
{
    public class DataMiner
    {
        private DataRequester requester;
        private static Timer timer;
        private int timerCounter;
        private int counterLimit;
        private double[][] data;

        public DataMiner(string ip = "127.0.0.1", int port = 5400, int duration = 10, int interval = 4)
        {
            // Initialize client & data
            requester = new DataRequester(ip, port);
            data = new double[duration * interval][];

            // Initialize timer
            timer = new Timer(1000 / interval);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;

            // Reset counters 
            timerCounter = 0;
            counterLimit = duration * interval;
        }

        public void Mine()
        {
            requester.Connect();
            timer.Start();
        }

        public double[][] GetData()
        {
            // Get data mined
            return data;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            // Get location date and insert into data
            double[] locationData = requester.RequestData();
            data[timerCounter] = locationData;

            // Update timer counter
            if (timerCounter < counterLimit)
            {
                ++timerCounter;
            }
            else
            {
                timer.Stop();
                timerCounter = 0;
            }
        }
    }
}
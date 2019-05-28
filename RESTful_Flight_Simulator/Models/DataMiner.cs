using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTful_Flight_Simulator.Models
{
    public class DataMiner
    {
        // Requester
        private DataRequester requester;
        // Timer
        private static Timer timer;
        private int timerCounter;
        private int counterLimit;
        // Data
        private bool save;
        private double[][] data;
        // Delegates used to notify when a tick triggered
        private List<Action<double, double>> listeners;
        private bool notify;

        // Interval - in milliseconds, duration - in seconds, save - decide whether to save data mined or not
        public DataMiner(string ip = "127.0.0.1", int port = 5400, int duration = 10, int interval = 1000, bool save = false)
        {
            // Initialize client & data
            requester = new DataRequester(ip, port);
            int logs = duration * (interval / 1000);

            // Data
            this.save = save;
            if (save) { data = new double[logs][]; }

            // Initialize timer
            timer = new Timer(interval);
            timer.Elapsed += Tick;
            timer.AutoReset = true;

            // Reset timer counters 
            timerCounter = 0;
            counterLimit = logs;

            // Initialize listeners
            listeners = new List<Action<double, double>>();
            notify = false;
        }

        public void AddListener(Action<double, double> action)
        {
            listeners.Add(action);
            notify = true;
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

        private void Tick(Object source, ElapsedEventArgs e)
        {
            // Get location date and insert into data
            double[] locationData = requester.RequestData();
            // Save data if neccessary
            if (save) { data[timerCounter] = locationData; }
            // Notify listeners if neccessary
            if (notify) {
                foreach (Action<double, double> action in listeners) {
                    action(locationData[0], locationData[1]);
                }
            }

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
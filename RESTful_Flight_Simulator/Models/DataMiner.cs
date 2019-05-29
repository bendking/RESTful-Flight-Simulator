using System;
using System.Timers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace RESTful_Flight_Simulator.Models
{
    public class DataMiner
    {
        // Requester
        private DataRequester requester;
        // Timer
        private static System.Timers.Timer timer;
        private int timerCounter;
        private int counterLimit;
        // Data
        private bool save;
        private double[][] data;
        private bool dataReady;
        private System.Threading.ManualResetEvent dataReadyTrigger;
        // Delegates used to notify when a tick triggered
        private List<Action<double[]>> tickListeners;
        private List<Action> doneListeners;
        private bool notifyTick;
        private bool notifyDone;

        // Interval - in milliseconds, duration - in seconds, save - decide whether to save data mined or not
        public DataMiner(string ip = "127.0.0.1", int port = 5400, int duration = 10, int interval = 1000, bool save = false)
        {
            // Initialize client & data
            requester = new DataRequester(ip, port);
            int logs_per_second = (int) (1 / ((float) interval / 1000));
            Debug.WriteLine(logs_per_second);
            int logs = duration * logs_per_second;
            Debug.WriteLine(logs);

            // Data
            this.save = save;
            if (save)
            {
                data = new double[logs][];
                dataReadyTrigger = new System.Threading.ManualResetEvent(false);
            }

            // Initialize timer
            timer = new Timer(interval);
            timer.Elapsed += Tick;
            timer.AutoReset = true;

            // Reset timer counters 
            timerCounter = 0;
            counterLimit = logs;

            // Initialize listeners
            tickListeners = new List<Action<double[]>>();
            notifyTick = false;
            notifyDone = false;
        }

        public void AddTickListener(Action<double[]> action)
        {
            tickListeners.Add(action);
            notifyTick = true;
        }

        public void AddDoneListener(Action action)
        {
            doneListeners.Add(action);
            notifyDone = true;
        }

        public double[][] GetData()
        {
            // Get data mined
            if (dataReady) { return data; }
            // If not ready, wait for trigger
            dataReadyTrigger.WaitOne();
            return data;
        }

        public void Mine()
        {
            requester.Connect();
            timer.Start();
            Debug.WriteLine("Mining commenced.");
        }

        private void Tick(Object source, ElapsedEventArgs e)
        {
            // Update timer counter
            if (timerCounter < counterLimit)
            {
                // Get location date and insert into data
                double[] locationData = requester.RequestData();
                // DEBUG
                Debug.Write(locationData[0]);
                Debug.Write(',');
                Debug.WriteLine(locationData[1]);
                // Save data if neccessary
                if (save) { data[timerCounter] = locationData; }
                // Notify tick listeners
                if (notifyTick) {
                    foreach (Action<double[]> action in tickListeners) {
                        action(locationData);
                    }
                }

                ++timerCounter;
            }
            else
            {
                // Reset timer
                timer.Stop();
                timerCounter = 0;
                // Announce data is ready to take
                if (save)
                {
                    dataReady = true;
                    dataReadyTrigger.Set();
                }
                // Notify done listeners
                if (notifyDone) {
                    foreach (Action action in doneListeners) {
                        action();
                    }
                }
            }
        }
    }
}
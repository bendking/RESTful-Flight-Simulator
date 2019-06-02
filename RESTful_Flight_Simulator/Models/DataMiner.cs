using System;
using System.Timers;
using System.Collections.Generic;
using System.Diagnostics;

//
// Deprecated
//

namespace RESTful_Flight_Simulator.Models
{
    public class DataMiner
    {
        // Requester
        private DataRequester requester;
        // Timer
        private static System.Timers.Timer timer;
        private int timerCounter;
        private int timerLimit;
        private float interval;
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
        public DataMiner(string ip, int port, float interval, float duration, bool save = false)
        {
            Debug.WriteLine(ip + ',' + port.ToString() + ',' + interval.ToString() + ',' + duration.ToString());
            // Initialize client & data
            requester = new DataRequester(ip, port);
            float logs_per_second = (1 / (interval / 1000));
            Debug.WriteLine("Logs per second: " + logs_per_second.ToString());
            int logs = (int) (duration * logs_per_second);
            Debug.WriteLine("Logs to be written: " + logs.ToString());

            // Data
            this.save = save;
            if (save)
            {
                data = new double[logs][];
                dataReadyTrigger = new System.Threading.ManualResetEvent(false);
            }

            // Reset timer counters 
            timerCounter = 0;
            timerLimit = logs;
            this.interval = interval;

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
            // Initialize timer
            timer = new Timer(interval);
            timer.Elapsed += Tick;
            timer.AutoReset = true;
            // Connect and start timer
            requester.Connect();
            timer.Start();
            Debug.WriteLine("Mining commenced.");
        }

        private void Tick(Object source, ElapsedEventArgs e)
        {
            // Update timer counter
            if (timerCounter < timerLimit)
            {
                // Get location date and insert into data
                double[] locationData = requester.RequestData();
                // DEBUG
                Debug.WriteLine(locationData[0].ToString() + "," + locationData[1].ToString() + ',' + timerCounter.ToString() + ',' + timerLimit.ToString());
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
                Debug.Write("Stopping timer.");
                // Reset timer / KILL TIMER
                timer = null;
                //timer.Stop();
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
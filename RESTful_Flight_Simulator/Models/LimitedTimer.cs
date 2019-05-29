using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;

namespace RESTful_Flight_Simulator.Models
{
    public class LimitedTimer
    {
        // Timer
        private static Timer timer;
        private int timerCounter;
        private int counterLimit;

        public LimitedTimer()
        {
            // Initialize timer
            timer = new Timer(1000);
            timer.Elapsed += Tick;
            timer.AutoReset = true;

            // Reset timer counters 
            timerCounter = 0;
            counterLimit = 10;
        }

        public void Start() {
            System.Diagnostics.Debug.WriteLine("Starting");
            timer.Start();
        }

        private void Tick(Object source, ElapsedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("TEST TICK");
            
            // Update timer counter
            if (timerCounter < counterLimit)
            {
                // Increment counter
                ++timerCounter;
            }
            else
            {
                // Stop timer and reset counter
                timer.Stop();
                timerCounter = 0;
            }
        }
    }
}
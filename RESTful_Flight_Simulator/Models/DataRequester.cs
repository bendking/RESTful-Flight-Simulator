using System;
using System.Text.RegularExpressions;

namespace RESTful_Flight_Simulator.Models
{
    public class DataRequester
    {
        private CommandClient client;
        private string[] requests;
        private Regex rx;
        private int cols;


        public DataRequester(string ip = "127.0.0.1", int port = 5400, int cols = 2)
        {
            // Initialize client & data
            client = new CommandClient(ip, port);
            this.cols = cols;

            // Hardcoded
            requests = new string[2];
            requests[0] = "get /position/longitude-deg";
            requests[1] = "get /position/latitude-deg";

            // Double Regex
            rx = new Regex(@"-?\d+(?:\.\d+)?", RegexOptions.Compiled);
        }

        public void Connect() {
            client.Connect();
        }

        // Must connect first
        public double[] RequestData()
        {
            double[] lineData = new double[cols];

            lineData[0] = ParseNumber(client.SendAndGet(requests[0]));
            lineData[1] = ParseNumber(client.SendAndGet(requests[1]));

            return lineData;
        }

        public double ParseNumber(string line, bool regexSearch = true)
        {
            string strNum;
            // Regex search
            if (regexSearch)
            {
                // Search for match and save result
                Match match = rx.Match(line);
                // Parse and return double
                return Double.Parse(match.ToString());
            }
            // Loop search
            else
            {
                strNum = "";
                bool flag = false;
                foreach (char c in line)
                {
                    // If between ' '
                    if (flag)
                    {
                        strNum += c;
                    }
                    else
                    {
                        // Flip flag if reached '
                        if (c == '\'')
                        {
                            flag = !flag;
                        }
                    }
                }
                // Parse and return double
                return Double.Parse(strNum);
            }
        }
    }
}
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace RESTful_Flight_Simulator.Models
{
    class CommandClient
    {
        private IPEndPoint ep;
        private TcpClient client;
        private StreamWriter writer = null;
        private StreamReader reader = null;
        private static CommandClient commandClientSingleton = null;
        private string ip;
        private int port;
        private bool connected = false;

        public string FlightServerIP
        {
            get { return ip; }
            set { ip = value; }
        }

        public int FlightCommandPort
        {
            get { return port; }
            set { port = value; }
        }
        public bool IsConnected
        {
            get { return connected; }
            set { connected = value; }
        }

        public CommandClient()
        {
            // Default constructor
        }

        public static CommandClient GetInstance()
        {
            if (commandClientSingleton == null) {
                commandClientSingleton = new CommandClient();
            }
            // Finally
            return commandClientSingleton;
        }
        public bool ParametersChanged(string _ip, int _port)
        {
            if (!ip.Equals( _ip) || port != _port)
            {
                Close();
                FlightServerIP = _ip;
                FlightCommandPort = _port;
                Connect();
                return true;
            }
            // Else
            return false;
        }
        public CommandClient(string _ip, int _port)
        {
            FlightServerIP = _ip;
            FlightCommandPort = _port;
        }



        public void Connect()
        {
            if (connected) return;
            connected = true;

            // Create endpoint & client, then connect
            ep = new IPEndPoint(IPAddress.Parse(FlightServerIP), FlightCommandPort);
            client = new TcpClient();
            // DEBUG
            System.Diagnostics.Debug.WriteLine("Connecting...");
            client.Connect(ep);
            // DEBUG
            System.Diagnostics.Debug.WriteLine("Client connected.");
            // Assign writer and reader
            writer = new StreamWriter(client.GetStream());
            reader = new StreamReader(client.GetStream());
        }
      
        public void Send(string msg)
        {
            if (!connected) return;
            writer.WriteLine(msg + "\n\r");
            writer.Flush();
        }

        public string Get() {
            return reader.ReadLine();
        }

        public string SendAndGet(string msg)
        {
            Send(msg);
            return Get();
        }

        public void Close()
        {
            if (!connected) return;
            connected = false;
            // Close connection
            client.Close();
        }
    }  
}
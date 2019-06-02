using System;
using System.IO;
using RESTful_Flight_Simulator.Models;

namespace RESTful_Flight_Simulator.Models
{
    public class FileManager
    {
        private StreamReader sr;
        private int lineNumber;
        private string currentFile=null;

        public FileManager()
        {
            // Default constructor
        }

        private string getPath(string file) {
            return "../RESTful_Flight_Simulator/Views/Save/Routes/" + file + ".txt";
        }

        public void AppendToFile(LonLat lonLat, string file)
        {
            string path = getPath(file);
            if (!File.Exists(path)) {
                // Create file and dispose of stream opened for it
                File.CreateText(path).Dispose();
            }
            // Append LonLoat to file
            File.AppendAllText(path, lonLat.toString() + Environment.NewLine);
        }

        public LonLat GetFromFile(int index, string file)
        {
            string path = getPath(file);

            if (currentFile == null || !file.Equals(currentFile) || index < lineNumber)
            {
                currentFile = file;
                sr = new StreamReader(path);
                lineNumber = 0;
            }

            string line = null;
            while (index != lineNumber)
            {
                line = sr.ReadLine();
                if (line == null) break; // there are less than 15 lines in the file
                lineNumber++;
            }

            if (line == null || line.Equals(Environment.NewLine) )
            {
                // End of file
                return null;
            }

            LonLat lonLat = LonLat.FromString(line);
            return lonLat;
        }
    }
}

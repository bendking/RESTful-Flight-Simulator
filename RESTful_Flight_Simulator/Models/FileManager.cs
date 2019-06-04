using System;
using System.Diagnostics;
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

        public static string getMappedPath(string file) {
            // Return mapped path
            return System.Web.Hosting.HostingEnvironment.MapPath("~/Views/Save/Routes/" + file + ".txt");
        }

        public void AppendToFile(LonLat lonLat, string file)
        {
            string path = getMappedPath(file);
            if (!File.Exists(path)) {
                // Create file and dispose of stream opened for it
                File.CreateText(path).Dispose();
            }
            // Append LonLoat to file
            File.AppendAllText(path, lonLat.toString() + Environment.NewLine);
        }

        public LonLat GetFromFile(int index, string file)
        {
            string path = getMappedPath(file);

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

        // Debug method
        private void changeDir(string dir)
        {
            try
            {
                //Set the current directory.
                Directory.SetCurrentDirectory(dir);
            }
            catch (DirectoryNotFoundException e)
            {
                Debug.WriteLine("The specified directory does not exist. {0}", e);
            }
        }
    }
}

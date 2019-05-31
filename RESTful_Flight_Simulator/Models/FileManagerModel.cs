using System;
using System.IO;
using RESTful_Flight_Simulator.Models;

namespace RESTful_Flight_Simulator.Models
{
    public class FileManagerModel
    {
        private StreamReader sr;
        private int lineNumber;
        private string currentFile=null;

        public FileManagerModel()
        {
         
        }

        private string getPath(string file)
        {
            return "../RESTful_Flight_Simulator/Views/Save/Routes/" + file + ".txt";
        }

        public void AppendFile(LonLat lonLat, string file)
        {
            string path = getPath(file);
            if(!File.Exists(path))
            {
                File.CreateText(path).Dispose();
            }

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
                //end of file
                return null;
            }

            LonLat lonLat = LonLat.FromString(line);
            return lonLat;
        }
    }
}

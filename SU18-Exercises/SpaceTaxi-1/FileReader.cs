using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Diagnostics;

namespace SpaceTaxi_1 {
    static class FileReader {
        private static string levelString;
        private static string name;
        private static string platforms;
        private static Dictionary<string, string> pictureDic = new Dictionary<string, string>();
        private static List<string> customerList = new List<string>();

        /// <summary>
        /// Reads the specified level creation file and extracts the relevant information
        /// </summary>
        /// <param name="path">The path to the .txt file for the level creation</param>
        public static void ReadFile(string path) {
            StreamReader sr = File.OpenText(path);

            //FileReader.levelString
            char[][] levelArray = new char[23][];
            char[] charArray; 
            int index = 0;
            string s = "";
            do {
                s = sr.ReadLine();
                charArray = s.ToCharArray();
                levelArray[index] = charArray;
                index++;
            } while (s != null && s != "");

            Debug.WriteLine(FileReader.levelString);

            //FileReader.name
            while (s == "") {
                s = sr.ReadLine();
            }

            int startIndex = s.IndexOf(" ");
            FileReader.name = s.Substring(startIndex + 1, (s.Length - 1) - startIndex);

            Debug.WriteLine(FileReader.name);
            List<char> platformsList = new List<char>();
            
            //FileReader.platforms
            s = sr.ReadLine();
            startIndex = s.IndexOf(" ");
            index = startIndex + 1;
            while (s[index] != ' ') {
                platformsList.Add(s[index]);
                index = index + 3;
            }
            // charListOfPlatforms.Add(Convert.ToChar(s.Substring(startIndex + 1, (s.Length - 1) - startIndex)));
            // FileReader.platforms = s.Substring(startIndex + 1, (s.Length - 1) - startIndex);

            Debug.WriteLine(FileReader.platforms);

            //FileReader.pictureDic
            while ((s = sr.ReadLine()) == "" || s.Contains(")")) {
                if (s == "") {
                    continue;
                } else {
                    startIndex = s.IndexOf(" ");
                    FileReader.pictureDic.Add(s[0].ToString(), s.Substring(startIndex + 1, (s.Length - 1) - startIndex));
                }
            }

            foreach (KeyValuePair<string, string> kvp in FileReader.pictureDic) {
                Debug.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }

            //FileReader.customerList
            while (s != null) {
                startIndex = s.IndexOf(" ");
                FileReader.customerList.Add(s.Substring(startIndex + 1, (s.Length - 1) - startIndex));
                s = sr.ReadLine();
            }

            foreach (string customer in FileReader.customerList) {
                Debug.WriteLine(customer);
            }
        }
    }
}

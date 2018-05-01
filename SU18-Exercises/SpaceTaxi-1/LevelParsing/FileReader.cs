using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Diagnostics;

namespace SpaceTaxi_1.LevelParsing {
    public static class FileReader {

        /// <summary>
        /// Reads a level file and creates a level object with the information
        /// </summary>
        /// <remarks>
        /// Assumes the file given consists of a level layout, name, a list of platform chars,
        /// a list of chars which correspond to an image, and one or more customers
        /// </remarks>
        /// <param name="path">The file path to the level file</param>
        /// <returns>A level object containing the extracted information</returns>
        public static Level ReadFile(string path) {
            StreamReader sr = File.OpenText(path);

            //levelArray
            char[][] levelArray = new char[24][];
            string s = "";
            for (int i = 0; i < levelArray.Length; i++) {
                s = sr.ReadLine();                
                levelArray[i] = s.ToCharArray();
            }

            for (int i = 0; i < levelArray.Length - 1; i++) {
                for (int j = 0; j < levelArray[0].Length; j++) {
                    Console.Write(levelArray[i][j]);
                }
                Console.WriteLine("");
            }

            //name
            while (s == "") {
                s = sr.ReadLine();
            }

            int startIndex = s.IndexOf(" ");
            string name = s.Substring(startIndex + 1, (s.Length - 1) - startIndex);

            Debug.WriteLine(name);
            
            //platformList
            s = sr.ReadLine();
            List<char> platformList = new List<char>();
            string[] splitArray = new string[1];
            splitArray = s.Split(new char[] { });

            for (int i = 1; i < splitArray.Length; i++) {
                platformList.Add(splitArray[i].ToCharArray()[0]);
            }

            foreach (char c in platformList) {
                Debug.Write(c + " ");
            }

            Debug.WriteLine("");

            //decoder
            Dictionary<char, string> decoder = new Dictionary<char, string>();

            while ((s = sr.ReadLine()) == "" || s.Contains(")")) {
                if (s == "") {
                    continue;
                } else {
                    startIndex = s.IndexOf(" ");
                    decoder.Add(s[0], s.Substring(startIndex + 1, (s.Length - 1) - startIndex));
                }
            }

            foreach (KeyValuePair<char, string> kvp in decoder) {
                Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }

            //customerList
            List<string> customerList = new List<string>();

            while (s != null) {
                startIndex = s.IndexOf(" ");
                customerList.Add(s.Substring(startIndex + 1, (s.Length - 1) - startIndex));
                s = sr.ReadLine();
            }

            foreach (string customer in customerList) {
                Debug.WriteLine(customer);
            }

            return new Level(levelArray, name, platformList, decoder, customerList);
        }
    }
}

using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using SpaceTaxi_1.Customers;

namespace SpaceTaxi_1.LevelParsing {
    public class FileReader {
        /// <summary>
        /// Reads a level file and creates a level object with the information
        /// </summary>
        /// <remarks>
        /// Assumes the file given consists of a level layout, name, a list of platform chars,
        /// a list of chars which correspond to an image, and one or more customers
        /// </remarks>
        /// <param name="stream">Stream to read level from</param>
        /// <returns>A level object containing the extracted information</returns>
        public Level ReadFile(Stream stream) {
            StreamReader sr = new StreamReader(stream);

            //levelArray
            char[][] levelArray = new char[24][];
            string s = "";
            for (int i = 0; i < levelArray.Length; i++) {
                s = sr.ReadLine();                
                levelArray[i] = s.ToCharArray();
            }

            //name
            while (s == "") {
                s = sr.ReadLine();
            }

            int startIndex = s.IndexOf(" ");
            string name = s.Substring(startIndex + 1, (s.Length - 1) - startIndex);
            
            //platformList
            s = sr.ReadLine();
            List<char> platformList = new List<char>();
            string[] splitArray = new string[1];
            splitArray = s.Split(new char[] { });

            for (int i = 1; i < splitArray.Length; i++) {
                platformList.Add(splitArray[i].ToCharArray()[0]);
            }

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

            //customerList
            List<string> customerList = new List<string>();

            while (s != null) {
                startIndex = s.IndexOf(" ");
                customerList.Add(s.Substring(startIndex + 1, (s.Length - 1) - startIndex));
                s = sr.ReadLine();
            }
            stream.Close();
            return new Level(levelArray, name, platformList, decoder, customerList);
        }
    }
}

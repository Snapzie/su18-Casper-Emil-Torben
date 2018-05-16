using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceTaxi_1.Customers {
    public class CustomerTranslator {
        public Customer MakeCustomer(List<string> customer, char[][] level) {
            int endIndex = customer[0].IndexOf(" ");
            string name = customer[0].Substring(0, endIndex);
            string substring = customer[0].Substring(endIndex + 1, (customer[0].Length - 1) - endIndex);
            endIndex = substring.IndexOf(" ");
            int spawnTime = int.Parse(substring.Substring(0, endIndex));
            substring = substring.Substring(endIndex + 1, (substring.Length - 1) - endIndex);
            endIndex = substring.IndexOf(" ");
            //Make string?
            char spawnPlatform = char.Parse(substring.Substring(0, endIndex));
            substring = substring.Substring(endIndex + 1, (substring.Length - 1) - endIndex);
            endIndex = substring.IndexOf(" ");
            string destination = substring.Substring(0, endIndex);
            substring = substring.Substring(endIndex + 1, (substring.Length - 1) - endIndex);
            endIndex = substring.IndexOf(" ");
            int time = int.Parse(substring.Substring(0, endIndex));
            substring = substring.Substring(endIndex + 1, (substring.Length - 1) - endIndex);
            int points = int.Parse(substring.Substring(0, substring.Length));

            int platformX = 0;
            int platformY = 0;
            for (int i = 0; i < level.Length; i++) {
                for (int j = 0; j < level[i].Length; j++) {
                    if (level[i][j] == spawnPlatform) {
                        platformX = j;
                        platformY = i - 1;
                    }
                }
            }
            
            return new Customer(name, spawnTime, spawnPlatform, destination, time, points, platformX, platformY);
        }
    }
}
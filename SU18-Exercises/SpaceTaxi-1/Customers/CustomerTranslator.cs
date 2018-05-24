    using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using DIKUArcade.Timers;
using SpaceTaxi_1.LevelParsing;
using SpaceTaxi_1.SpaceTaxiStates;

namespace SpaceTaxi_1.Customers {
    public class CustomerTranslator {
        /// <summary>
        /// Parses the information from Levelskeeper to create customers
        /// </summary>
        /// <param name="customers">A list of customers to be created</param>
        /// <param name="level">The level layout of the level the customer will be spawned in</param>
        /// <param name="customerImage">The image to be used for the customer</param>
        /// <returns>An array of customer objects</returns>
        public Customer[] MakeCustomers(List<string> customers, char[][] level,
            IBaseImage customerImage) {
            Customer[] result = new Customer[customers.Count];
            for (int k = 0; k < result.Length; k++) { 
                int endIndex = customers[k].IndexOf(" ");
                string name = customers[k].Substring(0, endIndex);
                string substring = customers[k].Substring(endIndex + 1, (customers[k].Length - 1) - endIndex);
                endIndex = substring.IndexOf(" ");
                int spawnTime = int.Parse(substring.Substring(0, endIndex));
                substring = substring.Substring(endIndex + 1, (substring.Length - 1) - endIndex);
                endIndex = substring.IndexOf(" ");
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
                EntityCreator ec = new EntityCreator();
                Entity entity = ec.CreateEntity(platformY, platformX, customerImage);
                result[k] = new Customer(name, spawnTime, spawnPlatform, destination, time, points, entity);
                CreateEvent(result[k], k);
            }

            return result;
        }
        
        /// <summary>
        /// Creates the timed event needed to spawn the customer
        /// </summary>
        /// <param name="customer">The customer for which the timed event should be made</param>
        /// <param name="index">An identifier for the customer</param>
        private void CreateEvent(Customer customer, int index) {
            TimedEventContainer timedEventContainer = GameRunning.GetInstance().TimedEventContainer;
            timedEventContainer.AddTimedEvent(TimeSpanType.Seconds, customer.SpawnTime, "CUSTOMER",
                index.ToString(), "");
        }
    }
}
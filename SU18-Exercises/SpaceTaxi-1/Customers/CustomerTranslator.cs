﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using DIKUArcade.Graphics;
using DIKUArcade.Entities;
using SpaceTaxi_1.LevelParsing;

namespace SpaceTaxi_1.Customers {
    public class CustomerTranslator {
        public Customer[] MakeCustomer(List<string> customers, char[][] level,
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
                EntityCreator ec = new EntityCreator();
                Entity entity = ec.CreateEntity(platformY, platformX, customerImage);
                result[k] = new Customer(name, spawnTime, spawnPlatform, destination, time, points, entity);
            }

            return result;
        }
    }
}
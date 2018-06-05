using System;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.State;
using DIKUArcade.Timers;
using OpenTK.Graphics;
using SpaceTaxi_1.LevelParsing;
using SpaceTaxi_1.SpaceTaxiStates;

namespace SpaceTaxi_1.Customers {
    public class Customer : Entity {
        public string Name { get;}
        public int SpawnTime { get;}
        public char SpawnPlatform { get;}
        public string DestinationPlatform { get;}
        public int TimeToDropOff { get;}
        public int Points { get; private set; }
        public bool CrossedBorder;
        public double PickUpTime;
            
        public Customer(string name, int spawnTime, char spawnPlatform, string destinationPlatform, int timeToDropOff,
            int points, Entity entity) : base(entity.Shape, entity.Image) {
            this.Name = name;
            this.SpawnTime = spawnTime;
            this.SpawnPlatform = spawnPlatform;
            this.DestinationPlatform = destinationPlatform;
            this.TimeToDropOff = timeToDropOff;
            this.Points = points;
            CrossedBorder = false;
        }
       
        /// <summary>
        /// Calculates the points scored for delivering the customer
        /// </summary>
        /// <returns>The total amount of points earned</returns>
        public int CalculatePoints() {
            double dropOffDelta = StaticTimer.GetElapsedSeconds() - PickUpTime;
            if (dropOffDelta > TimeToDropOff) {
                double frac = (dropOffDelta - TimeToDropOff) / TimeToDropOff;
                double penaltyPoints = frac * Points;
                Points = Points - (int) penaltyPoints;
            }

            return Points;
        }
    }
}
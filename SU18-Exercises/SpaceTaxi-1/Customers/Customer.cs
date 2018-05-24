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
        public string Name { get; private set; }
        public int SpawnTime { get; private set; }
        public char SpawnPlatform { get; private set; }
        public string DestinationPlatform { get; private set; }
        public int TimeToDropOff { get; private set; }
        public int Points { get; private set; }
        public bool CrossedBorder;
        private int posX;
        private int posY;
        public Level level;
        public double pickUpTime;
        
        private static EntityCreator entityCreator = new EntityCreator();
            
        /// <summary>
        /// The constructor from base (Entity) is called to make Customer an entity
        /// </summary>
        /// <param name="name">The name of the customer</param>
        /// <param name="spawnTime">The time required from when game is started until the cusotmer shall spawn</param>
        /// <param name="spawnPlatform">The platform on which the customer shall spawn</param>
        /// <param name="destinationPlatform">The platform on which the customer shall be dropped off</param>
        /// <param name="timeToDropOff">The time-span in which the customer must be dropped off after having been picked up</param>
        /// <param name="points">The amount of points given by the customer upon sucessfull drop-off</param>
        /// <param name="posX">The x-coordinate of the customers spawn-position</param>
        /// <param name="posY">The y-coordinate of the customers spawn-position</param>
        
        
        
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
       

        public void CalculatePoints() {
            double dropOffDelta = StaticTimer.GetElapsedSeconds() - pickUpTime;
            if (dropOffDelta > TimeToDropOff) {
                double frac = (dropOffDelta - TimeToDropOff) / TimeToDropOff;
                double penaltyPoints = frac * Points;
                Points = Points - (int) penaltyPoints;
            }
            GameRunning.GetInstance().GivePoints(Points);
        }
    }
}
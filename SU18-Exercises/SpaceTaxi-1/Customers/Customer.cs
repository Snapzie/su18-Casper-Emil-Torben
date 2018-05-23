using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.State;
using OpenTK.Graphics;
using SpaceTaxi_1.LevelParsing;
using SpaceTaxi_1.SpaceTaxiStates;

namespace SpaceTaxi_1.Customers {
    public class Customer : Entity, ICustomer {
        public string Name { get; private set; }
        public int SpawnTime { get; private set; }
        public char SpawnPlatform { get; private set; }
        public string destinationPlatform { get; private set; }
        public int TimeToDropOff { get; private set; }
        public int Points { get; private set; }
        public bool CrossedBorder;
        private int posX;
        private int posY;
        public Level level;
        
        private static EntityCreator entityCreator = new EntityCreator();
            
    
        public Customer(string name, int spawnTime, char spawnPlatform, string destinationPlatform, int timeToDropOff,
            int points, Entity entity) : base(entity.Shape, entity.Image) {
            this.Name = name;
            this.SpawnTime = spawnTime;
            this.SpawnPlatform = spawnPlatform;
            this.destinationPlatform = destinationPlatform;
            this.TimeToDropOff = timeToDropOff;
            this.Points = points;
            CrossedBorder = false;
            //Spawn();

        }
        
        /// <summary>
        /// Spawns the customer in current level
        /// </summary>
        /// <remarks>
        /// Should only be called when game is running, else the cast will fail
        /// </remarks>
        public void Spawn() {
            IGameState game = new StateMachine().ActiveState;
            ((GameRunning) game).AddCustomer(this); 
        }

        
        /// <summary>
        /// Despawns the customer from current level
        /// </summary>
        /// <remarks>
        /// Should only be called when game is running, else the cast will fail
        /// </remarks>
        public void Despawn() {
            IGameState game = new StateMachine().ActiveState;
            ((GameRunning) game).RemoveCustomer(this);
        }

        public void CalculatePoints() {
            GameRunning.GetInstance().GivePoints(Points);
        }
    }
}
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
        public char SpawnPlatform { get; private set; } //Skal nok ændres til en platform class
        private string destinationPlatform; //Skal nok ændres til en platform class
        public int TimeToDropOff { get; private set; }
        public int Points { get; private set; }
        private int posX;
        private int posY;
        public Level level;
        
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
            this.destinationPlatform = destinationPlatform;
            this.TimeToDropOff = timeToDropOff;
            this.Points = points;
            //Spawn();

        }
       
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

        public void GivePoints() {
            
            throw new System.NotImplementedException();
        }
    }
}
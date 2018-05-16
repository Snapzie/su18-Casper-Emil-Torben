using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using SpaceTaxi_1.LevelParsing;

namespace SpaceTaxi_1.Customers {
    public class Customer : Entity, ICustomer {
        private string name;
        private int spawnTime;
        private char spawnPlatform;
        private string destinationPlatform;
        private int timeToDropOff;
        private int points;
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
            int points, int posX, int posY) : base( 
            entityCreator.CreateEntity(posY, posX, new Image(Path.Combine("Assets", "Images", "CustomerStandLeft.png"))).Shape,  
            entityCreator.CreateEntity(posY, posX, new Image(Path.Combine("Assets", "Images", "CustomerStandLeft.png"))).Image) {
            this.name = name;
            this.spawnTime = spawnTime;
            this.spawnPlatform = spawnPlatform;
            this.destinationPlatform = destinationPlatform;
            this.timeToDropOff = timeToDropOff;
            this.points = points;
            this.posX = posX;
            this.posY = posY;
            Spawn();    

        }
        
        /// <summary>
        ///  The instance of Customer is added to an EntityContainer
        /// </summary>
        public void Spawn() {

            level.AddCustomer(this);
        }
        /// <summary>
        /// The instance of customer is removed from the EntityContainer
        /// </summary>
        public void Despawn() {
            
            level.RemoveCustomer(this);
        }

        public void GivePoints() {
            
            throw new System.NotImplementedException();
        }
    }
}
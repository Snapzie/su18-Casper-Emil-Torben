using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using SpaceTaxi_1.LevelParsing;

namespace SpaceTaxi_1.Customers {
    public class Customer : ICustomer {
        private string name;
        private int spawnTime;
        private char spawnPlatform;
        private string destinationPlatform;
        private int timeToDropOff;
        private int points;
        private string imageString;
        private int posX;
        private int posY;
        public Entity Entity;
        public Level level;
        
        
    
        public Customer(string name, int spawnTime, char spawnPlatform, string destinationPlatform, int timeToDropOff,
            int points, int posX, int posY) {
            this.name = name;
            this.spawnTime = spawnTime;
            this.spawnPlatform = spawnPlatform;
            this.destinationPlatform = destinationPlatform;
            this.timeToDropOff = timeToDropOff;
            this.points = points;
            this.posX = posX;
            this.posY = posY;

        }

        public void Spawn(Customer customer) {

            EntityCreator entityCreator = new EntityCreator();
            /// One is subtracted to ensure the customer lands upon the platform,
            /// which is equal to subtracting 1 from it's y-coordinate, as the y-coordinate
            /// is given at the base of the object. 
            entityCreator.CreateEntity(posY - 1, posX, new Image(Path.Combine("Assets", "Images", imageString)));
            level.AddCustomer(customer);
        }

        public void Despawn(Customer customer) {
            
            level.RemoveCustomer(customer);
        }

        public void GivePoints() {
            throw new System.NotImplementedException();
        }
    }
}
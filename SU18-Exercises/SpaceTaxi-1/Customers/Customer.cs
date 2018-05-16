using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.State;
using SpaceTaxi_1.LevelParsing;
using SpaceTaxi_1.SpaceTaxiStates;

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

        public void Spawn() {
            IGameState game = new StateMachine().ActiveState;
            ((GameRunning) game).AddCustomer(this);
        }

        public void Despawn() {
            IGameState game = new StateMachine().ActiveState;
            ((GameRunning) game).RemoveCustomer(this);
        }

        public void GivePoints() {
            
            throw new System.NotImplementedException();
        }
    }
}
namespace SpaceTaxi_1.Customers {
    public class Customer {
        private string Name;
        private int SpawnTime;
        private char SpawnPlatform;
        private string DestinationPlatform;
        private int TimeToDropOff;
        private int Points;

        public Customer(string name, int spawnTime, char spawnPlatform, string destinationPlatform, int timeToDropOff,
            int points) {
            Name = name;
            SpawnTime = spawnTime;
            SpawnPlatform = spawnPlatform;
            DestinationPlatform = destinationPlatform;
            TimeToDropOff = timeToDropOff;
            Points = points;

        }
        
    }
}
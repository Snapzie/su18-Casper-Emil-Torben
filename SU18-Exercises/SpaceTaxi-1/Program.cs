using SpaceTaxi_1.LevelParsing;

namespace SpaceTaxi_1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            LevelLoader.LoadLevels();
            var game = new Game();
            game.GameLoop();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceTaxi_1.LevelParser;

using System.IO;

namespace SpaceTaxi_1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var game = new Game();
            FileReader.ReadFile(Path.Combine("..", "..", "Levels", "the-beach.txt"));
            game.GameLoop();
        }
    }
}

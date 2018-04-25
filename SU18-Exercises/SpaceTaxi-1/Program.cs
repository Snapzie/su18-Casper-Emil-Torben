using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceTaxi_1.LevelParsing;

using System.IO;

namespace SpaceTaxi_1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var game = new Game();
            LevelLoader.LoadLevels();
            game.GameLoop();
        }
    }
}

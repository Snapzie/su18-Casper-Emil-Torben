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
            LevelLoader.LoadLevels();
            var game = new Game();
            game.GameLoop();
        }
    }
}

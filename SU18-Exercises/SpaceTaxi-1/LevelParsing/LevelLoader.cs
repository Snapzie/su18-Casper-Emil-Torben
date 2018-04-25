using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Diagnostics;

namespace SpaceTaxi_1.LevelParsing {
    static class LevelLoader {
        private static List<string> levelList = new List<string> { Path.Combine("..", "..", "Levels", "short-n-sweet.txt"),
                                                                   Path.Combine("..", "..", "Levels", "the-beach.txt")};

        /// <summary>
        /// Loads the levels specified in levelList
        /// </summary>
        public static void LoadLevels() {
            Level level;
            LevelsKeeper keeper = LevelsKeeper.Instance;

            foreach (String levelPath in levelList) {
                level = FileReader.ReadFile(levelPath);
                keeper.SaveLevel(level);
            }

            for (int i = 1; i < 3; i++) {
                Debug.WriteLine("\n" + "Name of level " + i + ": " + keeper.GetLevel(i).Name);
            }
        }
    }
}

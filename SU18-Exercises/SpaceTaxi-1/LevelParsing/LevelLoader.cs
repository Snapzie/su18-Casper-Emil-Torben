using System;
using System.Collections.Generic;
using System.IO;

namespace SpaceTaxi_1.LevelParsing {
    public static class LevelLoader {
        private static List<string> levelList = new List<string> { Path.Combine("..", "..", "Levels", "short-n-sweet.txt"),
                                                                   Path.Combine("..", "..", "Levels", "the-beach.txt"),
                                                                   Path.Combine("..", "..", "Levels", "TestLevel.txt")};

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
        }
    }
}

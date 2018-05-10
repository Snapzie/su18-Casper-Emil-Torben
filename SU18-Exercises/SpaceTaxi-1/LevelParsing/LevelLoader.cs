using System;
using System.Collections.Generic;
using System.IO;

namespace SpaceTaxi_1.LevelParsing {
    public class LevelLoader {
        private static List<string> levelList = new List<string> { Path.Combine("..", "..", "Levels", "short-n-sweet.txt"),
                                                                   Path.Combine("..", "..", "Levels", "the-beach.txt"),
                                                                   Path.Combine("..", "..", "Levels", "TestLevel.txt")};

        /// <summary>
        /// Loads the levels specified in levelList
        /// </summary>
        public void LoadLevels() {
            Level level;
            LevelsKeeper keeper = LevelsKeeper.Instance;
            FileReader fr = new FileReader();

            foreach (String levelPath in levelList) {
                level = fr.ReadFile(levelPath);
                keeper.SaveLevel(level);
            }
        }
    }
}

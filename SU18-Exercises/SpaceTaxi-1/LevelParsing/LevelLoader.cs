using System;
using System.Collections.Generic;
using System.IO;

namespace SpaceTaxi_1.LevelParsing {
    public class LevelLoader {
        private static List<string> levelList = 
            new List<string> { Path.Combine("..", "..", "Levels", "short-n-sweet.txt"),
                               Path.Combine("..", "..", "Levels", "the-beach.txt")};

        /// <summary>
        /// Loads the levels specified in levelList
        /// </summary>
        public void LoadLevels() {
            LevelsKeeper keeper = LevelsKeeper.Instance;
            FileReader fr = new FileReader();

            foreach (string levelPath in LevelLoader.levelList) {
                Level level = fr.ReadFile(new FileStream(levelPath, FileMode.Open));
                keeper.SaveLevel(level);
            }
        }
    }
}

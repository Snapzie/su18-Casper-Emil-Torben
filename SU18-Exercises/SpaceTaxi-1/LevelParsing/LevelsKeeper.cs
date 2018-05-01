﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceTaxi_1.LevelParsing {
    public class LevelsKeeper {
        private static LevelsKeeper instance;
        private Dictionary<int, Level> levelDic;
        private int levelCounter;

        public Level this[int i] {
            get {
                return this.levelDic[i];
            }
        }

        /// <summary>
        /// Constructs the LevelsKeeper singleton and instatiates its fields
        /// </summary>
        private LevelsKeeper() {
            levelDic = new Dictionary<int, Level>();
            levelCounter = 1;
        }

        /// <summary>
        /// Instatiates a LevelsKeeper singleton and returns it
        /// </summary>
        /// <returns>Returns the LevelsKeeper singleton</returns>
        public static LevelsKeeper Instance {
            get {
                if (LevelsKeeper.instance == null) {
                    LevelsKeeper.instance = new LevelsKeeper();
                }

                return LevelsKeeper.instance;
            }
        }

        /// <summary>
        /// Saves the level from the parameter in levelDic
        /// </summary>
        /// <param name="level">A level object which will be saved</param>
        public void SaveLevel(Level level) {
            this.levelDic.Add(this.levelCounter, level);
            this.levelCounter++;
        }
    }
}

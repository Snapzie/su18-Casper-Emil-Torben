using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DIKUArcade.Entities;

namespace SpaceTaxi_1.LevelParsing {
    public class LevelCreator {
        private static int level;
        private static LevelsKeeper levelKeeper = LevelsKeeper.Instance;
        private Level entityContainer = levelKeeper.GetLevel(level);
    }
   
}
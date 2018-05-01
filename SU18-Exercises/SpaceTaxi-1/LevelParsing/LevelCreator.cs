using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DIKUArcade.Entities;

namespace SpaceTaxi_1.LevelParsing {
    public static class LevelCreator {
        private static LevelsKeeper levelKeeper = LevelsKeeper.Instance;
        
        
        public static EntityContainer CreateLevel(int levelNumber) {
            Level level = levelKeeper[levelNumber];
            EntityContainer objects = new EntityContainer();
            for (int i = 0; i < level.LevelLayout.Length; i++) {
                for (int j = 0; j < level.LevelLayout[i].Length; j++) {
                    if (level.Decoder.ContainsKey(level.LevelLayout[i][j])) {
                        Entity ent =
                            EntityCreator.CreateEntity(i, j,
                                level.Decoder[level.LevelLayout[i][j]]);
                        objects.AddStationaryEntity((StationaryShape) ent.Shape, ent.Image);
                    }
                }
            }

            return objects;
        }
    }
   
}
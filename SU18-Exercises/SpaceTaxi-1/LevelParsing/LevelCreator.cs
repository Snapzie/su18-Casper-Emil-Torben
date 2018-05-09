using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace SpaceTaxi_1.LevelParsing {
    public static class LevelCreator {
        /// <summary>
        /// Instantiates levelsKeeper object
        /// </summary>
        private static LevelsKeeper levelKeeper = LevelsKeeper.Instance;
        
        /// <summary>
        /// This method creates a new level
        /// </summary>
        /// <param name = "level"> A level object is instantiated through the LevelsKeeper object</param>
        /// <param name="object"> An entity container which holds all the entities the level has</param>
        /// <remarks>All symbols in the ASCII version of our level is found a correspondent image
        /// through the Decoder dictionary, which is made as an entity to be added to object
        /// </remarks>
        /// <returns> This method returns the EntityContainer named object, which has all 
        /// the different entities needed to create the given levell
        ///</returns>
        public static EntityContainer[] CreateLevel(int levelNumber) {
            Level level = levelKeeper[levelNumber];
            EntityContainer[] objects = new EntityContainer[2];
            objects[0] = new EntityContainer();
            objects[1] = new EntityContainer();
            
            for (int i = 0; i < level.LevelLayout.Length; i++) {
                for (int j = 0; j < level.LevelLayout[i].Length; j++) {
                    if (level.Decoder.ContainsKey(level.LevelLayout[i][j])) {
                        Image img = new Image(Path.Combine(Path.Combine("Assets", "Images",
                            level.Decoder[level.LevelLayout[i][j]])));
                        Entity ent =
                            EntityCreator.CreateEntity(i, j, img);
                        if (level.Platforms.Contains(level.LevelLayout[i][j])) {                            
                            objects[0].AddStationaryEntity((StationaryShape) ent.Shape, ent.Image);
                        } else {
                            objects[1].AddStationaryEntity((StationaryShape) ent.Shape, ent.Image);
                        }
                        
                    }
                }
            }

            return objects;
        }
    }
   
}
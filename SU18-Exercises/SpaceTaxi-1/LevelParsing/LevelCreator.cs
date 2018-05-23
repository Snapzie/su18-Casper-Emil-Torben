
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using SpaceTaxi_1.Customers;

namespace SpaceTaxi_1.LevelParsing {
    public class LevelCreator {
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
        public EntityContainer[] CreateLevel(int levelNumber) {
            EntityCreator ec = new EntityCreator();
            Level level = levelKeeper[levelNumber];
            EntityContainer[] renderItems = new EntityContainer[3];
            renderItems[0] = new EntityContainer();
            renderItems[1] = new EntityContainer();
            renderItems[2] = new EntityContainer(); //empty container to add customers later
            
            for (int i = 0; i < level.LevelLayout.Length; i++) {
                for (int j = 0; j < level.LevelLayout[i].Length; j++) {
                    if (level.Decoder.ContainsKey(level.LevelLayout[i][j])) {
                        Image img = new Image(Path.Combine(Path.Combine("Assets", "Images",
                            level.Decoder[level.LevelLayout[i][j]])));
                        Entity ent =
                            ec.CreateEntity(i, j, img);
                        if (level.Platforms.Contains(level.LevelLayout[i][j])) {                            
                            renderItems[0].AddStationaryEntity((StationaryShape) ent.Shape, ent.Image);
                        } else {
                            renderItems[1].AddStationaryEntity((StationaryShape) ent.Shape, ent.Image);
                        }
                        
                    }
                }
            }
            
            
            return renderItems;
        }
    }
   
}
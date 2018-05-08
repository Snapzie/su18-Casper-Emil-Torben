using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace SpaceTaxi_1.LevelParsing {
    public static class EntityCreator {
        
        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="line">The line from which the image-char comes from</param>
        /// <param name="col">The column from which the image-char comes from</param>
        /// <param name="img">The name of the image to attach to the entity</param>
        /// <returns>Returns new entity with given parameters</returns>
        public static Entity CreateEntity(int line, int col, IBaseImage img) {
            //All levels consist of 23 lines with 40 chars, so this converts coordinates to something
            //between 0 and 1
            float x = col / 40f;
            //We subtract 1/23 since coordinates is set to bottom left corner
            float y = (23 - line) / 23f - (1.0f / 23);
            Shape s = new StationaryShape(x, y, 1 / 40f, 1 / 23f);
            return new Entity(s, img);
        }
    }
}
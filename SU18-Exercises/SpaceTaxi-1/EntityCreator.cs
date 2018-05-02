using System.IO;
using System.Runtime.InteropServices;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace SpaceTaxi_1 {
    public static class EntityCreator {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="line">The line from which the image-char comes from</param>
        /// <param name="col">The column from which the image-char comes from</param>
        /// <param name="img">The name of the image to attach to the entity</param>
        /// <returns>Returns new entity with given parameters</returns>
        public static Entity CreateEntity(int line, int col, string img) {
            //All levels consist of 23 lines with 40 chars, so this converts coordinates to something
            //between 0 and 1
            float x = col / 40f;
            float y = line / 23f;
            Shape s = new StationaryShape(x, y, 1 / 40f, 1 / 23f);
            Image i = new Image(Path.Combine("Assets", "Images", img));
            return new Entity(s, i);
        }
    }
}
using System.Collections.Generic;
using DIKUArcade.Entities;

namespace SpaceTaxi_1.Entities {
    public class SmartEntityContainer  {
        private List<Entity> entities;
        public void AddEntity(Entity ent) {
            entities.Add(ent);
        }
    }
}
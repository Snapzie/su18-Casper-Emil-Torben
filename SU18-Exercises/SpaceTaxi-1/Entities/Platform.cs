using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace SpaceTaxi_1 {
    public class Platform : Entity {
        public char Identifier { get; private set; }
        public Platform(Entity entity, char identifier) : base(entity.Shape, entity.Image) {
            Identifier = identifier;
        }
    }
}
using DIKUArcade.Entities;

namespace SpaceTaxi_1.Entities {
    public class Platform : Entity {
        public char Identifier { get;}
        public Platform(Entity entity, char identifier) : base(entity.Shape, entity.Image) {
            Identifier = identifier;
        }
    }
}
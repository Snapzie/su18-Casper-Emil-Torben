using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_2.GalagaEntities {
    public class Enemy : DIKUArcade.Entities.Entity {
        public Vec2F Position {get; private set;}

        public Enemy(StationaryShape shape, IBaseImage image) : base(shape, image) {
            this.Position = Shape.Position; 
        }
    }
}
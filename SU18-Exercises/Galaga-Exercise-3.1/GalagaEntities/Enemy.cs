using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_3._1.GalagaEntities {
    public class Enemy : DIKUArcade.Entities.Entity {
        public Vec2F Position {get; private set;}
        public Vec2F InitPosition { get; private set; }

        public Enemy(DynamicShape shape, IBaseImage image) : base(shape, image) {
            this.Position = Shape.Position; 
            this.InitPosition = Shape.Position; 
        }
    }
}
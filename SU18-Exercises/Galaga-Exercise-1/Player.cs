using System.IO;
using DIKUArcade.EventBus;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_1 {
    public class Player : IGameEventProcessor<object> {
        private Entity self;
        private float movementSpeed = 0.01f;

        public Player() {
            self = new Entity(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)), 
                new Image(Path.Combine("Assets", "Images", "Player.png")));
        }

        public void Move() {
            self.Shape.Move();
            if (self.Shape.Position.X > 0.9) {
                self.Shape.Position.X = 0.9f;
            }else if (self.Shape.Position.X < 0.0) {
                self.Shape.Position.X = 0.0f;
            }
        }

        public void Render() {
            self.RenderEntity();
        }

        public void MoveLeft() {
            ((DynamicShape) (self.Shape)).Direction.X = -movementSpeed;
        }
        
        public void MoveRight() {
            ((DynamicShape) (self.Shape)).Direction.X = movementSpeed; 
        }

        public void KeyRelease() {
            ((DynamicShape) (self.Shape)).Direction.X = 0.0f;
        }
        
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            
        }
    }
}
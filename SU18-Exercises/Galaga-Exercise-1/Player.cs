using System.IO;
using DIKUArcade.EventBus;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_1 {
    public class Player : IGameEventProcessor<object> {
        public Entity Self { get; private set; }
        private float movementSpeed = 0.01f;

        public Player() {
            Self = new Entity(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)), 
                new Image(Path.Combine("Assets", "Images", "Player.png")));
        }

        public void Move() {
            Self.Shape.Move();
            if (Self.Shape.Position.X > 0.9) {
                Self.Shape.Position.X = 0.9f;
            }else if (Self.Shape.Position.X < 0.0) {
                Self.Shape.Position.X = 0.0f;
            }
        }

        public void Render() {
            Self.RenderEntity();
        }

        public void MoveLeft() {
            ((DynamicShape) (Self.Shape)).Direction.X = -movementSpeed;
        }
        
        public void MoveRight() {
            ((DynamicShape) (Self.Shape)).Direction.X = movementSpeed; 
        }

        public void KeyRelease() {
            ((DynamicShape) (Self.Shape)).Direction.X = 0.0f;
        }
        
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.PlayerEvent) {
                switch (gameEvent.Message) {
                    case "MOVE_LEFT":
                        MoveLeft();
                        break;
                    case "MOVE_RIGHT":
                        MoveRight();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
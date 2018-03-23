using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_3._1 {
    public class Player : IGameEventProcessor<object> {
        public Entity PlayerEntity { get; private set; }
        private float movementSpeed = 0.01f;

        public Player() {
            PlayerEntity = new Entity(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)), 
                new Image(Path.Combine("Assets", "Images", "Player.png")));
        }

        public void Move() {
            PlayerEntity.Shape.Move();
            if (PlayerEntity.Shape.Position.X > 0.9) {
                PlayerEntity.Shape.Position.X = 0.9f;
            }else if (PlayerEntity.Shape.Position.X < 0.0) {
                PlayerEntity.Shape.Position.X = 0.0f;
            }
        }

        public void Render() {
            PlayerEntity.RenderEntity();
        }

        public void MoveLeft() {
            ((DynamicShape) (PlayerEntity.Shape)).Direction.X = -movementSpeed;
        }
        
        public void MoveRight() {
            ((DynamicShape) (PlayerEntity.Shape)).Direction.X = movementSpeed; 
        }

        public void KeyRelease() {
            ((DynamicShape) (PlayerEntity.Shape)).Direction.X = 0.0f;
        }
        
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.PlayerEvent) {
                switch (gameEvent.Message) {
                    case "MOVE LEFT":
                        MoveLeft();
                        break;
                    case "MOVE RIGHT":
                        MoveRight();
                        break;
                    case "KEY RELEASE":
                        KeyRelease();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
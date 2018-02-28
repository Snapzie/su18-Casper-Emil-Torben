using System.IO;
using DIKUArcade.EventBus;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga_Exercise_1 {
    public class Player : IGameEventProcessor<object> {
        private GameEventBus<object> eventBus;
        private Entity player;
        private float movementSpeed = 0.01f;

        public Player() {
            eventBus = new GameEventBus<object>();
            eventBus.Subscribe(GameEventType.PlayerEvent, this);
            
            player = new Entity(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)), 
                new Image(Path.Combine("Assets", "Images", "Player.png")));
        }

        public void Move() {
            player.Shape.Move();
            if (((DynamicShape) (player.Shape)).Position.X > 0.9) {
                //Console.WriteLine((((DynamicShape) (player.Shape)).Position.X));
                ((DynamicShape) (player.Shape)).Position.X = 0.9f;
            }else if (((DynamicShape) (player.Shape)).Position.X < 0.0) {
                //Console.WriteLine((((DynamicShape) (player.Shape)).Position.X));
                ((DynamicShape) (player.Shape)).Position.X = 0.0f;
            }
        }

        public void Render() {
            player.RenderEntity();
        }

        public void MoveLeft() {
            ((DynamicShape) (player.Shape)).Direction.X = -movementSpeed;
        }
        
        public void MoveRight() {
            ((DynamicShape) (player.Shape)).Direction.X = movementSpeed; 
        }
        
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            
        }

        public void KeyRelease() {
            ((DynamicShape) (player.Shape)).Direction.X = 0.0f;
        }
    }
}
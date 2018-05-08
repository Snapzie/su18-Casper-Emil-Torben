using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using OpenTK.Graphics;
using OpenTK.Platform.Windows;

namespace SpaceTaxi_1
{
    public class Player : IGameEventProcessor<object> {
        public Vec2F Velocity;
        public Entity Entity { get; private set; }
        private readonly DynamicShape shape;
        private readonly Image taxiBoosterOffImageLeft;
        private readonly Image taxiBoosterOffImageRight;
        private readonly ImageStride taxiBoosterOnImageRight;
        private readonly ImageStride taxiBoosterOnImageLeft;
        private readonly ImageStride taxiBoosterOnImageUpLeft;
        private readonly ImageStride taxiBoosterOnImageUpRight;
        private readonly ImageStride taxiBoosterOnImageRightUp;
        private readonly ImageStride taxiBoosterOnImageLeftUp;
        private Orientation _taxiOrientation;
        private Vec2F force;
        private float gravity = -0.01f;
        private float boosterForce = 0.01f;
        private bool backBoosterOn = false;
        private bool bottomBosterOn = false;

        public Player()
        {
            shape = new DynamicShape(new Vec2F(), new Vec2F());
            taxiBoosterOffImageLeft = new Image(Path.Combine("Assets", "Images", "Taxi_Thrust_None.png"));
            taxiBoosterOffImageRight = new Image(Path.Combine("Assets", "Images", "Taxi_Thrust_None_Right.png"));
            taxiBoosterOnImageLeft = new ImageStride(80,
                ImageStride.CreateStrides(2, Path.Combine("Assets", "Images", "Taxi_Thrust_Back.png")));
            
            taxiBoosterOnImageRight = new ImageStride(80,
                ImageStride.CreateStrides(2, Path.Combine("Assets", "Images", "Taxi_Thrust_Back_Right.png")));
            
            taxiBoosterOnImageUpLeft = new ImageStride(80,
                ImageStride.CreateStrides(2, Path.Combine("Assets", "Images", "Taxi_Thrust_Bottom.png")));
            
            taxiBoosterOnImageUpRight = new ImageStride(80,
                ImageStride.CreateStrides(2, Path.Combine("Assets", "Images", "Taxi_Thrust_Bottom_Right.png")));
            
            taxiBoosterOnImageRightUp = new ImageStride(80,
                ImageStride.CreateStrides(2, Path.Combine("Assets", "Images", "Taxi_Thrust_Bottom_Back_Right.png")));
            
            taxiBoosterOnImageLeftUp = new ImageStride(80,
                ImageStride.CreateStrides(2, Path.Combine("Assets", "Images", "Taxi_Thrust_Bottom_Back.png")));
                        

            Entity = new Entity(shape, taxiBoosterOffImageLeft);
            force = new Vec2F(0, gravity);
            shape.Direction = new Vec2F(0, 0);
            Velocity = new Vec2F(0, 0);
        }

        public void SetPosition(float x, float y)
        {
            shape.Position.X = x;
            shape.Position.Y = y;
        }

        public void SetExtent(float width, float height)
        {
            shape.Extent.X = width;
            shape.Extent.Y = height;
        }

        public void RenderPlayer()
        {
            //TODO: Next version needs animation. Skipped for clarity.
            Entity.Image = _taxiOrientation == Orientation.Left
                ? taxiBoosterOffImageLeft
                : taxiBoosterOffImageRight;
            if (_taxiOrientation == Orientation.Left) {
                if (backBoosterOn) {
                    if (bottomBosterOn) {
                        Entity.Image = taxiBoosterOnImageLeftUp;
                    } else {
                        Entity.Image = taxiBoosterOnImageLeft;
                    }
                } else {
                    if (bottomBosterOn) {
                        Entity.Image = taxiBoosterOnImageUpLeft;
                    } else {
                        Entity.Image = taxiBoosterOffImageLeft;
                    }
                }
            } else {
                if (backBoosterOn) {
                    if (bottomBosterOn) {
                        Entity.Image = taxiBoosterOnImageRightUp;
                    } else {
                        Entity.Image = taxiBoosterOnImageRight;
                    }
                } else {
                    if (bottomBosterOn) {
                        Entity.Image = taxiBoosterOnImageUpRight;
                    } else {
                        Entity.Image = taxiBoosterOffImageRight;
                    }
                }

                
            }
            shape.Direction.X += 1.0f / Game.GameTimer.CapturedFrames * force.X;
            shape.Direction.Y += 1.0f / Game.GameTimer.CapturedFrames * force.Y;
            shape.Move();
            Entity.RenderEntity();
            
//            shape.Direction = Velocity * (1.0f / Game.GameTimer.CapturedFrames);
//            shape.Position.X += 1.0f / Game.GameTimer.CapturedFrames * Velocity.X;
//            shape.Position.Y += 1.0f / Game.GameTimer.CapturedFrames * Velocity.Y;
        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent)
        {
            if (eventType == GameEventType.PlayerEvent)
            {
                switch (gameEvent.Message) {
                case "BOOSTER_UPWARDS":
                    force.Y = boosterForce * 1f;
                    bottomBosterOn = true;
                    break;
                case "STOP_BOOSTER_UPWARDS":
                    force.Y = gravity;
                    bottomBosterOn = false;
                    break;
                case "BOOSTER_LEFT":
                    force.X = -boosterForce;
                    _taxiOrientation = Orientation.Left;
                    backBoosterOn = true;
                    break;
                case "STOP_BOOSTER_LEFT":
                    force.X = 0;
                    backBoosterOn = false;
                    break;
                case "BOOSTER_RIGHT":
                    force.X = boosterForce;
                    _taxiOrientation = Orientation.Right;
                    backBoosterOn = true;
                    break;
                case "STOP_BOOSTER_RIGHT":
                    force.X = 0;
                    backBoosterOn = false;
                    break;
                }
            }
        }
    }
}

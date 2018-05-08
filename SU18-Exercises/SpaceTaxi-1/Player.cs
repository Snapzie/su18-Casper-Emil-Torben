using System;
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
        public Vec2F force;
        public float Gravity { get; private set; }
        private int gravityOn = 1; //Set to zero to disable gravity
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
                        
            Gravity = -0.01f;
            Entity = new Entity(shape, taxiBoosterOffImageLeft);
            force = new Vec2F(0, 0);
            shape.Direction = new Vec2F(0, 0);
            Velocity = new Vec2F(0, 0);
            SetPosition(0.45f, 0.6f);
            SetExtent(0.1f, 0.1f);
            
        }

        public void SetPosition(float x, float y)
        {
            shape.Position.X = x;
            shape.Position.Y = y;
        }

        public void SetDirrection(float x, float y) {
            shape.Direction.X = x;
            shape.Direction.Y = y;
        }

        public void SetExtent(float width, float height)
        {
            shape.Extent.X = width;
            shape.Extent.Y = height;
        }

        public void SetForce(float x, float y) {
            force.X = x;
            force.Y = y;
        }

        public void SetGravity(bool on) {
            if (on) {
                gravityOn = 1;
                //force.Y += Gravity;
            } else {
                gravityOn = 0;
                //force.Y -= Gravity;
            }
        }

        public void RenderPlayer() {
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
            shape.Direction.X += (1.0f / Game.GameTimer.CapturedFrames) * force.X;
            shape.Direction.Y += (1.0f / Game.GameTimer.CapturedFrames) * (force.Y + Gravity * gravityOn);
            shape.Move();
            Entity.RenderEntity();
            
        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent)
        {
            if (eventType == GameEventType.PlayerEvent)
            {
                switch (gameEvent.Message) {
                case "BOOSTER_UPWARDS":
                    force.Y = -Gravity * 2;
                    gravityOn = 1;
                    bottomBosterOn = true;
                    break;
                case "STOP_BOOSTER_UPWARDS":
                    force.Y = 0;
                    gravityOn = 1;
                    bottomBosterOn = false;
                    break;
                case "BOOSTER_LEFT":
                    //ONly allowed to use booster when not landed
                    force.X = -boosterForce * gravityOn;
                    _taxiOrientation = Orientation.Left;
                    backBoosterOn = true;
                    break;
                case "STOP_BOOSTER_LEFT":
                    force.X = 0;
                    backBoosterOn = false;
                    break;
                case "BOOSTER_RIGHT":
                    //ONly allowed to use booster when not landed
                    force.X = boosterForce  * gravityOn;
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

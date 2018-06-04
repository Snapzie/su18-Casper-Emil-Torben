﻿using System;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;

namespace SpaceTaxi_1
{
    public class Player : IGameEventProcessor<object> {
        public Entity Entity { get;}
        public Vec2F Force;
        public float Gravity { get;}
        private readonly DynamicShape shape;
        private Image taxiBoosterOffImageLeft;
        private Image taxiBoosterOffImageRight;
        private ImageStride taxiBoosterOnImageRight;
        private ImageStride taxiBoosterOnImageLeft;
        private ImageStride taxiBoosterOnImageUpLeft;
        private ImageStride taxiBoosterOnImageUpRight;
        private ImageStride taxiBoosterOnImageRightUp;
        private ImageStride taxiBoosterOnImageLeftUp;
        private Orientation taxiOrientation;
        private int gravityOn = 1; //Set to zero to disable gravity
        private float boosterForce = 0.01f;
        private bool backBoosterOn = false;
        private bool bottomBoosterOn = false;
        private GameTimer gameTimer;
        

        public Player(GameTimer gameTimer)
        {
            shape = new DynamicShape(new Vec2F(), new Vec2F());
               
            Gravity = -0.005f;
            Entity = new Entity(shape, taxiBoosterOffImageLeft);
            Force = new Vec2F(0, 0);
            shape.Direction = new Vec2F(0, 0);
            SetPosition(0.45f, 0.6f);
            SetExtent(0.06f, 0.06f);
            SetImages();
            this.gameTimer = gameTimer;
        }

        public void SetImages() {
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
        }
        
        /// <summary>lic Entity Entity 
        /// Sets the position
        /// </summary>
        /// <param name="x">x position</param>
        /// <param name="y">y position</param>
        public void SetPosition(float x, float y) {
            shape.Position.X = x;
            shape.Position.Y = y;
        }
        
        /// <summary>
        /// Sets the direction
        /// </summary>
        /// <param name="x">x direction</param>
        /// <param name="y">y direction</param>
        public void SetDirrection(float x, float y) {
            shape.Direction.X = x;
            shape.Direction.Y = y;
        }
        
        /// <summary>
        /// Sets the extents
        /// </summary>
        /// <param name="width">x extent</param>
        /// <param name="height">y extent</param>
        public void SetExtent(float width, float height) {
            shape.Extent.X = width;
            shape.Extent.Y = height;
        }
        
        /// <summary>
        /// Sets the force
        /// </summary>
        /// <param name="x">x force</param>
        /// <param name="y">y force</param>
        public void SetForce(float x, float y) {
            Force.X = x;
            Force.Y = y;
        }
        
        /// <summary>
        /// Toggles the gravity
        /// </summary>
        /// <param name="on">Bool to determine if the gravity should be on</param>
        public void SetGravity(bool on) {
            gravityOn = on ? 1 : 0;
        }
        
        /// <summary>
        /// Renders the player during the game
        /// </summary>
        public void RenderPlayer() {
            Entity.Image = taxiOrientation == Orientation.Left
                ? taxiBoosterOffImageLeft : taxiBoosterOffImageRight;
            if (taxiOrientation == Orientation.Left) {
                if (backBoosterOn) {
                    Entity.Image = bottomBoosterOn ? taxiBoosterOnImageLeftUp : taxiBoosterOnImageLeft;
                } else {
                    if (bottomBoosterOn) {
                        Entity.Image = taxiBoosterOnImageUpLeft;
                    } else {
                        Entity.Image = taxiBoosterOffImageLeft;
                    }
                }
            } else {
                if (backBoosterOn) {
                    Entity.Image = bottomBoosterOn ? taxiBoosterOnImageRightUp : taxiBoosterOnImageRight;
                } else {
                    if (bottomBoosterOn) {
                        Entity.Image = taxiBoosterOnImageUpRight;
                    } else {
                        Entity.Image = taxiBoosterOffImageRight;
                    }
                }
            }
            shape.Direction.X += (1.0f / gameTimer.CapturedUpdates) * Force.X;
            shape.Direction.Y += (1.0f / gameTimer.CapturedUpdates) * (Force.Y + Gravity * gravityOn);
            shape.Move();
            Entity.RenderEntity();
        }
        
        /// <summary>
        /// Handles the input events
        /// </summary>
        /// <param name="eventType">Eventype</param>
        /// <param name="gameEvent">Game event</param>
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent)
        {
            if (eventType == GameEventType.PlayerEvent)
            {
                switch (gameEvent.Message) {
                case "BOOSTER_UPWARDS":
                    Force.Y = -Gravity * 2;
                    //Make sure gravity turns on, so it's on after takeof
                    gravityOn = 1;
                    bottomBoosterOn = true;
                    break;
                case "STOP_BOOSTER_UPWARDS":
                    Force.Y = 0;
                    gravityOn = 1;
                    bottomBoosterOn = false;
                    break;
                case "BOOSTER_LEFT":
                    //Only allowed to use booster when not landed
                    Force.X = -boosterForce * gravityOn;
                    taxiOrientation = Orientation.Left;
                    backBoosterOn = true;
                    break;
                case "STOP_BOOSTER_LEFT":
                    Force.X = 0;
                    backBoosterOn = false;
                    break;
                case "BOOSTER_RIGHT":
                    //Only allowed to use booster when not landed
                    Force.X = boosterForce  * gravityOn;
                    taxiOrientation = Orientation.Right;
                    backBoosterOn = true;
                    break;
                case "STOP_BOOSTER_RIGHT":
                    Force.X = 0;
                    backBoosterOn = false;
                    break;
                }
            }
        }
    }
}

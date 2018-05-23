﻿using System;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;
using DIKUArcade.State;
using DIKUArcade.Timers;
using SpaceTaxi_1.Customers;
using SpaceTaxi_1.LevelParsing;
using SpaceTaxi_1.SpaceTaxiGame;

namespace SpaceTaxi_1.SpaceTaxiStates {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;
        private Player player;
        private EntityContainer[] levelContainer;
        private int levelNumber = 0;
        private Customer currentCustomer;
        private Customer[] customers;
        private IBaseImage customerImage;
        private CustomerTranslator ct;
        public TimedEventContainer TimedEventContainer;
         
        private GameRunning() {
            levelContainer = new EntityContainer[3];
            TimedEventContainer = new TimedEventContainer(3); //Assuming maximun of 3 customers pr. level
            TimedEventContainer.AttachEventBus(SpaceBus.GetBus());
            customerImage =
                new Image(Path.Combine("Assets", "Images", "CustomerStandLeft.png"));
            ct = new CustomerTranslator();
        }
        
        /// <summary>
        /// Instantiates or returns a GameRunning object with the singleton pattern
        /// </summary>
        /// <returns>Returns a GameRunning object</returns>
        public static GameRunning GetInstance() {
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }
        
        /// <summary>
        /// Called from Game every update and executes the methods needed for the state
        /// </summary>
        public void GameLoop() {
            this.IterateCollisions();
            this.RenderState();
            TimedEventContainer.ProcessTimedEvents();
        }
        
        /// <summary>
        /// Sets the field wich determine which level is loaded
        /// </summary>
        /// <param name="newLevel">The level to load</param>
        public void SetLevel(int newLevel) {
            levelNumber = newLevel;
        }

        /// <summary>
        /// Setup method
        /// </summary>
        public void InitializeGameState() {
            TimedEventContainer.ResetContainer();
            LevelCreator lc = new LevelCreator();
            levelContainer = lc.CreateLevel(levelNumber % LevelsKeeper.Instance.Count());

            Level level = LevelsKeeper.Instance[levelNumber % LevelsKeeper.Instance.Count()];
            customers = ct.MakeCustomers(level.Customers, level.LevelLayout, customerImage);;         
            TimedEventContainer.AttachEventBus(SpaceBus.GetBus());
            player = new Player();
            SpaceBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
        }

        public void UpdateGameLogic() {
            throw new System.NotImplementedException();
        }
        
        /// <summary>
        /// Renders the state
        /// </summary>
        public void RenderState() {
            foreach (EntityContainer entityContainer in levelContainer) {
                entityContainer.RenderEntities();
            }
            player.RenderPlayer();
        }
        
        /// <summary>
        /// Does collision detection by iterating all blocks
        /// </summary>
        public void IterateCollisions() {
            bool collisionDetected = false;
            foreach (Entity platform in levelContainer[0]) {
                if (CollisionDetection.Aabb((DynamicShape) player.Entity.Shape, platform.Shape).Collision) {
                    
                    collisionDetected = true;
                    //Collision with platform from bellow
                    if (((DynamicShape) (player.Entity.Shape)).Direction.Y > 0) {
                        SpaceBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.GameStateEvent, this, "CHANGE_STATE", "GameLost", ""));
                        
                    } //Collision with platform too fast
                    else if (((DynamicShape) (player.Entity.Shape)).Direction.Y < -0.01f) {
                        SpaceBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.GameStateEvent, this, "CHANGE_STATE", "GameLost", ""));
                    } //Landed on platform 
                    else {
                        player.SetDirrection(0, 0);
                        player.SetForce(0, 0);
                        player.SetGravity(false);
                    }
                }
            }
            
            
            foreach (Entity block in levelContainer[1]) {
                if (CollisionDetection.Aabb((DynamicShape) player.Entity.Shape, block.Shape).Collision) {
                    collisionDetected = true;
                    SpaceBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.GameStateEvent, this, "CHANGE_STATE", "GameLost", ""));
                }
            }

            if (!collisionDetected) {
                if (player.Entity.Shape.Position.Y > 1) {
                    SpaceBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.GameStateEvent, this, "CHANGE_STATE", "GameRunning",
                            (levelNumber + 1).ToString()));
                }
            }
        }
        
        public void AddCustomer(Entity customer) {
            levelContainer[2].AddStationaryEntity((StationaryShape)customer.Shape, customer.Image);
        }

        public void RemoveCustomer(Entity entity) {
            entity.DeleteEntity();
            //CustomerIterator kaldes for at iterere over Customers
            //for at fjerne den pågældende customers entity i Customers
            levelContainer[2].Iterate(CustomerIterator);
            
        }
        /// <summary>
        /// Empty method to ensure iteration
        /// </summary>
        /// <param name="customer"></param>
        private void CustomerIterator(Entity customer) {
            
        }
        
        /// <summary>
        /// Processes keyevents
        /// </summary>
        /// <param name="keyValue">The action related to the keyevent</param>
        /// <param name="keyAction">The key pressed related to the keyevent</param>
        public void HandleKeyEvent(string keyValue, string keyAction) {
            if (keyAction == "KEY_PRESS") {
                switch (keyValue) {
                case "KEY_UP":
                    SpaceBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this, "BOOSTER_UPWARDS", "", ""));
                    break;
                case "KEY_LEFT":
                    SpaceBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this, "BOOSTER_LEFT", "", ""));
                    break;
                case "KEY_RIGHT":
                    SpaceBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this, "BOOSTER_RIGHT", "", ""));
                    break;
                case "KEY_ESCAPE":
                    SpaceTaxiGame.SpaceBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.GameStateEvent, this, "CHANGE_STATE", "GamePaused", ""));
                    break;
                }
            } else {
                switch (keyValue) {
                case "KEY_LEFT":
                    SpaceBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this, "STOP_BOOSTER_LEFT", "", ""));
                    break;
                case "KEY_RIGHT":
                    SpaceBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this, "STOP_BOOSTER_RIGHT", "", ""));
                    break;
                case "KEY_UP":
                    SpaceBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.PlayerEvent, this, "STOP_BOOSTER_UPWARDS", "", ""));
                    break;
                }
            }
        }

        public void HandleCustomerEvents(string parameter) {
            AddCustomer(customers[int.Parse(parameter)]);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.State;
using DIKUArcade.Timers;
using SpaceTaxi_1.Customers;
using SpaceTaxi_1.Entities;
using SpaceTaxi_1.LevelParsing;
using SpaceTaxi_1.SpaceTaxiGame;
using Image = DIKUArcade.Graphics.Image;

namespace SpaceTaxi_1.SpaceTaxiStates {
    public class GameRunning : IGameState {
        public TimedEventContainer TimedEventContainer;
        private static GameRunning instance = null;
        private Player player;
        private EntityContainer<Entity>[] levelContainer;
        private int levelNumber = 0;
        private Customer currentCustomer;
        private Customer[] customers;
        private IBaseImage customerImage;
        private CustomerTranslator ct;
        private int points;
        private Text pointsText;
        
        private GameRunning() {
           NewGame();
        }
        
        /// <summary>
        /// Instantiates or returns a GameRunning object with the singleton pattern
        /// </summary>
        /// <returns>Returns a GameRunning object</returns>
        public static GameRunning GetInstance() {
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }
        
        /// <summary>
        /// Resets the game
        /// </summary>
        public void NewGame() {
            currentCustomer = null;
            points = 0;
            levelContainer = new EntityContainer<Entity>[3]; //Assuming maximun of 3 customers pr. level
            TimedEventContainer = new TimedEventContainer(3); //Assuming maximun of 3 customers pr. level
            TimedEventContainer.AttachEventBus(SpaceBus.GetBus());
            customerImage =
                new Image(Path.Combine("Assets", "Images", "CustomerStandLeft.png"));
            ct = new CustomerTranslator();
            pointsText = new Text("Points: 0", new Vec2F(0.06f,-0.12f), new Vec2F(0.2f,0.2f));
            pointsText.SetColor(Color.White);
       }
        
        /// <summary>
        /// Called from Game every update and executes the methods needed for the state
        /// </summary>
        public void GameLoop() {
            this.IterateCollisions();
            this.RenderState();
            TimedEventContainer.ProcessTimedEvents();
            pointsText.SetText("Points: " + points);
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
            player.SetImages(); 
            SpaceBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);
        }

        public void UpdateGameLogic() {
            throw new System.NotImplementedException();
        }
        
        /// <summary>
        /// Renders the state
        /// </summary>
        public void RenderState() {
            foreach (EntityContainer<Entity> entityContainer in levelContainer) {
                entityContainer.RenderEntities();
            }
            player.RenderPlayer();
            pointsText.RenderText();
        }
        
        /// <summary>
        /// Does collision detection by iterating all blocks, platforms and customers
        /// </summary>
        public void IterateCollisions() {
            bool collisionDetected = false;
            foreach (Platform platform in levelContainer[0]) {
                if (CollisionDetection.Aabb((DynamicShape) player.Entity.Shape, platform.Shape).Collision) {
                    
                    collisionDetected = true;
                    //Collision with platform from bellow
                    if (((DynamicShape) (player.Entity.Shape)).Direction.Y > 0) {
                        BelowPlatform();
                    } //Collision with platform too fast
                    else if (((DynamicShape) (player.Entity.Shape)).Direction.Y < -0.004f) {
                        CrashingPlatform();
                    } //Landed on platform 
                    else {
                        LandingPlatform(platform);
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

            foreach (Customer customer in levelContainer[2]) {
                if (CollisionDetection.Aabb((DynamicShape) player.Entity.Shape, customer.Shape).Collision) {
                    if (currentCustomer == null) {
                        collisionDetected = true;
                        currentCustomer = customer;
                        customer.PickUpTime = StaticTimer.GetElapsedSeconds();
                        RemoveCustomer(customer);
                    }
                }
            }

            if (!collisionDetected) {
                if (player.Entity.Shape.Position.Y > 1) {
                    SpaceBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.GameStateEvent, this, "CHANGE_STATE", "GameRunning",
                            (levelNumber + 1).ToString()));
                    if (currentCustomer != null) {
                        currentCustomer.CrossedBorder = true;
                    }
                }
            }
        }
        
        /// <summary>
        /// Handles the what happens when the player hits a block from below
        /// </summary>
        private void BelowPlatform() {
            SpaceBus.GetBus().RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.GameStateEvent, this, "CHANGE_STATE", "GameLost", ""));
        }
        
        /// <summary>
        /// Handles the what happens when the player hits a block too fast
        /// </summary>
        private void CrashingPlatform() {
            SpaceBus.GetBus().RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.GameStateEvent, this, "CHANGE_STATE", "GameLost", ""));
        }
        
        /// <summary>
        /// Handles the what happens when the player will land on a platform
        /// </summary>
        /// <param name="platform">The platform to collide with</param>
        private void LandingPlatform(Platform platform) {
            player.SetDirrection(0, 0);
            player.SetForce(0, 0);
            player.SetGravity(false);
            if (currentCustomer != null &&
                currentCustomer.DestinationPlatform.Contains("^") ==
                currentCustomer.CrossedBorder) {
                //We are in correct level
                if (currentCustomer.DestinationPlatform.Length == 1) {
                    if (currentCustomer.DestinationPlatform[0] == '^') {
                        points += currentCustomer.CalculatePoints();
                        currentCustomer = null;
                    } else if (currentCustomer.DestinationPlatform[0] == platform.Identifier) {
                        points += currentCustomer.CalculatePoints();
                        currentCustomer = null;
                    }
                } else if (currentCustomer.DestinationPlatform[1] ==
                           platform.Identifier) {
                    points += currentCustomer.CalculatePoints();
                    currentCustomer = null;
                }
            }
        }
        
        /// <summary>
        /// Adds the customer to the rendering EntityContainer
        /// </summary>
        /// <param name="customer">The customer to add</param>
        public void AddCustomer(Entity customer) {
            levelContainer[2].AddStationaryEntity(customer);
        }
        
        /// <summary>
        /// Removes a customer from the rendering EntityContainer
        /// </summary>
        /// <param name="entity">The customer to remove</param>
        public void RemoveCustomer(Entity entity) {
            entity.DeleteEntity();
            //CustomerIterator kaldes for at fjerne slettede customers
            levelContainer[2].Iterate(CustomerIterator);
            
        }

        /// <summary>
        /// Empty method to ensure deletion of customer from its Container
        /// </summary>
        private void CustomerIterator(Entity entity) {
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
        
        /// <summary>
        /// Handles the timed event call to add a customer to the rendering EntityContainer
        /// </summary>
        /// <param name="parameter">The identifier of a customer</param>
        public void HandleCustomerEvents(string parameter) {
            AddCustomer(customers[int.Parse(parameter)]);
        }
    }
}
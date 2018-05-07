using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.State;
using SpaceTaxi_1.LevelParsing;
using SpaceTaxi_1.SpaceTaxiGame;

namespace SpaceTaxi_1.SpaceTaxiStates {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;
        private Player player;
        private EntityContainer[] levelContainer;
        private int levelNumber = 0;
        private GameRunning() {
            InitializeGameState();
        }

        public static GameRunning GetInstance() {
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }
        
        public void GameLoop() {
            this.IterateCollisions();
            this.RenderState();
        }

        public void SetLevel(int newLevel) {
            levelNumber = newLevel;
        }

        public void InitializeGameState() {
            //TODO:CHange game flow
            levelContainer = LevelCreator.CreateLevel(levelNumber);
            player = new Player();
            player.SetPosition(0.45f, 0.6f);
            player.SetExtent(0.1f, 0.1f);
            SpaceBus.GetBus().Subscribe(GameEventType.PlayerEvent, player);

        }

        public void UpdateGameLogic() {
            throw new System.NotImplementedException();
        }

        public void RenderState() {
            foreach (EntityContainer entityContainer in levelContainer) {
                entityContainer.RenderEntities();
            }
            player.RenderPlayer();
        }

        public void IterateCollisions() {
            //Console.WriteLine("Dectection");
            foreach (Entity platform in levelContainer[0]) {
                if (CollisionDetection.Aabb((DynamicShape) player.Entity.Shape, platform.Shape).Collision) {
                    Console.WriteLine("Platform");
                    if (player.Velocity.Y < 0) {
                        //Loose Game
                        Console.WriteLine("Collision");
                    }else if (player.Velocity.Y > 2) {
                        //Loose Game
                        Console.WriteLine("Collision");
                    } else {
                        player.Velocity.Y = 0;
                    }
                }
            }
            
            foreach (Entity block in levelContainer[1]) {
                //Console.WriteLine(player.Entity.Shape.Position);
                if (CollisionDetection.Aabb((DynamicShape) player.Entity.Shape, block.Shape).Collision) {
                    Console.WriteLine("Collision");
                    player.SetExtent(0, 0); //TODO: loose game
                }
            }
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            //Keeping all the keys in so we have them for later
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
    }
}
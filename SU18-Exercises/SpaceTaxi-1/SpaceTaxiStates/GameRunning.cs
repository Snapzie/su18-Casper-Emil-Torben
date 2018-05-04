using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.State;
using SpaceTaxi_1.LevelParsing;

namespace SpaceTaxi_1.SpaceTaxiStates {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;
        private Player player;
        private EntityContainer levelContainer;

        private GameRunning() {
            InitializeGameState();
        }

        public static GameRunning GetInstance() {
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }
        
        public void GameLoop() {
            this.RenderState();
        }
        


        public void InitializeGameState() {
            //TODO:CHange game flow
            levelContainer = LevelCreator.CreateLevel(2);
            player = new Player();
            
        }

        public void UpdateGameLogic() {
            throw new System.NotImplementedException();
        }

        public void RenderState() {
            levelContainer.RenderEntities();
            player.RenderPlayer();
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            //Keeping all the keys in so we have them for later
            if (keyAction == "KEY_PRESS") {
                switch (keyValue) {
                case "KEY_SPACE":
                    
                    break;
                case "KEY_LEFT":
                    
                    break;   
                case "KEY_RIGHT":
                    
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
                    break;
                case "KEY_RIGHT":
                    break;
                }
            }
        }
    }
}
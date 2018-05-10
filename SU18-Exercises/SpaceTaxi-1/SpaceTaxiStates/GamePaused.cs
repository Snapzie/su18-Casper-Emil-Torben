using System;
using System.Drawing;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
using SpaceTaxi_1.SpaceTaxiGame;

namespace SpaceTaxi_1.SpaceTaxiStates {
    public class GamePaused : IGameState {
        private static GamePaused instance = null;
        
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;


        private GamePaused() {
            menuButtons = new Text[] {
                new Text("Continue", new Vec2F(0.4f, 0.4f), new Vec2F(0.3f, 0.3f)),
                new Text("Main Menu", new Vec2F(0.4f, 0.3f), new Vec2F(0.3f, 0.3f))
            };
            activeMenuButton = 0;
            maxMenuButtons = 2;
        }
        
        /// <summary>
        /// Instantiates or returns a GamePaused object with the singleton pattern
        /// </summary>
        /// <returns>Returns a GamePaused object</returns>
        public static GamePaused GetInstance() {
            return GamePaused.instance ?? (GamePaused.instance = new GamePaused());
        }
        
        /// <summary>
        /// Called from Game every update and executes the methods needed for the state
        /// </summary>
        public void GameLoop() {
            this.RenderState();
        }

        public void InitializeGameState() {
            throw new System.NotImplementedException();
        }

        public void UpdateGameLogic() {
            throw new System.NotImplementedException();
        }
        
        /// <summary>
        /// Renders the state
        /// </summary>
        public void RenderState() {
            foreach (Text text in this.menuButtons) {
                text.SetColor(Color.Blue);
                this.menuButtons[activeMenuButton].SetColor(Color.Red);
                text.RenderText();
            }
        }
        
        /// <summary>
        /// Processes keyevents
        /// </summary>
        /// <param name="keyValue">The action related to the keyevent</param>
        /// <param name="keyAction">The key pressed related to the keyevent</param>
        public void HandleKeyEvent(string keyValue, string keyAction) {
            if (keyAction == "KEY_RELEASE") {
                switch (keyValue) {
                case "KEY_ENTER" :
                    if (activeMenuButton == 0) {
                        SpaceBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.GameStateEvent, 
                                this, 
                                "CHANGE_STATE", 
                                "GameRunning", 
                                ""));
                    } else {
                        SpaceBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.GameStateEvent, 
                                this, 
                                "CHANGE_STATE", 
                                "MainMenu", 
                                ""));
                    }
                    break;
                case "KEY_UP" :
                    activeMenuButton = Math.Abs(activeMenuButton - 1);
                    break;
                case "KEY_DOWN" :
                    activeMenuButton = (activeMenuButton + 1) % 2;
                    break;
                }   
            }
        }
    }
}
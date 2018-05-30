using System.Drawing;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
using SpaceTaxi_1.SpaceTaxiGame;

namespace SpaceTaxi_1.SpaceTaxiStates {
    public class GameLost : IGameState {
        private static GameLost instance = null;
        private Text[] gameLostTexts;
        
        private GameLost() {
            gameLostTexts = new Text[] {
                new Text("Game Lost!", new Vec2F(0.4f, 0.4f), new Vec2F(0.3f, 0.3f)),
                new Text("Enter to continue", new Vec2F(0.36f, 0.3f), new Vec2F(0.3f, 0.3f))
            };    
        }
        
        /// <summary>
        /// Instantiates or returns a GameLost object with the singleton pattern
        /// </summary>
        /// <returns>Returns a GameLost object</returns>
        public static GameLost GetInstance() {
            return GameLost.instance ?? (GameLost.instance = new GameLost());
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
            foreach (Text text in this.gameLostTexts) {
                text.SetColor(Color.Red);
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
                if (keyValue == "KEY_ENTER") {
                    SpaceBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.GameStateEvent, this, "STATE_CHANGE", "MainMenu", ""));
                }
            }
        }
    }
}
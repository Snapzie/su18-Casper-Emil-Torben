using System.Drawing;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
using Galaga_Exercise_3._1.GalagaGame;

namespace Galaga_Exercise_3._1.GalagaStates {
    public class GameLost : IGameState {
        private static GameLost instance = null;
        private Text[] GameLostTexts;

        private GameLost() {
            GameLostTexts = new Text[] {
                new Text("Game Lost!", new Vec2F(0.4f, 0.4f), new Vec2F(0.3f, 0.3f)),
                new Text("Enter to continue", new Vec2F(0.36f, 0.3f), new Vec2F(0.3f, 0.3f))
            };    
        }
        
        public static GameLost GetInstance() {
            return GameLost.instance ?? (GameLost.instance = new GameLost());
        }
        
        public void GameLoop() {
            this.RenderState();
        }

        public void InitializeGameState() {
            throw new System.NotImplementedException();
        }

        public void UpdateGameLogic() {
            throw new System.NotImplementedException();
        }

        public void RenderState() {
            foreach (Text text in this.GameLostTexts) {
                text.SetColor(Color.Red);
                text.RenderText();
            }
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            if (keyAction == "KEY_RELEASE") {
                if (keyValue == "KEY_ENTER") {
                    GalagaBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.GameStateEvent, this, "STATE_CHANGE", "MainMenu", ""));
                }
            }
        }
    }
}
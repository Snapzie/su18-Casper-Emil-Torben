using System;
using System.Drawing;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
using Galaga_Exercise_3._1.GalagaGame;
using Image = DIKUArcade.Graphics.Image;

namespace Galaga_Exercise_3._1.GalagaStates {
    public class MainMenu : IGameState {
        private static MainMenu instance = null;
        
        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;

        private MainMenu() {
            backGroundImage = new Entity(new StationaryShape(0, 0, 1, 1), 
                new Image("Assets/Images/TitleImage.png"));
            menuButtons = new Text[] {
                new Text("New Game", new Vec2F(0.4f, 0.4f), new Vec2F(0.3f, 0.3f)),
                new Text("Quit", new Vec2F(0.4f, 0.2f), new Vec2F(0.3f, 0.3f))
            };
            activeMenuButton = 0;
            maxMenuButtons = 2;
        }

        public static MainMenu GetInstance() {
            return MainMenu.instance ?? (MainMenu.instance = new MainMenu());
        }
        
        public void GameLoop() {
            throw new System.NotImplementedException();
        }

        public void InitializeGameState() {
            throw new System.NotImplementedException();
        }

        public void UpdateGameLogic() {
            throw new System.NotImplementedException();
        }

        public void RenderState() {
            this.backGroundImage.RenderEntity();
            foreach (Text text in this.menuButtons) {
                text.SetColor(Color.Blue);
                this.menuButtons[activeMenuButton].SetColor(Color.Red);
                text.RenderText();
            }
            
            
            
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            if (keyAction == "KEY_RELEASE") {
                switch (keyValue) {
                    case "KEY_ENTER" :
                        if (activeMenuButton == 0) {
                            GalagaBus.GetBus().RegisterEvent(
                                GameEventFactory<object>.CreateGameEventForAllProcessors(
                                    GameEventType.GameStateEvent, 
                                    this, 
                                    "CHANGE_STATE", 
                                    "GameRunning", 
                                    ""));
                        }
                        break;
                    //TODO: Wrapping modulu
                    case "KEY_UP" :
                        activeMenuButton -= 1;
                        break;
                    case "KEY_DOWN" :
                        activeMenuButton += 1;
                        break;
                    default:
                        throw new ArgumentException();
                }   
            }
        }
    }
}
using System;
using System.Drawing;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
using SpaceTaxi_1.SpaceTaxiGame;
using Image = DIKUArcade.Graphics.Image;

namespace SpaceTaxi_1.SpaceTaxiStates {
    public class MainMenu : IGameState {
        private static MainMenu instance = null;
        
        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;

        private MainMenu() {
            backGroundImage = new Entity(new StationaryShape(0.0f, 0.0f, 1, 1), 
                new Image("Assets/Images/SpaceBackground.png"));
            menuButtons = new Text[] {
                new Text("New Game", new Vec2F(0.4f, 0.4f), new Vec2F(0.3f, 0.3f)),
                new Text("Quit", new Vec2F(0.4f, 0.3f), new Vec2F(0.3f, 0.3f))
            };
            activeMenuButton = 0;
            maxMenuButtons = 2;
        }

        public static MainMenu GetInstance() {
            return MainMenu.instance ?? (MainMenu.instance = new MainMenu());
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
                                GameEventType.WindowEvent, this, "CLOSE_WINDOW", "", ""));
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
using System.Drawing;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
using SpaceTaxi_1.LevelParsing;
using SpaceTaxi_1.SpaceTaxiGame;
using Image = DIKUArcade.Graphics.Image;

namespace SpaceTaxi_1.SpaceTaxiStates {
    public class MainMenu : IGameState {
        private static MainMenu instance = null;
        
        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;
        private int selectedLevel = 0;
        private LevelsKeeper levelsKeeper = LevelsKeeper.Instance;
        
        private MainMenu() {
            backGroundImage = new Entity(new StationaryShape(0.0f, 0.0f, 1, 1), 
                new Image("Assets/Images/SpaceBackground.png"));    
            menuButtons = new Text[] {
                new Text("New Game", new Vec2F(0.1f, 0.4f), new Vec2F(0.3f, 0.3f)),
                new Text("Selected Level: \n" + levelsKeeper[selectedLevel].Name, 
                    new Vec2F(0.1f, 0.3f), new Vec2F(0.3f, 0.3f)),
                new Text("Quit", new Vec2F(0.1f, 0.2f), new Vec2F(0.3f, 0.3f))
            };
            menuButtons[0].SetFontSize(42);
            menuButtons[1].SetFontSize(42);
            menuButtons[2].SetFontSize(42);
            activeMenuButton = 0;
            maxMenuButtons = menuButtons.Length;
            
        }
        
        /// <summary>
        /// Instantiates or returns a GameLost object with the singleton pattern
        /// </summary>
        /// <returns>Returns a GameLost object</returns>
        public static MainMenu GetInstance() {
            return MainMenu.instance ?? (MainMenu.instance = new MainMenu());
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
            this.backGroundImage.RenderEntity();
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
                                    selectedLevel.ToString()));
                        } else if (activeMenuButton == 1) {
                            selectedLevel = (selectedLevel + 1) % levelsKeeper.Count();
                            menuButtons[1].SetText("Selected Level: \n" + levelsKeeper[selectedLevel].Name);
                        } else {
                            SpaceBus.GetBus().RegisterEvent(
                                GameEventFactory<object>.CreateGameEventForAllProcessors(
                                    GameEventType.WindowEvent, this, "CLOSE_WINDOW", "", ""));
                        }
                        break;
                    case "KEY_UP" :
                        if (activeMenuButton - 1 >= 0) {
                            activeMenuButton--;
                        } else {
                            activeMenuButton = maxMenuButtons - 1;
                        }
                        break;
                    case "KEY_DOWN" :
                        activeMenuButton = (activeMenuButton + 1) % maxMenuButtons;
                        break;
                }   
            }
        }
    }
}
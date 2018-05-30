using NUnit.Framework;
using SpaceTaxi_1.LevelParsing;
using SpaceTaxi_1.SpaceTaxiStates;

namespace Tests {
    [TestFixture]
    public class MainMenuTests {
        private StateMachine stateMachine;
        private LevelsKeeper levelsKeeper = LevelsKeeper.Instance;
        private MainMenu mainMenu = MainMenu.GetInstance();

        [SetUp]
        private void SetUp() {
            DIKUArcade.Window.CreateOpenGLContext();
            stateMachine = new StateMachine();
        }


        [Test]
        public void HandleKeyEventNewGame() {
            mainMenu.HandleKeyEvent("KEY_ENTER", "KEY_RELEASE");
            Assert.AreEqual(stateMachine.ActiveState, GameRunning.GetInstance());
        }

        // [Test]
       // public void HandleKeyEventQuitGame() {
           // mainMenu.HandleKeyEvent("KEY_DOWN", "KEY_RELEASE");
            //mainMenu.HandleKeyEvent("KEY_DOWN", "KEY_RELEASE");
           
        }
        
    }

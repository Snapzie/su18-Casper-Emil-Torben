using NUnit.Framework;
using SpaceTaxi_1.LevelParsing;
using SpaceTaxi_1.SpaceTaxiStates;
using SpaceTaxi_1.SpaceTaxiGame;
using System.Collections.Generic;

using DIKUArcade.EventBus;
namespace Tests {
    [TestFixture]
    public class MainMenuTests {
        private StateMachine stateMachine;
        private MainMenu mainMenu;
        
        [SetUp]
        public void SetUp() {
            DIKUArcade.Window.CreateOpenGLContext();
            stateMachine = new StateMachine();
            mainMenu = MainMenu.GetInstance();
            SpaceBus.GetBus().InitializeEventBus(new List<GameEventType>() {
                GameEventType.GameStateEvent
            });
        }


        [Test]
        public void TestSelectedLevel() {
            Assert.AreEqual(mainMenu.selectedLevel, 0);
            mainMenu.HandleKeyEvent("KEY_DOWN", "KEY_RELEASE");
            mainMenu.HandleKeyEvent("KEY_ENTER", "KEY_RELEASE");
            Assert.AreEqual(mainMenu.selectedLevel, 1);
            
        }
           
        }
        
    }

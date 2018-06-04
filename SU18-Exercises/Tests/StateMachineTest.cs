using DIKUArcade.EventBus;
using NUnit.Framework;
using SpaceTaxi_1.SpaceTaxiStates;
using SpaceTaxi_1.SpaceTaxiGame;
using System.Collections.Generic;

namespace Tests {
    [TestFixture]
    public class StateMachineTest {
        private StateMachine stateMachine;
        private GameEvent<object> gameEvent;

        [SetUp]
        public void SetUp() {
            DIKUArcade.Window.CreateOpenGLContext();
            GameEventBus<object> eventBus = SpaceBus.GetBus();
            eventBus.InitializeEventBus(new List<GameEventType>() {
                GameEventType.GameStateEvent,
                GameEventType.InputEvent,      // key press / key release
                GameEventType.WindowEvent,     // messages to the window, e.g. CloseWindow()
                GameEventType.PlayerEvent,      // commands issued to the player object, e.g. move, destroy, receive health, etc.
                GameEventType.TimedEvent
            });
            stateMachine = new StateMachine();
            
        }

        [Test]
        public void StateMachineInstatiationTest() {
            Assert.AreEqual(stateMachine.ActiveState, MainMenu.GetInstance());
        }

        [Test]
        public void ChangeStateToGameRunning() {
            gameEvent = new GameEvent<object>() {
                EventType = GameEventType.GameStateEvent,
                Parameter1 = "GameRunning",
                Parameter2 = "0"
            };
            stateMachine.ProcessEvent(GameEventType.GameStateEvent, gameEvent);
            Assert.AreEqual(stateMachine.ActiveState, GameRunning.GetInstance());
        }

        [Test]
        public void ChangeStateToGamePaused() {
            gameEvent = new GameEvent<object>() {
                EventType = GameEventType.GameStateEvent,
                Parameter1 = "GamePaused"
            };
            stateMachine.ProcessEvent(GameEventType.GameStateEvent, gameEvent);
            Assert.AreEqual(stateMachine.ActiveState, GamePaused.GetInstance());
        }

        [Test]
        public void ChangeStateToMainMenu() {
            gameEvent = new GameEvent<object>() {
                EventType = GameEventType.GameStateEvent,
                Parameter1 = "MainMenu"
            };
            stateMachine.ProcessEvent(GameEventType.GameStateEvent, gameEvent);
            Assert.AreEqual(stateMachine.ActiveState, MainMenu.GetInstance());
        }

        [Test]
        public void ChangeStateToGameLost() {
            gameEvent = new GameEvent<object>() {
                EventType = GameEventType.GameStateEvent,
                Parameter1 = "GameLost"
            };
            stateMachine.ProcessEvent(GameEventType.GameStateEvent, gameEvent);
            Assert.AreEqual(stateMachine.ActiveState, GameLost.GetInstance());
        }
    }
}
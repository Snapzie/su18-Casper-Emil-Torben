using DIKUArcade.EventBus;
using NUnit.Framework;
using SpaceTaxi_1;
using SpaceTaxi_1.SpaceTaxiStates;

namespace Tests {
    [TestFixture]
    public class GameRunningTests {
        private StateMachine stateMachine;
        private GameEvent<object> gameEvent;
        private Player player;

        [SetUp]
        private void SetUp() {
            stateMachine = new StateMachine();
            player = new Player();
        }

        [Test]
        private void TestDeadlyColision() {
            gameEvent = new GameEvent<object>() {
                EventType = GameEventType.PlayerEvent,
                Parameter1 = "GameRunning"
            };
            stateMachine.ProcessEvent(GameEventType.GameStateEvent, gameEvent);
            player.SetDirrection(1, 1);
            player.SetPosition(0.25f, 1 / 23f);
            GameRunning.GetInstance().IterateCollisions();
            Assert.AreEqual(stateMachine.ActiveState, GameLost.GetInstance());
        }

        [Test]
        private void TestLandingOnPlatform() {
            gameEvent = new GameEvent<object>() {
                EventType = GameEventType.PlayerEvent,
                Parameter1 = "GameRunning"
            };
            stateMachine.ProcessEvent(GameEventType.GameStateEvent, gameEvent);
            player.SetDirrection(1, 0);
            player.SetPosition(0.25f, 1 / 23f);
            GameRunning.GetInstance().IterateCollisions();
            Assert.AreEqual(stateMachine.ActiveState, GameRunning.GetInstance());
        }
    }
}
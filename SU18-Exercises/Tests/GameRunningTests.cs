using DIKUArcade.EventBus;
using NUnit.Framework;
using SpaceTaxi_1;
using SpaceTaxi_1.SpaceTaxiStates;
using SpaceTaxi_1.LevelParsing;

namespace Tests {
    [TestFixture]
    public class GameRunningTests {
    //    private StateMachine stateMachine;
    //    private GameEvent<object> gameEvent;
    //    private Player player;

    //    [SetUp]
    //    public void SetUp() {
    //        LevelLoader levelLoader = new LevelLoader();
    //        levelLoader.LoadLevels();
    //        stateMachine = new StateMachine();
    //        player = new Player();
            
            
    //    }

    //    [Test]
    //    public void TestDeadlyColision() {
    //        gameEvent = new GameEvent<object>() {
    //            EventType = GameEventType.GameStateEvent,
    //            Parameter1 = "GameRunning",
    //            Parameter2 = "0"
    //        };
    //        stateMachine.ProcessEvent(GameEventType.GameStateEvent, gameEvent);
    //        player.SetDirrection(0.1f, 0.1f);
    //        player.SetPosition(0.25f, 1 / 23f);
    //        GameRunning.GetInstance().IterateCollisions();
    //        Assert.AreEqual(GameLost.GetInstance(), stateMachine.ActiveState);
    //    }

    //    [Test]
    //    public void TestLandingOnPlatform() {
    //        gameEvent = new GameEvent<object>()
    //        {
    //            EventType = GameEventType.GameStateEvent,
    //            Parameter1 = "GameRunning",
    //            Parameter2 = "0"
    //        };
    //        stateMachine.ProcessEvent(GameEventType.GameStateEvent, gameEvent);
    //        player.SetDirrection(0, -0.001f);
    //        player.SetPosition(0.25f, 1 / 23f);
    //        GameRunning.GetInstance().IterateCollisions();
    //        Assert.AreEqual(stateMachine.ActiveState, GameRunning.GetInstance());
    //    }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade.EventBus;
using NUnit.Framework;
using SpaceTaxi_1;
using SpaceTaxi_1.LevelParsing;
using SpaceTaxi_1.SpaceTaxiStates;
using DIKUArcade.Timers;

namespace Tests {
    namespace Tests {
        [TestFixture]
        public class StateTests {
            private StateMachine stateMachine;
            private Player p;
            private GameTimer gameTimer;
            [SetUp]
            public void SetUp() {
                gameTimer = new GameTimer(60, 60);
                p = new Player(gameTimer);
                
                stateMachine = new StateMachine();
                GameEvent<object> gameEvent = new GameEvent<object>() {
                    EventType = GameEventType.GameStateEvent,
                    Parameter1 = "GameRunning",
                    Parameter2 = "0"
                };
                stateMachine.ProcessEvent(GameEventType.GameStateEvent, gameEvent);
                
            }

            [Test]
            public void ForceTest() {
                Assert.AreEqual(p.Force.X, 0);
                Assert.AreEqual(p.Force.Y, 0);
            }

            [Test]
            public void GravityTest() {
                double startTime = StaticTimer.GetElapsedSeconds();
                double nowTime;
                //Let game run for a bit more than a second so gameTimer will update fps
                while((nowTime = StaticTimer.GetElapsedSeconds()) - startTime < 1.2) {
                    gameTimer.MeasureTime();
                    gameTimer.ShouldRender();
                    gameTimer.ShouldReset();
                    gameTimer.ShouldUpdate();
                }
                p.RenderPlayer();
                Assert.AreEqual(0, p.Entity.Shape.AsDynamicShape().Direction.X);
                Assert.Less(p.Entity.Shape.AsDynamicShape().Direction.Y, 0);

            }

            [Test]
            public void BoosterUpTest() {
                GameEvent<object> gameEvent = new GameEvent<object>() {
                    EventType = GameEventType.PlayerEvent,
                    Message = "BOOSTER_UPWARDS"
                };
                p.ProcessEvent(GameEventType.PlayerEvent, gameEvent);
                Assert.AreEqual(p.Force.X, 0);
                Assert.AreEqual(p.Force.Y, -p.Gravity * 2);
                gameEvent = new GameEvent<object>() {
                    EventType = GameEventType.PlayerEvent,
                    Message = "STOP_BOOSTER_UPWARDS"
                };
                p.ProcessEvent(GameEventType.PlayerEvent, gameEvent);
                Assert.AreEqual(p.Force.X, 0);
                Assert.AreEqual(p.Force.Y, 0);
            }
            
            
            [Test]
            public void BoosterLeftTest() {
                GameEvent<object> gameEvent = new GameEvent<object>() {
                    EventType = GameEventType.PlayerEvent,
                    Message = "BOOSTER_LEFT"
                };
                p.ProcessEvent(GameEventType.PlayerEvent, gameEvent);
                Assert.AreEqual(p.Force.X, -0.01f);
                
                gameEvent = new GameEvent<object>() {
                    EventType = GameEventType.PlayerEvent,
                    Message = "STOP_BOOSTER_LEFT"
                };
                p.ProcessEvent(GameEventType.PlayerEvent, gameEvent);
                Assert.AreEqual(p.Force.X, 0);
            } 
            
            [Test]
            public void BoosterRightTest() {
                GameEvent<object> gameEvent = new GameEvent<object>() {
                    EventType = GameEventType.PlayerEvent,
                    Message = "BOOSTER_RIGHT"
                };
                p.ProcessEvent(GameEventType.PlayerEvent, gameEvent);
                Assert.AreEqual(p.Force.X, 0.01f);
                
                gameEvent = new GameEvent<object>() {
                    EventType = GameEventType.PlayerEvent,
                    Message = "STOP_BOOSTER_RIGHT"
                };
                p.ProcessEvent(GameEventType.PlayerEvent, gameEvent);
                Assert.AreEqual(p.Force.X, 0);
            }
            [Test]
            public void NonZeroForce() {
            double startTime = StaticTimer.GetElapsedSeconds();
                double nowTime;
                //Let game run for a bit more than a second so gameTimer will update fps
                while((nowTime = StaticTimer.GetElapsedSeconds()) - startTime < 1.2) {
                    gameTimer.MeasureTime();
                    gameTimer.ShouldRender();
                    gameTimer.ShouldReset();
                    gameTimer.ShouldUpdate();
                }
                p.SetForce(1, 1);
                p.RenderPlayer();
                Assert.GreaterOrEqual(p.Entity.Shape.Position.X, 0.45f);
                Assert.GreaterOrEqual(p.Entity.Shape.Position.Y, 0.60f);
                
            }
        }
    }
}
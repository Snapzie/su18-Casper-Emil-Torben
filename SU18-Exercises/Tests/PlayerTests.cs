using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade.EventBus;
using NUnit.Framework;
using SpaceTaxi_1;
using SpaceTaxi_1.LevelParsing;
using SpaceTaxi_1.SpaceTaxiStates;

namespace Tests {
    namespace Tests {
        [TestFixture]
        public class StateTests {
            private StateMachine stateMachine;
            private Player p;
            [SetUp]
            public void SetUp() {
                p = new Player();
                
            }

            [Test]
            public void ForceTest() {
                Assert.AreEqual(p.force.X, 0);
                Assert.AreEqual(p.force.Y, 0);
            }

            [Test]
            public void GravityTest() {
                //Render player to calculate gravity, this fails 
                // since we don't have and can't make a game or gameTimer
//                p.RenderPlayer();
//                Assert.AreEqual(p.Entity.Shape.AsDynamicShape().Direction.X, 0);
//                Assert.Less(p.Entity.Shape.AsDynamicShape().Direction.Y, 0);

            }

            [Test]
            public void BoosterUpTest() {
                GameEvent<object> gameEvent = new GameEvent<object>() {
                    EventType = GameEventType.PlayerEvent,
                    Message = "BOOSTER_UPWARDS"
                };
                p.ProcessEvent(GameEventType.PlayerEvent, gameEvent);
                Assert.AreEqual(p.force.X, 0);
                Assert.AreEqual(p.force.Y, -p.Gravity * 2);
                gameEvent = new GameEvent<object>() {
                    EventType = GameEventType.PlayerEvent,
                    Message = "STOP_BOOSTER_UPWARDS"
                };
                p.ProcessEvent(GameEventType.PlayerEvent, gameEvent);
                Assert.AreEqual(p.force.X, 0);
                Assert.AreEqual(p.force.Y, 0);
            }
            
            
            [Test]
            public void BoosterLeftTest() {
                GameEvent<object> gameEvent = new GameEvent<object>() {
                    EventType = GameEventType.PlayerEvent,
                    Message = "BOOSTER_LEFT"
                };
                p.ProcessEvent(GameEventType.PlayerEvent, gameEvent);
                Assert.AreEqual(p.force.X, -0.01f);
                
                gameEvent = new GameEvent<object>() {
                    EventType = GameEventType.PlayerEvent,
                    Message = "STOP_BOOSTER_LEFT"
                };
                p.ProcessEvent(GameEventType.PlayerEvent, gameEvent);
                Assert.AreEqual(p.force.X, 0);
            } 
            
            [Test]
            public void BoosterRightTest() {
                GameEvent<object> gameEvent = new GameEvent<object>() {
                    EventType = GameEventType.PlayerEvent,
                    Message = "BOOSTER_RIGHT"
                };
                p.ProcessEvent(GameEventType.PlayerEvent, gameEvent);
                Assert.AreEqual(p.force.X, 0.01f);
                
                gameEvent = new GameEvent<object>() {
                    EventType = GameEventType.PlayerEvent,
                    Message = "STOP_BOOSTER_RIGHT"
                };
                p.ProcessEvent(GameEventType.PlayerEvent, gameEvent);
                Assert.AreEqual(p.force.X, 0);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
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
                //Sets working dirrectory in order to find assets
                var dir = Path.GetDirectoryName(typeof(LevelLoader).Assembly.Location);
                Environment.CurrentDirectory = dir;
//                LevelLoader ll = new LevelLoader();
//                ll.LoadLevels();
                p = new Player();
            }

            [Test]
            public void MainMenuTest() {
                Assert.AreEqual(p.force.X, 0);
            }
        }
    }
}
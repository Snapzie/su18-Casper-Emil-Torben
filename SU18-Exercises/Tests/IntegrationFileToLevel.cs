using System;
using System.IO;
using NUnit.Framework;
using SpaceTaxi_1;
using SpaceTaxi_1.LevelParsing;
using System.Collections.Generic;

namespace Tests {
        [TestFixture]
        public class IntegrationFileToLevel {
            [SetUp]
            public void SetUp() {
                //Sets working dirrectory in order to find assets
                var dir = Path.GetDirectoryName(typeof(LevelLoader).Assembly.Location);
                Environment.CurrentDirectory = dir;
               
            }

            [Test]
            public void FileToLevelKeeperTest() {
                LevelLoader.LoadLevels();
                Level lvl = LevelsKeeper.Instance.GetLevel(1);
                Assert.AreEqual("SHORT -N- SWEET", lvl.Name);
                Assert.AreEqual(lvl.Platforms, new List<char> {'1'});
                Assert.AreEqual(lvl.Customers, new List<string> {"Alice 10 1 ^J 10 100"});
                lvl = LevelsKeeper.Instance.GetLevel(2);
                Assert.AreEqual("THE BEACH", lvl.Name);
                Assert.AreEqual(lvl.Platforms, new List<char> {'J', 'i', 'r'});
                Assert.AreEqual(lvl.Customers, new List<string> {"Bob 10 J r 10 100", "Carol 30 r ^ 10 100"});
            }
        }
}
using System;
using System.IO;
using NUnit.Framework;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using SpaceTaxi_1;
using SpaceTaxi_1.LevelParsing;
using SpaceTaxi_1.MockUps;

namespace Tests {
    [TestFixture]
    public class CustomerTests {
        [SetUp]
        public void SetUp() {
            var dir = Path.GetDirectoryName(typeof(LevelLoader).Assembly.Location);
            Environment.CurrentDirectory = dir;
        }
        [Test]
        public void TestPosition() {
            
        }
    }
}